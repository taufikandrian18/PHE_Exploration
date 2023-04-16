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
    public class TXESDCProductionDto : BaseDtoAutoMapper<TX_ESDCProd>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPPrevOilTemplate")]
        public Nullable<decimal> GCPPrevOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPPrevCondensateTemplate")]
        public Nullable<decimal> GCPPrevCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPPrevAssociatedTemplate")]
        public Nullable<decimal> GCPPrevAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPPrevNonAssociatedTemplate")]
        public Nullable<decimal> GCPPrevNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPPrevOilTemplate")]
        public Nullable<decimal> SCPPrevOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPPrevCondensateTemplate")]
        public Nullable<decimal> SCPPrevCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPPrevAssociatedTemplate")]
        public Nullable<decimal> SCPPrevAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPPrevNonAssociatedTemplate")]
        public Nullable<decimal> SCPPrevNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPOilTemplate")]
        public Nullable<decimal> GCPOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPCondensateTemplate")]
        public Nullable<decimal> GCPCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPAssociatedTemplate")]
        public Nullable<decimal> GCPAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("GCPNonAssociatedTemplate")]
        public Nullable<decimal> GCPNonAssociated { get; set; }
        [Required]
        [DisplayName("Oil")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPOilTemplate")]
        public Nullable<decimal> SCPOil { get; set; }
        [Required]
        [DisplayName("Condensate")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPCondensateTemplate")]
        public Nullable<decimal> SCPCondensate { get; set; }
        [Required]
        [DisplayName("Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPAssociatedTemplate")]
        public Nullable<decimal> SCPAssociated { get; set; }
        [Required]
        [DisplayName("Non Associated Gas")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("SCPNonAssociatedTemplate")]
        public Nullable<decimal> SCPNonAssociated { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }

        public TXESDCProductionDto()
        {

        }

        public TXESDCProductionDto(TX_ESDCProd entity) : base(entity)
        {

        }
    }
}
