using ASPNetMVC.Abstraction.Model.Entities;
using Dapper;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class TXContingenResourcesRepository : BaseCrudRepository<TX_ContingentResources, TXContingenResourcesDto, TXContingenResourcesDto, TXContingenResourcesQuery>, ITXContingenResourcesRepository
    {
        public TXContingenResourcesRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new TXContingenResourcesQuery(), hrContext)
        {

        }

        public override async Task Create(TXContingenResourcesDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_ContingentResources.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task Destroy(string structureID)
        {
            try
            {
                var item = await _explorationContext.Set<TX_ContingentResources>().FindAsync(structureID);
                _explorationContext.Set<TX_ContingentResources>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DestroyTarget(string structureID)
        {
            var item = Task.Run(async () => await _explorationContext.Set<TX_ContingentResources>().FindAsync(structureID)).Result;
            _explorationContext.Set<TX_ContingentResources>().Remove(item);
            Task.Run(async () => await _explorationContext.SaveChangesAsync());
        }

        public async Task<TX_ContingentResources> GetContResourceTargetByStructureID(string structureID)
        {
            try
            {
                var getResult = await _explorationContext.TX_ContingentResources.Where(x => x.xStructureID.Trim() == structureID.Trim()).FirstOrDefaultAsync();
                return getResult;
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
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM xplore.TX_ContingentResources ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<TXContingenResourcesDto> GetAll()
        {
            List<TX_ContingentResources> result = _explorationContext.Set<TX_ContingentResources>().ToList();
            List<TXContingenResourcesDto> dto = (List<TXContingenResourcesDto>)Activator.CreateInstance(typeof(List<TXContingenResourcesDto>), result);
            return dto;
        }

        public async Task<IEnumerable<TXContResourcesExcelDto>> GetExportContResourceTargetByStructureID(string structureID)
        {
            TXContingenResourcesQuery queryObj = new TXContingenResourcesQuery();
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(queryObj.ExcelExportQuery, structureID);
                var lookupText = await connection.QueryAsync<TXContResourcesExcelDto>(querySQL);
                return lookupText;
            }
        }
    }
}
