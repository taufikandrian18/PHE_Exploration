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
    public class LGProsResourcesRepository : BaseCrudRepository<LG_ProsResources, LGProsResourceDto, LGProsResourceDto, TXProsResourcesQuery>, ILGProsResourcesRepository
    {
        public LGProsResourcesRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXProsResourcesQuery(), hrContext)
        {

        }

        public override async Task Create(LGProsResourceDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_ProsResources.Add(entity);
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
                var item = await _explorationContext.Set<TX_ProsResources>().FindAsync(structureID);
                _explorationContext.Set<TX_ProsResources>().Remove(item);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DestroyTarget(string structureID)
        {
            var item = Task.Run(async () => await _explorationContext.Set<TX_ProsResources>().FindAsync(structureID)).Result;
            _explorationContext.Set<TX_ProsResources>().Remove(item);
            Task.Run(async () => await _explorationContext.SaveChangesAsync());
        }

        public override async Task<LGProsResourceDto> GetOne(string structureId)
        {
            try
            {
                var entity = await _explorationContext.LG_ProsResources.FindAsync(structureId);
                var dto = new LGProsResourceDto(entity);
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
                var items = await connection.QueryAsync<string>($"SELECT DISTINCT {columnId} FROM xplore.TX_ProsResources ORDER BY {columnId}");

                result.Items = items.Select(item => new LookupItem
                {
                    Text = item,
                    Value = item
                }).ToList();
            }


            result.Items = result.Items.GroupBy(o => o.Text).Select(o => o.FirstOrDefault()).ToList();

            return result;
        }

        public List<LGProsResourceDto> GetAll()
        {
            List<LG_ProsResources> result = _explorationContext.Set<LG_ProsResources>().ToList();
            List<LGProsResourceDto> dto = (List<LGProsResourceDto>)Activator.CreateInstance(typeof(List<LGProsResourceDto>), result);
            return dto;
        }
    }
}
