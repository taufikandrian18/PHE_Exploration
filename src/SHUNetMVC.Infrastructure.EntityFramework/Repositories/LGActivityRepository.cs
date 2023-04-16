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
    public class LGActivityRepository : BaseCrudRepository<LG_Activity, LGActivityDto, LGActivityDto, TXDrillingQuery>, ILGActivityRepository
    {
        public LGActivityRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
            : base(explorationContext, connection, new TXDrillingQuery(), hrContext)
        {

        }

        public override async Task Create(LGActivityDto model)
        {
            var entity = model.ToEntity();
            try
            {
                _explorationContext.LG_Activity.Add(entity);
                await _explorationContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LGActivityDto> GetAll()
        {
            List<LG_Activity> result = _explorationContext.Set<LG_Activity>().ToList();
            List<LGActivityDto> dto = (List<LGActivityDto>)Activator.CreateInstance(typeof(List<LGActivityDto>), result);
            return dto;
        }
    }
}
