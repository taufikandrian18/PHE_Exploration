using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class HRISDevOrgUnitHierarchyDto : BaseDtoAutoMapper<DIM_OrgUnitHierarchy>
    {
        public string LvlEntity { get; set; }
    }
}
