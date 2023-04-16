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
    public class TXProsResourceDto : BaseDtoAutoMapper<TX_ProsResources>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        [ScaffoldColumn(false)]
        public string ExplorationStructureName { get; set; }
        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90InPlaceOilPRTemplate")]
        public decimal P90InPlaceOilPR { get; set; }
        [ScaffoldColumn(false)]
        public string P90InPlaceOilPRUoM { get; set; }
        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50InPlaceOilPRTemplate")]
        public decimal P50InPlaceOilPR { get; set; }
        [ScaffoldColumn(false)]
        public string P50InPlaceOilPRUoM { get; set; }
        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanInPlaceOilPRTemplate")]
        public decimal PMeanInPlaceOilPR { get; set; }
        [ScaffoldColumn(false)]
        public string PMeanInPlaceOilPRUoM { get; set; }
        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10InPlaceOilPRTemplate")]
        public decimal P10InPlaceOilPR { get; set; }
        [ScaffoldColumn(false)]
        public string P10InPlaceOilPRUoM { get; set; }
        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90InPlaceGasPRTemplate")]
        public decimal P90InPlaceGasPR { get; set; }
        [ScaffoldColumn(false)]
        public string P90InPlaceGasPRUoM { get; set; }
        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50InPlaceGasPRTemplate")]
        public decimal P50InPlaceGasPR { get; set; }
        [ScaffoldColumn(false)]
        public string P50InPlaceGasPRUoM { get; set; }
        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanInPlaceGasPRTemplate")]
        public decimal PMeanInPlaceGasPR { get; set; }
        [ScaffoldColumn(false)]
        public string PMeanInPlaceGasPRUoM { get; set; }
        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10InPlaceGasPRTemplate")]
        public decimal P10InPlaceGasPR { get; set; }
        [ScaffoldColumn(false)]
        public string P10InPlaceGasPRUoM { get; set; }
        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P90InPlaceTotalPR { get; set; }
        [ScaffoldColumn(false)]
        public string P90InPlaceTotalPRUoM { get; set; }
        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P50InPlaceTotalPR { get; set; }
        [ScaffoldColumn(false)]
        public string P50InPlaceTotalPRUoM { get; set; }
        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal PMeanInPlaceTotalPR { get; set; }
        [ScaffoldColumn(false)]
        public string PMeanInPlaceTotalPRUoM { get; set; }
        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P10InPlaceTotalPR { get; set; }
        [ScaffoldColumn(false)]
        public string P10InPlaceTotalPRUoM { get; set; }
        [Required]
        [DisplayName("RF Oil (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("RFOilPRTemplate")]
        public decimal RFOilPR { get; set; }
        [Required]
        [DisplayName("RF Gas (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("RFGasPRTemplate")]
        public decimal RFGasPR { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90RROilTemplate")]
        public decimal P90RROil { get; set; }
        [ScaffoldColumn(false)]
        public string P90RROilUoM { get; set; }
        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50RROilTemplate")]
        public decimal P50RROil { get; set; }
        [ScaffoldColumn(false)]
        public string P50RROilUoM { get; set; }
        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanRROilTemplate")]
        public decimal PMeanRROil { get; set; }
        [ScaffoldColumn(false)]
        public string PMeanRROilUoM { get; set; }
        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10RROilTemplate")]
        public decimal P10RROil { get; set; }
        [ScaffoldColumn(false)]
        public string P10RROilUoM { get; set; }
        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90RRGasTemplate")]
        public decimal P90RRGas { get; set; }
        [ScaffoldColumn(false)]
        public string P90RRGasUoM { get; set; }
        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50RRGasTemplate")]
        public decimal P50RRGas { get; set; }
        [ScaffoldColumn(false)]
        public string P50RRGasUoM { get; set; }
        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanRRGasTemplate")]
        public decimal PMeanRRGas { get; set; }
        [ScaffoldColumn(false)]
        public string PMeanRRGasUoM { get; set; }
        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10RRGasTemplate")]
        public decimal P10RRGas { get; set; }
        [ScaffoldColumn(false)]
        public string P10RRGasUoM { get; set; }
        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P90RRTotal { get; set; }
        [ScaffoldColumn(false)]
        public string P90RRTotalUoM { get; set; }
        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P50RRTotal { get; set; }
        [ScaffoldColumn(false)]
        public string P50RRTotalUoM { get; set; }
        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal PMeanRRTotal { get; set; }
        [ScaffoldColumn(false)]
        public string PMeanRRTotalUoM { get; set; }
        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P10RRTotal { get; set; }
        [ScaffoldColumn(false)]
        public string P10RRTotalUoM { get; set; }
        [Required]
        [DisplayName("Hydrocarbon Type")]
        [UIHint("ClientCategory")]
        public string HydrocarbonTypePRParID { get; set; }
        [Required]
        [DisplayName("GCF SR (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFSRPRTemplate")]
        public decimal GCFSRPR { get; set; }
        [ScaffoldColumn(false)]
        public string GCFSRPRUoM { get; set; }
        [Required]
        [DisplayName("GCF TM (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFTMPRTemplate")]
        public decimal GCFTMPR { get; set; }
        [ScaffoldColumn(false)]
        public string GCFTMPRUoM { get; set; }
        [Required]
        [DisplayName("GCF Reservoir (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFReservoirPRTemplate")]
        public decimal GCFReservoirPR { get; set; }
        [ScaffoldColumn(false)]
        public string GCFReservoirPRUoM { get; set; }
        [Required]
        [DisplayName("GCF Closure (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFClosurePRTemplate")]
        public decimal GCFClosurePR { get; set; }
        [ScaffoldColumn(false)]
        public string GCFClosurePRUoM { get; set; }
        [Required]
        [DisplayName("GCF Containment (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFContainmentPRTemplate")]
        public decimal GCFContainmentPR { get; set; }
        [ScaffoldColumn(false)]
        public string GCFContainmentPRUoM { get; set; }
        [Required]
        [DisplayName("GCF PG Total (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFPGTotalPRTemplate")]
        public decimal GCFPGTotalPR { get; set; }
        [ScaffoldColumn(false)]
        public string GCFPGTotalPRUoM { get; set; }
        public decimal ExpectedPG { get; set; }
        public decimal CurrentPG { get; set; }
        [ScaffoldColumn(false)]
        public string MethodParID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public TXProsResourceDto()
        {

        }

        public TXProsResourceDto(TX_ProsResources entity) : base(entity)
        {

        }
    }
}
