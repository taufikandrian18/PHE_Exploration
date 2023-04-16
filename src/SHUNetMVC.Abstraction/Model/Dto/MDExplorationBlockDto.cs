using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDExplorationBlockDto : BaseDtoAutoMapper<MD_ExplorationBlock>
    {
        public MDExplorationBlockDto()
        {

        }
        public MDExplorationBlockDto(MD_ExplorationBlock entity) : base(entity)
        {

        }

        public string xBlockID { get; set; }
        public string xBlockName { get; set; }
        public Nullable<System.DateTime> AwardDate { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public string xBlockStatusParID { get; set; }
        public string OperatorshipStatusParID { get; set; }
        public string OperatorName { get; set; }
        public string CountriesID { get; set; }
    }
}
