using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class MDExplorationStructureExcelDto
    {
        public string xStructureID { get; set; }
        public string xStructureName { get; set; }
        public int EffectiveYear { get; set; }
        public string xStructureStatusName { get; set; }
        public string SingleOrMultiName { get; set; }
        public string ExplorationTypeName { get; set; }
        public string SubholdingName { get; set; }
        public string BasinName { get; set; }
        public string RegionalName { get; set; }
        public string ZonaName { get; set; }
        public string APHName { get; set; }
        public string xAssetName { get; set; }
        public string xBlockName { get; set; }
        public string xAreaName { get; set; }
        public string UDClassificationName { get; set; }
        public string UDSubClassificationName { get; set; }
        public string UDSubTypeName { get; set; }
        public string ExplorationAreaParID { get; set; }
        public string CountriesID { get; set; }
        public string Play { get; set; }
        public string StatusData { get; set; }
    }
}
