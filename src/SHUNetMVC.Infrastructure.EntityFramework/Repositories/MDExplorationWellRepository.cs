using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
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
    public class MDExplorationWellRepository : BaseCrudRepository<MD_ExplorationWell, MDExplorationWellDto, MDExplorationWellDto, MDExplorationWellQuery>, IMDExplorationWellRepository
    {
        public MDExplorationWellRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new MDExplorationWellQuery(), hrContext)
        {

        }

        public override async Task Create(MDExplorationWellDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.MD_ExplorationWell.Add(entity);
                await _explorationContext.SaveChangesAsync();
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
                //var literal = "xW";
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
                    var idTmp = "xW0000000";
                    var newId = Regex.Replace(idTmp, "\\d+", m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
                    return newId;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<MDExplorationWellDto> GetOne(string wellId)
        {
            var entity = await _explorationContext.MD_ExplorationWell.FindAsync(wellId);
            var dto = new MDExplorationWellDto(entity);
            return dto;
        }

        public void DestroyWell(string wellId)
        {
            var item = Task.Run(async () => await _explorationContext.Set<MD_ExplorationWell>().FindAsync(wellId)).Result;
            _explorationContext.Set<MD_ExplorationWell>().Remove(item);
            Task.Run(async () => await _explorationContext.SaveChangesAsync());
        }

        public async Task Destroy(string wellID)
        {
            try
            {
                var wellItem = await _explorationContext.Set<MD_ExplorationWell>().FindAsync(wellID);
                _explorationContext.Set<MD_ExplorationWell>().Remove(wellItem);
                await _explorationContext.SaveChangesAsync();
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
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM dbo.MD_ExplorationWell ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }
    }
}
