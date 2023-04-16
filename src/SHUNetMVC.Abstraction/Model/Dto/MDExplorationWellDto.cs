using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDExplorationWellDto : BaseDtoAutoMapper<MD_ExplorationWell>
    {
        public string xWellID { get; set; }
        public string xWellName { get; set; }
        public string DrillingLocation { get; set; }
        public string RigTypeParID { get; set; }
        public string WellTypeParID { get; set; }
        public decimal BHLocationLatitude { get; set; }
        public decimal BHLocationLongitude { get; set; }

        public MDExplorationWellDto()
        {

        }

        public MDExplorationWellDto(MD_ExplorationWell entity) : base(entity)
        {

        }
    }
}
