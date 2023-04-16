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
    public class MDExplorationBlockPartnerDto : BaseDtoAutoMapper<MD_ExplorationBlockPartner>
    {
        public MDExplorationBlockPartnerDto()
        {

        }
        public MDExplorationBlockPartnerDto(MD_ExplorationBlockPartner entity) : base(entity)
        {

        }

        [ScaffoldColumn(false)]
        public string PartnerID { get; set; }
        [Required]
        [Remote("IsPartnerName_Available", "Economic")]
        public string PartnerName { get; set; }
        [Required]
        [DisplayName("PI (%)")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}%")]
        public decimal PI { get; set; }
        public string OperatorshipStatusParID { get; set; }
        [ScaffoldColumn(false)]
        public string xBlockID { get; set; }
        [ScaffoldColumn(false)]
        public Nullable<System.DateTime> EffectiveDate { get; set; }
    }
}
