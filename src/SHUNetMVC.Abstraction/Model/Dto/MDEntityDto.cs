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
    public class MDEntityDto : BaseDtoAutoMapper<MP_Entity>
    {
        [DisplayName("Effective Year")]
        public string EffectiveYear { get; set; }
        [ScaffoldColumn(false)]
        public string SubholdingID { get; set; }
        [DisplayName("Entity Name")]
        public string EntityName { get; set; }
        [ScaffoldColumn(false)]
        public string RegionalID { get; set; }
        [DisplayName("Regional Name")]
        public string RegionalName { get; set; }
        [ScaffoldColumn(false)]
        public string ZonaID { get; set; }
        [DisplayName("Zona Name")]
        public string ZonaName { get; set; }
        [ScaffoldColumn(false)]
        public string xBlockID { get; set; }
        [DisplayName("Block Name")]
        public string xBlockName { get; set; }
        [ScaffoldColumn(false)]
        public string BasinID { get; set; }
        [DisplayName("Basin Name")]
        public string BasinName { get; set; }
        [ScaffoldColumn(false)]
        public string xAssetID { get; set; }
        [DisplayName("Asset Name")]
        public string xAssetName { get; set; }
        [ScaffoldColumn(false)]
        public string APHID { get; set; }
        [DisplayName("APH Name")]
        public string APHName { get; set; }
        [ScaffoldColumn(false)]
        public string xAreaID { get; set; }
        [DisplayName("Area Name")]
        public string xAreaName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
