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
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class TXDrillingRepository : BaseCrudRepository<TX_Drilling, TXDrillingDto, TXDrillingDto, TXDrillingQuery>, ITXDrillingRepository
    {
        public TXDrillingRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new TXDrillingQuery(), hrContext)
        {

        }

        public override async Task Create(TXDrillingDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_Drilling.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Destroy(string xStructureID, string xWellID)
        {
            try
            {
                var item = await _explorationContext.Set<TX_Drilling>().FindAsync(new object[] { xStructureID, xWellID });
                _explorationContext.Set<TX_Drilling>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DestroyTarget(string structureID, string wellID)
        {
            var item = Task.Run(async () => await _explorationContext.Set<TX_Drilling>().FindAsync(structureID)).Result;
            var wellItem = Task.Run(async () => await _explorationContext.Set<MD_ExplorationWell>().FindAsync(wellID)).Result;
            _explorationContext.Set<TX_Drilling>().Remove(item);
            _explorationContext.Set<MD_ExplorationWell>().Remove(wellItem);
            Task.Run(async () => await _explorationContext.SaveChangesAsync());
        }

        public async Task<List<TXDrillingDto>> GetDrillingByStructureID(string structureID)
        {
            try
            {
                var getResult = await GetLookupListText(structureID);
                return getResult.ToList();
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
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM xplore.TX_Drilling dr JOIN xplore.MD_ExplorationWell ew ON dr.WellID = ew.xWellID ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<TXDrillingDto> GetAll()
        {
            List<TX_Drilling> result = _explorationContext.Set<TX_Drilling>().ToList();
            List<TXDrillingDto> dto = (List<TXDrillingDto>)Activator.CreateInstance(typeof(List<TXDrillingDto>), result);
            return dto;
        }

        public async Task<IEnumerable<TXDrillingExcelDto>> GetExportDrillingByStructureID(string structureID)
        {
            TXDrillingQuery queryObj = new TXDrillingQuery();
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(queryObj.ExcelExportQuery, structureID);
                var lookupText = await connection.QueryAsync<TXDrillingExcelDto>(querySQL);
                return lookupText;
            }
        }
    }
}
