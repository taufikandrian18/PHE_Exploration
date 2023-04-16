using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDParameterDto : BaseDtoAutoMapper<MD_Paramater>
    {
        public MDParameterDto()
        {

        }
        public MDParameterDto(MD_Paramater entity) : base(entity)
        {

        }

        public string Schema { get; set; }
        public string ParamID { get; set; }
        public string ParamDesc { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
