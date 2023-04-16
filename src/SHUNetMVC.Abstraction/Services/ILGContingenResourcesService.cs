using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ILGContingenResourcesService : ICrudService<LGContingenResourcesDto, LGContingenResourcesDto>
    {
        List<LGContingenResourcesDto> GetAll();
        Task<LG_ContingentResources> GetContResourceTargetByStructureID(string structureID);
    }
}
