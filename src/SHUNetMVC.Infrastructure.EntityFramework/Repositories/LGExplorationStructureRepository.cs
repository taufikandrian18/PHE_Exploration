using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class LGExplorationStructureRepository : BaseCrudRepository<LG_ExplorationStructure, LGExplorationStructureDto, LGExplorationStructureDto, MDExplorationStructureQuery>, ILGExplorationStructureRepository
    {
        public LGExplorationStructureRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new MDExplorationStructureQuery(), hrContext)
        {
        }

        public override async Task Create(LGExplorationStructureDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_ExplorationStructure.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task Update(LGExplorationStructureDto dto)
        {
            try
            {
                //var existing = _context.Employees.Find(dto.EmpId);

                var data = dto.ToEntity();
                _explorationContext.LG_ExplorationStructure.AddOrUpdate(data);
                await _explorationContext.SaveChangesAsync();
            }
            catch (System.Exception e)
            {

                throw e;
            }

        }

        public override async Task<LGExplorationStructureDto> GetOne(string structureId)
        {
            try
            {
                var entity = await _explorationContext.LG_ExplorationStructure.FindAsync(structureId);
                var dto = new LGExplorationStructureDto(entity);
                return dto;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            if (columnId == "AssetName")
            {
                result.Items = _explorationContext.MD_ExplorationStructure.AsNoTracking()
                    .OrderBy(o => o.MD_ExplorationAsset.xAssetName)
                    .Select(o => new LookupItem
                    {
                        Value = o.MD_ExplorationAsset.xAssetName,
                        Text = o.MD_ExplorationAsset.xAssetName
                    }).Distinct().ToList();
            }
            else if (columnId == "BasinName")
            {
                result.Items = _explorationContext.MD_ExplorationStructure.AsNoTracking()
                    .OrderBy(o => o.MD_ExplorationBasin.BasinName)
                    .Select(o => new LookupItem
                    {
                        Value = o.MD_ExplorationBasin.BasinName,
                        Text = o.MD_ExplorationBasin.BasinName
                    }).Distinct().ToList();
            }
            else if (columnId == "BlockName")
            {
                result.Items = _explorationContext.MD_ExplorationStructure.AsNoTracking()
                    .OrderBy(o => o.MD_ExplorationBlock.xBlockName)
                    .Select(o => new LookupItem
                    {
                        Value = o.MD_ExplorationBlock.xBlockName,
                        Text = o.MD_ExplorationBlock.xBlockName
                    }).Distinct().ToList();
            }
            else
            {
                using (var connection = OpenConnection())
                {
                    _explorationContext.MD_ExplorationStructure.Select(o => o.MD_ExplorationAsset.xAssetName);
                    var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM dbo.MD_ExplorationStructure ORDER BY {columnId}");

                    result.Items = items.Select(item => new LookupItem
                    {
                        Text = item,
                        Value = item
                    }).ToList();
                }
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<LG_ExplorationStructure> GetByStructureName(string structureName)
        {
            return _explorationContext.LG_ExplorationStructure.Where(o => o.xStructureName == structureName).ToList();
        }

        public async Task<string> GenerateNewID()
        {
            try
            {
                //var getCount = await GetCountDataTable();
                //var idTmp = "xS00000001";
                //var Testid = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                //var sequence = int.Parse(getCount);
                ////var sequence = 9090909090;
                //var literal = "xS";
                //var id = literal + (sequence + 1).ToString("0000000");
                //return id;

                var getCount = await GetCountDataTable();
                if(!string.IsNullOrEmpty(getCount))
                {
                    var newId = Regex.Replace(getCount, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
                else
                {
                    var idTmp = "xS0000000";
                    var newId = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<LG_ExplorationStructure> GetByStructureID(string structureID)
        {
            return _explorationContext.LG_ExplorationStructure.Where(o => o.xStructureID == structureID).ToList();
        }
    }
}
