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
    public class TXESDCDiscrepancyDto : BaseDtoAutoMapper<TX_ESDCDiscrepancy>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        [DisplayName("Low/Mid/High")]
        public string UncertaintyLevel { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFUMOilTemplate")]
        public Nullable<decimal> CFUMOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFUMCondensateTemplate")]
        public Nullable<decimal> CFUMCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFUMAssociatedTemplate")]
        public Nullable<decimal> CFUMAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFUMNonAssociatedTemplate")]
        public Nullable<decimal> CFUMNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFPPAOilTemplate")]
        public Nullable<decimal> CFPPAOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFPPACondensateTemplate")]
        public Nullable<decimal> CFPPACondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFPPAAssociatedTemplate")]
        public Nullable<decimal> CFPPAAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFPPANonAssociatedTemplate")]
        public Nullable<decimal> CFPPANonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFWIOilTemplate")]
        public Nullable<decimal> CFWIOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFWICondensateTemplate")]
        public Nullable<decimal> CFWICondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFWIAssociatedTemplate")]
        public Nullable<decimal> CFWIAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFWINonAssociatedTemplate")]
        public Nullable<decimal> CFWINonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFCOilTemplate")]
        public Nullable<decimal> CFCOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFCCondensateTemplate")]
        public Nullable<decimal> CFCCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFCAssociatedTemplate")]
        public Nullable<decimal> CFCAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CFCNonAssociatedTemplate")]
        public Nullable<decimal> CFCNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("UCOilTemplate")]
        public Nullable<decimal> UCOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("UCCondensateTemplate")]
        public Nullable<decimal> UCCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("UCAssociatedTemplate")]
        public Nullable<decimal> UCAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("UCNonAssociatedTemplate")]
        public Nullable<decimal> UCNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIOOilTemplate")]
        public Nullable<decimal> CIOOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIOCondensateTemplate")]
        public Nullable<decimal> CIOCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIOAssociatedTemplate")]
        public Nullable<decimal> CIOAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("CIONonAssociatedTemplate")]
        public Nullable<decimal> CIONonAssociated { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }
    }
}
