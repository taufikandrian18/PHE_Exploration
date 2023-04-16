using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Response
{
    public class MDExplorationStructureWithAdditionalFields
    {
        public string xStructureID { get; set; }
        public string xStructureName { get; set; }
        public string ParamValue1Text { get; set; }
        public string ZonaID { get; set; }
        public string ZonaName { get; set; }
        public string BasinName { get; set; }
        public string RegionalID { get; set; }
        public string RegionalName { get; set; }
        public string xBlockName { get; set; }
        public string CountriesID { get; set; }
        public int RKAPFiscalYear { get; set; }
        public string StatusData { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
