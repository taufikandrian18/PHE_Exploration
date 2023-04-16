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
    public class TXESDCDto : BaseDtoAutoMapper<TX_ESDC>
    {
        #region DB Field
        [DisplayName("Structure ID")]
        public string xStructureID { get; set; }
        [DisplayName("Structure Name")]
        public string xStructureName { get; set; }
        [ScaffoldColumn(false)]
        public string xStructureStatusParID { get; set; }
        [ScaffoldColumn(false)]
        public string xStructureStatusName { get; set; }
        [ScaffoldColumn(false)]
        public string SingleOrMultiParID { get; set; }
        [ScaffoldColumn(false)]
        public string SingleOrMultiName { get; set; }
        [ScaffoldColumn(false)]
        public string ExplorationTypeParID { get; set; }
        [ScaffoldColumn(false)]
        public string ExplorationTypeName { get; set; }
        [ScaffoldColumn(false)]
        public string UDClassificationParID { get; set; }
        [ScaffoldColumn(false)]
        public string UDClassificationName { get; set; }
        [ScaffoldColumn(false)]
        public string UDSubClassificationParID { get; set; }
        [ScaffoldColumn(false)]
        public string UDSubClassificationName { get; set; }
        [ScaffoldColumn(false)]
        public string UDSubTypeParID { get; set; }
        [ScaffoldColumn(false)]
        public string UDSubTypeName { get; set; }
        [ScaffoldColumn(false)]
        public string ExplorationAreaParID { get; set; }
        [ScaffoldColumn(false)]
        public string CountriesID { get; set; }
        [ScaffoldColumn(false)]
        public string Play { get; set; }
        #endregion

        #region Additional Fields
        public string AssetName { get; set; }
        public string AreaName { get; set; }
        public string BlockName { get; set; }
        #endregion

        #region attachment link
        public string OnePagerMontage { get; set; }
        public string StructureOutline { get; set; }
        #endregion

        [DisplayName("Effective Year")]
        public string EffectiveYear { get; set; }
        [ScaffoldColumn(false)]
        public string SubholdingID { get; set; }
        [DisplayName("Entity Name")]
        public string SubholdingName { get; set; }
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
        public string ESDCFieldID { get; set; }
        public string ESDCProjectID { get; set; }
        public string ESDCProjectName { get; set; }
        [DisplayName("TX Status")]
        public string StatusData { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public TXESDCDto()
        {

        }

        public TXESDCDto(TX_ESDC entity) : base(entity)
        {

        }
    }
}
