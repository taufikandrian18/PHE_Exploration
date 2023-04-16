using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class VWDIMEntityDto : BaseDtoAutoMapper<vw_DIM_Entity>
    {
        public VWDIMEntityDto()
        {

        }

        public VWDIMEntityDto(vw_DIM_Entity entity) : base(entity)
        {

        }

        public string EntityID { get; set; }
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
