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
    public class LGDrillingRepository : BaseCrudRepository<LG_Drilling, LGDrillingDto, LGDrillingDto, TXDrillingQuery>, ILGDrillingRepository
    {
        public LGDrillingRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new TXDrillingQuery(), hrContext)
        {

        }

        public override async Task Create(LGDrillingDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_Drilling.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<LGDrillingDto>> GetDrillingByStructureID(string structureID)
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

        public List<LGDrillingDto> GetAll()
        {
            List<LG_Drilling> result = _explorationContext.Set<LG_Drilling>().ToList();
            List<LGDrillingDto> dto = (List<LGDrillingDto>)Activator.CreateInstance(typeof(List<LGDrillingDto>), result);
            return dto;
        }
    }
}
