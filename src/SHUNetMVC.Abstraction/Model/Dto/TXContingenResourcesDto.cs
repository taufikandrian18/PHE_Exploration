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
    public class TXContingenResourcesDto : BaseDtoAutoMapper<TX_ContingentResources>
    {
        [ScaffoldColumn(false)]
        public string xStructureID { get; set; }
        [ScaffoldColumn(false)]
        public string ExplorationStructureName { get; set; }
        [DisplayName("1C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("C1COilTemplate")]
        public decimal C1COil { get; set; }
        [ScaffoldColumn(false)]
        public string C1COilUoM { get; set; }
        [DisplayName("2C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("C2COilTemplate")]
        public decimal C2COil { get; set; }
        [ScaffoldColumn(false)]
        public string C2COilUoM { get; set; }
        [DisplayName("3C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("C3COilTemplate")]
        public decimal C3COil { get; set; }
        [ScaffoldColumn(false)]
        public string C3COilUoM { get; set; }
        [DisplayName("1C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("C1CGasTemplate")]
        public decimal C1CGas { get; set; }
        [ScaffoldColumn(false)]
        public string C1CGasUoM { get; set; }
        [DisplayName("2C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("C2CGasTemplate")]
        public decimal C2CGas { get; set; }
        [ScaffoldColumn(false)]
        public string C2CGasUoM { get; set; }
        [DisplayName("3C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        [UIHint("C3CGasTemplate")]
        public decimal C3CGas { get; set; }
        [ScaffoldColumn(false)]
        public string C3CGasUoM { get; set; }
        [DisplayName("1C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal C1CTotal { get; set; }
        [ScaffoldColumn(false)]
        public string C1CTotalUoM { get; set; }
        [DisplayName("2C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal C2CTotal { get; set; }
        [ScaffoldColumn(false)]
        public string C2CTotalUoM { get; set; }
        [DisplayName("3C")]
        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N}")]
        [Range(0, double.MaxValue)]
        public decimal C3CTotal { get; set; }
        [ScaffoldColumn(false)]
        public string C3CTotalUoM { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public TXContingenResourcesDto()
        {

        }

        public TXContingenResourcesDto(TX_ContingentResources entity) : base(entity)
        {

        }
    }
}
