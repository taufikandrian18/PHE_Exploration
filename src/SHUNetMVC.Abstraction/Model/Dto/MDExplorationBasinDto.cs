using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class MDExplorationBasinDto : BaseDtoAutoMapper<MD_ExplorationBasin>
    {
        public string BasinID { get; set; }
        public string BasinName { get; set; }
        public MDExplorationBasinDto()
        {

        }
        public MDExplorationBasinDto(MD_ExplorationBasin entity) : base(entity)
        {

        }
    }
}
