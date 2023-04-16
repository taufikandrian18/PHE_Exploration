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
    public class TXEconomicRepository : BaseCrudRepository<TX_Economic, TXEconomicDto, TXEconomicDto, TXEConomicQuery>, ITXEconomicRepository
    {
        public TXEconomicRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXEConomicQuery(), hrContext)
        {

        }

        public override async Task Create(TXEconomicDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_Economic.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task Destroy(string structureID)
        {
            var item = await _explorationContext.Set<TX_Economic>().FindAsync(structureID);
            _explorationContext.Set<TX_Economic>().Remove(item);
            await _explorationContext.SaveChangesAsync();
        }

        public override async Task<TXEconomicDto> GetOne(string structureId)
        {
            var entity = await _explorationContext.TX_Economic.FindAsync(structureId);
            var dto = new TXEconomicDto(entity);
            return dto;
        }

        public override async Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            var result = new LookupList
            {
                ColumnId = columnId
            };

            using (var connection = OpenConnection())
            {
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM xplore.TX_Economic ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<TXEconomicDto> GetAll()
        {
            List<TX_Economic> result = _explorationContext.Set<TX_Economic>().ToList();
            List<TXEconomicDto> dto = (List<TXEconomicDto>)Activator.CreateInstance(typeof(List<TXEconomicDto>), result);
            return dto;
        }

        public async Task<IEnumerable<TXEconomicExcelDto>> GetExportEconomicByStructureID(string structureID)
        {
            TXEConomicQuery queryObj = new TXEConomicQuery();
            using (var connection = OpenConnection())
            {
                var querySQL = string.Format(queryObj.ExcelExportQuery, structureID);
                var lookupText = await connection.QueryAsync<TXEconomicExcelDto>(querySQL);
                return lookupText;
            }
        }
    }
}
