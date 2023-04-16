using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class LGContingenResourcesDto : BaseDtoAutoMapper<LG_ContingentResources>
    {
        public string StructureHistoryID { get; set; }
        public string xStructureID { get; set; }
        public Nullable<decimal> C1COil { get; set; }
        public string C1COilUoM { get; set; }
        public Nullable<decimal> C2COil { get; set; }
        public string C2COilUoM { get; set; }
        public Nullable<decimal> C3COil { get; set; }
        public string C3COilUoM { get; set; }
        public Nullable<decimal> C1CGas { get; set; }
        public string C1CGasUoM { get; set; }
        public Nullable<decimal> C2CGas { get; set; }
        public string C2CGasUoM { get; set; }
        public Nullable<decimal> C3CGas { get; set; }
        public string C3CGasUoM { get; set; }
        public Nullable<decimal> C1CTotal { get; set; }
        public string C1CTotalUoM { get; set; }
        public Nullable<decimal> C2CTotal { get; set; }
        public string C2CTotalUoM { get; set; }
        public Nullable<decimal> C3CTotal { get; set; }
        public string C3CTotalUoM { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual LG_ExplorationStructure LG_ExplorationStructure { get; set; }

        public LGContingenResourcesDto()
        {

        }

        public LGContingenResourcesDto(LG_ContingentResources entity) : base(entity)
        {

        }
    }
}
