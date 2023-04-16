using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class LGProsResourcesTargetRepository : BaseCrudRepository<LG_ProsResourcesTarget, LGProsResourcesTargetDto, TXPropectiveResourceTargetWthFields, TXProsResourcesTargetQuery>, ILGProsResourcesTargetRepository
    {
        public LGProsResourcesTargetRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new TXProsResourcesTargetQuery(), hrContext)
        {

        }

        public override async Task Create(LGProsResourcesTargetDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_ProsResourcesTarget.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<LGProsResourcesTargetDto>> GetProsResourceTargetByStructureID(string structureID)
        {
            var getResult = await GetLookupListText(structureID);
            return getResult.ToList();
        }

        public async Task Destroy(string targetID)
        {
            try
            {
                var item = await _explorationContext.Set<TX_ProsResourcesTarget>().FindAsync(targetID);
                _explorationContext.Set<TX_ProsResourcesTarget>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void DestroyTarget(string targetID)
        {
            try
            {
                var item = Task.Run(async () => await _explorationContext.Set<TX_ProsResourcesTarget>().FindAsync(targetID)).Result;
                _explorationContext.Set<TX_ProsResourcesTarget>().Remove(item);
                Task.Run(async () => await _explorationContext.SaveChangesAsync());
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> GenerateNewID()
        {
            try
            {
                //var getCount = await GetCountDataTable();
                //var sequence = int.Parse(getCount);
                ////var sequence = 9090909090;
                //var literal = "xT";
                //var id = literal + (sequence + 1).ToString("0000000");
                //return id;

                var getCount = await GetCountDataTable();
                if (!string.IsNullOrEmpty(getCount))
                {
                    var newId = Regex.Replace(getCount, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
                else
                {
                    var idTmp = "xT0000000";
                    var newId = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            using (var connection = OpenConnection())
            {
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM xplore.TX_ProsResourcesTarget ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<LGProsResourcesTargetDto> GetAll()
        {
            List<LG_ProsResourcesTarget> result = _explorationContext.Set<LG_ProsResourcesTarget>().ToList();
            List<LGProsResourcesTargetDto> dto = (List<LGProsResourcesTargetDto>)Activator.CreateInstance(typeof(List<LGProsResourcesTargetDto>), result);
            return dto;
        }
    }
}
