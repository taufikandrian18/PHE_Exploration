using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Request
{
    public class MDExplorationStructureFilter : PagedFilter
    {
        public string StructureName { get; set; }
        public string AssetName { get; set; }
        public string BasinName { get; set; }
        public string BlockName { get; set; }
    }
}
