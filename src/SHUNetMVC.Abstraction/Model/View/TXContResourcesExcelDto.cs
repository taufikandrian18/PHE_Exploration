using System;
using System.Collections.Generic;   
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class TXContResourcesExcelDto
    {
        public string xStructureID { get; set; }
        public string xStructureName { get; set; }
        public Nullable<decimal> C1COil { get; set; }
        public string C1COilUoM { get; set; }
        public Nullable<decimal> C2COil { get; set; }
        public string C2COilUoM { get; set; }
        public Nullable<decimal> C3COil { get; set; }
        public string C3COilUoM { get; set; }
        public Nullable<decimal> C1CGas { get; set; }
        public string C1CGasUoM { get; set; }
        public Nullable<decimal> C2CGas { get; set; }
        public string C2CGasUoM { get; set; }
        public Nullable<decimal> C3CGas { get; set; }
        public string C3CGasUoM { get; set; }
        public Nullable<decimal> C1CTotal { get; set; }
        public string C1CTotalUoM { get; set; }
        public Nullable<decimal> C2CTotal { get; set; }
        public string C2CTotalUoM { get; set; }
        public Nullable<decimal> C3CTotal { get; set; }
        public string C3CTotalUoM { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
