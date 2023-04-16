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
    public class LGContingenResourcesRepository : BaseCrudRepository<LG_ContingentResources, LGContingenResourcesDto, LGContingenResourcesDto, TXContingenResourcesQuery>, ILGContingenResourcesRepository
    {
        public LGContingenResourcesRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new TXContingenResourcesQuery(), hrContext)
        {

        }

        public override async Task Create(LGContingenResourcesDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_ContingentResources.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<LG_ContingentResources> GetContResourceTargetByStructureID(string structureID)
        {
            try
            {
                var getResult = await _explorationContext.LG_ContingentResources.Where(x => x.xStructureID.Trim() == structureID.Trim()).FirstOrDefaultAsync();
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

        public List<LGContingenResourcesDto> GetAll()
        {
            List<LG_ContingentResources> result = _explorationContext.Set<LG_ContingentResources>().ToList();
            List<LGContingenResourcesDto> dto = (List<LGContingenResourcesDto>)Activator.CreateInstance(typeof(List<LGContingenResourcesDto>), result);
            return dto;
        }
    }
}
