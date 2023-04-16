using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Infrastructure.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Repositories
{
    public class HRISDevOrgUnitHierarchyRepository : BaseCrudRepository<DIM_OrgUnitHierarchy, HRISDevOrgUnitHierarchyDto, HRISDevOrgUnitHierarchyDto, HRISDevQuery>, IHRISDevOrgUnitHierarchyRepository
    {
        public HRISDevOrgUnitHierarchyRepository(DB_PHE_ExplorationEntities explorationContext, IConnectionProvider connection, DB_PHE_HRIS_DEVEntities hrContext)
        : base(explorationContext, connection, new HRISDevQuery(), hrContext)
        {

        }
        public override Task<string> GetLookupText(string id)
        {
            return base.GetLookupText(id);
        }
    }
}
