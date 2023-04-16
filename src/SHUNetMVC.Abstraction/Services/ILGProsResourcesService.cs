using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ILGProsResourcesService : ICrudService<LGProsResourceDto, LGProsResourceDto>
    {
        List<LGProsResourceDto> GetAll();
        Task Destroy(string structureID);
        void DestroyTarget(string structureID);
    }
}
