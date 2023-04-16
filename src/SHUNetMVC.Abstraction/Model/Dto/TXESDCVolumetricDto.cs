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
    public class TXESDCVolumetricDto : BaseDtoAutoMapper<TX_ESDCVolumetric>
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
        [UIHint("GRRPrevOilTemplate")]
        public Nullable<decimal> GRRPrevOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GRRPrevCondensateTemplate")]
        public Nullable<decimal> GRRPrevCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GRRPrevAssociatedTemplate")]
        public Nullable<decimal> GRRPrevAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GRRPrevNonAssociatedTemplate")]
        public Nullable<decimal> GRRPrevNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesPrevOilTemplate")]
        public Nullable<decimal> ReservesPrevOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesPrevCondensateTemplate")]
        public Nullable<decimal> ReservesPrevCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesPrevAssociatedTemplate")]
        public Nullable<decimal> ReservesPrevAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesPrevNonAssociatedTemplate")]
        public Nullable<decimal> ReservesPrevNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GOIOilTemplate")]
        public Nullable<decimal> GOIOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GOICondensateTemplate")]
        public Nullable<decimal> GOICondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GOIAssociatedTemplate")]
        public Nullable<decimal> GOIAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GOINonAssociatedTemplate")]
        public Nullable<decimal> GOINonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesOilTemplate")]
        public Nullable<decimal> ReservesOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesCondensateTemplate")]
        public Nullable<decimal> ReservesCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesAssociatedTemplate")]
        public Nullable<decimal> ReservesAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("ReservesNonAssociatedTemplate")]
        public Nullable<decimal> ReservesNonAssociated { get; set; }
        public string Remarks { get; set; }
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
