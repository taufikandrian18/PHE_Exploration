using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class TXProsResourcesTargetDto : BaseDtoAutoMapper<TX_ProsResourcesTarget>
    {
        [ScaffoldColumn(false)]
        public string TargetID { get; set; }

        [Required]
        [DisplayName("Target name")]
        [Remote("IsTargetName_Available", "Validation")]
        public string TargetName { get; set; }

        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90InPlaceOilTemplate")]
        public decimal P90InPlaceOil { get; set; }

        [ScaffoldColumn(false)]
        public string P90InPlaceOilUoM { get; set; }

        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50InPlaceOilTemplate")]
        public decimal P50InPlaceOil { get; set; }

        [ScaffoldColumn(false)]
        public string P50InPlaceOilUoM { get; set; }

        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanInPlaceOilTemplate")]
        public decimal PMeanInPlaceOil { get; set; }

        [ScaffoldColumn(false)]
        public string PMeanInPlaceOilUoM { get; set; }

        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10InPlaceOilTemplate")]
        public decimal P10InPlaceOil { get; set; }

        [ScaffoldColumn(false)]
        public string P10InPlaceOilUoM { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90InPlaceGasTemplate")]
        public decimal P90InPlaceGas { get; set; }

        [ScaffoldColumn(false)]
        public string P90InPlaceGasUoM { get; set; }

        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50InPlaceGasTemplate")]
        public decimal P50InPlaceGas { get; set; }

        [ScaffoldColumn(false)]
        public string P50InPlaceGasUoM { get; set; }

        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanInPlaceGasTemplate")]
        public decimal PMeanInPlaceGas { get; set; }

        [ScaffoldColumn(false)]
        public string PMeanInPlaceGasUoM { get; set; }

        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10InPlaceGasTemplate")]
        public decimal P10InPlaceGas { get; set; }

        [ScaffoldColumn(false)]
        public string P10InPlaceGasUoM { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P90InPlaceTotal { get; set; }

        [ScaffoldColumn(false)]
        public string P90InPlaceTotalUoM { get; set; }

        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P50InPlaceTotal { get; set; }

        [ScaffoldColumn(false)]
        public string P50InPlaceTotalUoM { get; set; }

        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal PMeanInPlaceTotal { get; set; }

        [ScaffoldColumn(false)]
        public string PMeanInPlaceTotalUoM { get; set; }

        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P10InPlaceTotal { get; set; }

        [ScaffoldColumn(false)]
        public string P10InPlaceTotalUoM { get; set; }

        [Required]
        [DisplayName("RF Oil (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("RFOilTemplate")]
        public decimal RFOil { get; set; }
        [Required]
        [DisplayName("RF Gas (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("RFGasTemplate")]
        public decimal RFGas { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90RecoverableOilTemplate")]
        public decimal P90RecoverableOil { get; set; }

        [ScaffoldColumn(false)]
        public string P90RecoverableOilUoM { get; set; }

        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50RecoverableOilTemplate")]
        public decimal P50RecoverableOil { get; set; }

        [ScaffoldColumn(false)]
        public string P50RecoverableOilUoM { get; set; }

        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanRecoverableOilTemplate")]
        public decimal PMeanRecoverableOil { get; set; }

        [ScaffoldColumn(false)]
        public string PMeanRecoverableOilUoM { get; set; }

        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10RecoverableOilTemplate")]
        public decimal P10RecoverableOil { get; set; }

        [ScaffoldColumn(false)]
        public string P10RecoverableOilUoM { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90RecoverableGasTemplate")]
        public decimal P90RecoverableGas { get; set; }

        [ScaffoldColumn(false)]
        public string P90RecoverableGasUoM { get; set; }

        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50RecoverableGasTemplate")]
        public decimal P50RecoverableGas { get; set; }

        [ScaffoldColumn(false)]
        public string P50RecoverableGasUoM { get; set; }

        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("PMeanRecoverableGasTemplate")]
        public decimal PMeanRecoverableGas { get; set; }

        [ScaffoldColumn(false)]
        public string PMeanRecoverableGasUoM { get; set; }

        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10RecoverableGasTemplate")]
        public decimal P10RecoverableGas { get; set; }

        [ScaffoldColumn(false)]
        public string P10RecoverableGasUoM { get; set; }

        [Required]
        [DisplayName("P90")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P90RecoverableTotal { get; set; }

        [ScaffoldColumn(false)]
        public string P90RecoverableTotalUoM { get; set; }

        [Required]
        [DisplayName("P50")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P50RecoverableTotal { get; set; }

        [ScaffoldColumn(false)]
        public string P50RecoverableTotalUoM { get; set; }

        [Required]
        [DisplayName("PMean")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal PMeanRecoverableTotal { get; set; }

        [ScaffoldColumn(false)]
        public string PMeanRecoverableTotalUoM { get; set; }

        [Required]
        [DisplayName("P10")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal P10RecoverableTotal { get; set; }

        [ScaffoldColumn(false)]
        public string P10RecoverableTotalUoM { get; set; }

        [Required]
        [DisplayName("Hydrocarbon Type")]
        [UIHint("ClientCategory")]
        public string HydrocarbonTypeParID { get; set; }

        [Required]
        [DisplayName("GCF SR (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFSRTemplate")]
        public decimal GCFSR { get; set; }

        [ScaffoldColumn(false)]
        public string GCFSRUoM { get; set; }

        [Required]
        [DisplayName("GCF TM (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFTMTemplate")]
        public decimal GCFTM { get; set; }

        [ScaffoldColumn(false)]
        public string GCFTMUoM { get; set; }

        [Required]
        [DisplayName("GCF Reservoir (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFReservoirTemplate")]
        public decimal GCFReservoir { get; set; }

        [ScaffoldColumn(false)]
        public string GCFReservoirUoM { get; set; }

        [Required]
        [DisplayName("GCF Closure (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFClosureTemplate")]
        public decimal GCFClosure { get; set; }

        [ScaffoldColumn(false)]
        public string GCFClosureUoM { get; set; }

        [Required]
        [DisplayName("GCF Containment (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFContainmentTemplate")]
        public decimal GCFContainment { get; set; }

        [ScaffoldColumn(false)]
        public string GCFContainmentUoM { get; set; }

        [Required]
        [DisplayName("GCF PG Total (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("GCFPGTotalTemplate")]
        public decimal GCFPGTotal { get; set; }

        [ScaffoldColumn(false)]
        public string GCFPGTotalUoM { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }






        //[Required]
        //[DisplayName("P50")]
        //[DataType(DataType.Currency)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        //[Range(0, double.MaxValue)]
        //public decimal P50InPlaceOil { get; set; }
    }
}
