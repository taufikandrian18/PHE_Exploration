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
    public class LGEconomicRepository : BaseCrudRepository<LG_Economic, LGEconomicDto, LGEconomicDto, TXEConomicQuery>, ILGEconomicRepository
    {
        public LGEconomicRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
                : base(explorationContext, connection, new TXEConomicQuery(), hrContext)
        {

        }

        public override async Task Create(LGEconomicDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_Economic.Add(entity);
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

        public override async Task<LGEconomicDto> GetOne(string structureId)
        {
            var entity = await _explorationContext.LG_Economic.FindAsync(structureId);
            var dto = new LGEconomicDto(entity);
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

        public List<LGEconomicDto> GetAll()
        {
            List<LG_Economic> result = _explorationContext.Set<LG_Economic>().ToList();
            List<LGEconomicDto> dto = (List<LGEconomicDto>)Activator.CreateInstance(typeof(List<LGEconomicDto>), result);
            return dto;
        }
    }
}
