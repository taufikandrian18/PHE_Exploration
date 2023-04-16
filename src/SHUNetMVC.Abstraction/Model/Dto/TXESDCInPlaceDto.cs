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
    public class TXESDCInPlaceDto : BaseDtoAutoMapper<TX_ESDCInPlace>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        [DisplayName("Low/Mid/High")]
        public string UncertaintyLevel { get; set; }
        [Required]
        [DisplayName("IOIP")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90IOIPTemplate")]
        public Nullable<decimal> P90IOIP { get; set; }
        [Required]
        [DisplayName("IGIP")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("P90IGIPTemplate")]
        public Nullable<decimal> P90IGIP { get; set; }
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
