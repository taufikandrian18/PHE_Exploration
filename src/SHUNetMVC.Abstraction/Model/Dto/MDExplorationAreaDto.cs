using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDExplorationAreaDto : BaseDtoAutoMapper<MD_ExplorationArea>
    {
        public string xAreaID { get; set; }
        public string xAreaName { get; set; }
        public MDExplorationAreaDto()
        {

        }
        public MDExplorationAreaDto(MD_ExplorationArea entity) : base(entity)
        {

        }
    }
}
