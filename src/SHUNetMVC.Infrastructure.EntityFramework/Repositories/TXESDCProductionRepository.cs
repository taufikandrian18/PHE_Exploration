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
    public class TXESDCProductionRepository : BaseCrudRepository<TX_ESDCProd, TXESDCProductionDto, TXESDCProductionDto, TXESDCProductionQuery>, ITXESDCProductionRepository
    {
        public TXESDCProductionRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXESDCProductionQuery(), hrContext)
        {

        }

        public override async Task Create(TXESDCProductionDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.TX_ESDCProd.Add(entity);
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
                var item = await _explorationContext.Set<TX_ESDCProd>().FindAsync(structureID);
                _explorationContext.Set<TX_ESDCProd>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<TXESDCProductionDto>> GetListTXESDCProductionByStructureID(string structureID)
        {
            var getResult = await GetLookupListText(structureID);
            return getResult.ToList();
        }

        public void DestroyTarget(string structureID)
        {
            var item = Task.Run(async () => await _explorationContext.Set<TX_ESDCProd>().FindAsync(structureID)).Result;
            _explorationContext.Set<TX_ESDCProd>().Remove(item);
            Task.Run(async () => await _explorationContext.SaveChangesAsync());
        }
        public async Task<TX_ESDCProd> GetProdTargetByStructureID(string structureID)
        {
            try
            {
                var getResult = await _explorationContext.TX_ESDCProd.Where(x => x.xStructureID.Trim() == structureID.Trim()).FirstOrDefaultAsync();
                return getResult;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<TXESDCProductionDto> GetOne(string structureId)
        {
            try
            {
                var entity = await _explorationContext.TX_ESDCProd.FindAsync(structureId);
                var dto = new TXESDCProductionDto(entity);
                return dto;
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
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM [DB_PHE_Exploration].[xplore].[TX_ESDCProd] ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<TXESDCProductionDto> GetAll()
        {
            List<TX_ESDCProd> result = _explorationContext.Set<TX_ESDCProd>().ToList();
            List<TXESDCProductionDto> dto = (List<TXESDCProductionDto>)Activator.CreateInstance(typeof(List<TXESDCProductionDto>), result);
            return dto;
        }
    }
}
