using ASPNetMVC.Abstraction.Model.Entities;
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
    public class TXESDCForecastDto : BaseDtoAutoMapper<TX_ESDCForecast>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        public int Year { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("TPFOilTemplate")]
        public decimal TPFOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("TPFCondensateTemplate")]
        public decimal TPFCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("TPFAssociatedTemplate")]
        public decimal TPFAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("TPFNonAssociatedTemplate")]
        public decimal TPFNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SFOilTemplate")]
        public decimal SFOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SFCondensateTemplate")]
        public decimal SFCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SFAssociatedTemplate")]
        public decimal SFAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SFNonAssociatedTemplate")]
        public decimal SFNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIOOilTemplate")]
        public decimal CIOOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIOCondensateTemplate")]
        public decimal CIOCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIOAssociatedTemplate")]
        public decimal CIOAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIONonAssociatedTemplate")]
        public decimal CIONonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("LPOilTemplate")]
        public decimal LPOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("LPCondensateTemplate")]
        public decimal LPCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("LPAssociatedTemplate")]
        public decimal LPAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("LPNonAssociatedTemplate")]
        public decimal LPNonAssociated { get; set; }
        [Required]
        [DisplayName("Average Gross Heat")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("AverageGrossHeatTemplate")]
        public decimal AverageGrossHeat { get; set; }
        public string Remarks { get; set; }
    }
}
