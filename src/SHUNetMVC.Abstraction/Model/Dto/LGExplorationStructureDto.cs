using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class LGExplorationStructureDto : BaseDtoAutoMapper<LG_ExplorationStructure>
    {
        #region DB Field
        public string StructureHistoryID { get; set; }
        public string xStructureID { get; set; }
        public string xStructureName { get; set; }
        public string xStructureStatusParID { get; set; }
        public string SingleOrMultiParID { get; set; }
        public string ExplorationTypeParID { get; set; }
        public string SubholdingID { get; set; }
        public string BasinID { get; set; }
        public string RegionalID { get; set; }
        public string ZonaID { get; set; }
        public string APHID { get; set; }
        public string xAssetID { get; set; }
        public string xBlockID { get; set; }
        public string xAreaID { get; set; }
        public string UDClassificationParID { get; set; }
        public string UDSubClassificationParID { get; set; }
        public string UDSubTypeParID { get; set; }
        public string ExplorationAreaParID { get; set; }
        public string CountriesID { get; set; }
        public string Play { get; set; }
        public string StatusData { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string MadamTransID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        #endregion

        #region Additional Fields
        public string xAssetName { get; set; }
        public string xAreaName { get; set; }
        public string BasinName { get; set; }
        public string xBlockName { get; set; }
        #endregion

        #region attachment link
        public string OnePagerMontage { get; set; }
        public string StructureOutline { get; set; }
        #endregion

        public LGExplorationStructureDto()
        {

        }

        public LGExplorationStructureDto(LG_ExplorationStructure entity) : base(entity)
        {

        }
    }
}
