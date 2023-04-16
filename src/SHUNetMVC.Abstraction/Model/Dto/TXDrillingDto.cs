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
    public class TXDrillingDto : BaseDtoAutoMapper<TX_Drilling>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        [ScaffoldColumn(false)]
        public string xWellID { get; set; }
        [Required]
        [DisplayName("Well name")]
        [Remote("IsWellName_Available", "Validation")]
        public string xWellName { get; set; }
        [Required]
        [DisplayName("Drilling Location")]
        [UIHint("DrillingLocationTemplate")]
        public string DrillingLocation { get; set; }
        [ScaffoldColumn(false)]
        public int RKAPFiscalYear { get; set; }
        [Required]
        [DisplayName("Play Opener")]
        [UIHint("DrillingBitTemplate")]
        public string PlayOpenerBit { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<bool> PlayOpener { get; set; }
        [Required]
        [DisplayName("Drilling Completion Period (days)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("DrillingCompletionPeriodTemplate")]
        public decimal DrillingCompletionPeriod { get; set; }
        [Required]
        [DisplayName("Location")]
        public string Location { get; set; }
        [Required]
        [DisplayName("Feet")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal WaterDepthFeet { get; set; }
        [Required]
        [DisplayName("Meter")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("WaterDepthMeterTemplate")]
        public decimal WaterDepthMeter { get; set; }
        [Required]
        [DisplayName("Feet")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal TotalDepthFeet { get; set; }
        [Required]
        [DisplayName("Meter")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("TotalDepthMeterTemplate")]
        public decimal TotalDepthMeter { get; set; }
        [Required]
        [DisplayName("Rig Type")]
        [UIHint("RigCategory")]
        public string RigTypeParID { get; set; }
        [Required]
        [DisplayName("Well Type")]
        [UIHint("WellCategory")]
        public string WellTypeParID { get; set; }
        [Required]
        [DisplayName("Latitude (decimal)")]
        [UIHint("SurfaceLocationLatitudeTemplate")]
        public decimal SurfaceLocationLatitude { get; set; }
        [Required]
        [DisplayName("Longitude (decimal)")]
        [UIHint("SurfaceLocationLongitudeTemplate")]
        public decimal SurfaceLocationLongitude { get; set; }
        [Required]
        [DisplayName("Latitude (decimal)")]
        [UIHint("BHLocationLatitudeTemplate")]
        public decimal BHLocationLatitude { get; set; }
        [Required]
        [DisplayName("Longitude (decimal)")]
        [UIHint("BHLocationLongitudeTemplate")]
        public decimal BHLocationLongitude { get; set; }
        [Required]
        [DisplayName("Commitment Well")]
        [UIHint("DrillingBitTemplate")]
        public string CommitmentWellBit { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<bool> CommitmentWell { get; set; }
        [Required]
        [DisplayName("Operational Context")]
        [UIHint("OperationalContextParIdTemplate")]
        public string OperationalContextParId { get; set; }
        [Required]
        [DisplayName("Potential Delay")]
        [UIHint("DrillingBitTemplate")]
        public string PotentialDelayBit { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<bool> PotentialDelay { get; set; }
        [Required]
        [DisplayName("Net Revenue Interest (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("NetRevenueInterestTemplate")]
        public decimal NetRevenueInterest { get; set; }
        [Required]
        [DisplayName("CHB")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("DrillingCostTemplate")]
        public decimal DrillingCost { get; set; }
        [ScaffoldColumn(false)]
        public string DrillingCostCurr { get; set; }
        [Required]
        [DisplayName("DHB")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("DrillingCostDHBTemplate")]
        public decimal DrillingCostDHB { get; set; }
        [ScaffoldColumn(false)]
        public string DrillingCostDHBCurr { get; set; }
        [Required]
        [DisplayName("Expected Drilling Date")]
        [UIHint("DatePickerTemplate")]
        public string ExpectedDrillingDate { get; set; }
        [Required]
        [DisplayName("P90 Oil (MMBO)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90ResourceOilTemplate")]
        public decimal P90ResourceOil { get; set; }

        [ScaffoldColumn(false)]
        public string P90ResourceOilUoM { get; set; }

        [Required]
        [DisplayName("P50 Oil (MMBO)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50ResourceOilTemplate")]
        public decimal P50ResourceOil { get; set; }

        [ScaffoldColumn(false)]
        public string P50ResourceOilUoM { get; set; }

        [Required]
        [DisplayName("P10 Oil (MMBO)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10ResourceOilTemplate")]
        public decimal P10ResourceOil { get; set; }

        [ScaffoldColumn(false)]
        public string P10ResourceOilUoM { get; set; }

        [Required]
        [DisplayName("P90 Gas (BCF)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90ResourceGasTemplate")]
        public decimal P90ResourceGas { get; set; }

        [ScaffoldColumn(false)]
        public string P90ResourceGasUoM { get; set; }

        [Required]
        [DisplayName("P50 Gas (BCF)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50ResourceGasTemplate")]
        public decimal P50ResourceGas { get; set; }

        [ScaffoldColumn(false)]
        public string P50ResourceGasUoM { get; set; }

        [Required]
        [DisplayName("P10 Gas (BCF)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10ResourceGasTemplate")]
        public decimal P10ResourceGas { get; set; }

        [ScaffoldColumn(false)]
        public string P10ResourceGasUoM { get; set; }

        [Required]
        [DisplayName("Expected PG (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("ExpectedPGTemplate")]
        public decimal ExpectedPG { get; set; }
        [Required]
        [DisplayName("Current PG (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("CurrentPGTemplate")]
        public decimal CurrentPG { get; set; }

        [Required]
        [DisplayName("Source (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("ChanceComponentSourceTemplate")]
        public decimal ChanceComponentSource { get; set; }
        [Required]
        [DisplayName("Timing (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("ChanceComponentTimingTemplate")]
        public decimal ChanceComponentTiming { get; set; }
        [Required]
        [DisplayName("Reservoir (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("ChanceComponentReservoirTemplate")]
        public decimal ChanceComponentReservoir { get; set; }
        [Required]
        [DisplayName("Closure (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("ChanceComponentClosureTemplate")]
        public decimal ChanceComponentClosure { get; set; }
        [Required]
        [DisplayName("Containment (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        [Range(0, double.MaxValue)]
        [UIHint("ChanceComponentContainmentTemplate")]
        public decimal ChanceComponentContainment { get; set; }

        [Required]
        [DisplayName("P90 Oil (USD/Bbl)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90NPVProfitabilityOilTemplate")]
        public decimal P90NPVProfitabilityOil { get; set; }

        [ScaffoldColumn(false)]
        public string P90NPVProfitabilityOilCurr { get; set; }

        [Required]
        [DisplayName("P50 Oil (USD/Bbl)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50NPVProfitabilityOilTemplate")]
        public decimal P50NPVProfitabilityOil { get; set; }

        [ScaffoldColumn(false)]
        public string P50NPVProfitabilityOilCurr { get; set; }

        [Required]
        [DisplayName("P10 Oil (USD/Bbl)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10NPVProfitabilityOilTemplate")]
        public decimal P10NPVProfitabilityOil { get; set; }

        [ScaffoldColumn(false)]
        public string P10NPVProfitabilityOilCurr { get; set; }

        [Required]
        [DisplayName("P90 Gas (USD/Mcf)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90NPVProfitabilityGasTemplate")]
        public decimal P90NPVProfitabilityGas { get; set; }

        [ScaffoldColumn(false)]
        public string P90NPVProfitabilityGasCurr { get; set; }

        [Required]
        [DisplayName("P50 Gas (USD/Mcf)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P50NPVProfitabilityGasTemplate")]
        public decimal P50NPVProfitabilityGas { get; set; }

        [ScaffoldColumn(false)]
        public string P50NPVProfitabilityGasCurr { get; set; }

        [Required]
        [DisplayName("P10 Gas (USD/Mcf)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P10NPVProfitabilityGasTemplate")]
        public decimal P10NPVProfitabilityGas { get; set; }

        [ScaffoldColumn(false)]
        public string P10NPVProfitabilityGasCurr { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
    }
}
