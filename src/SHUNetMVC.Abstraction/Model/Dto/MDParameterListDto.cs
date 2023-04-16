using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDParameterListDto : BaseDtoAutoMapper<MD_ParamaterList>
    {
        public MDParameterListDto()
        {

        }
        public MDParameterListDto(MD_ParamaterList entity) : base(entity)
        {

        }

        public string Schema { get; set; }
        public string ParamID { get; set; }
        public string ParamListID { get; set; }
        public decimal ParamValue1 { get; set; }
        public string ParamValue1Text { get; set; }
        public decimal ParamValue2 { get; set; }
        public string ParamValue2Text { get; set; }
        public string ParamListDesc { get; set; }
        public int RowOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
