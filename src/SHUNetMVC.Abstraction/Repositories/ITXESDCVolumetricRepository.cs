using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface ITXESDCVolumetricRepository : ICrudRepository<TXESDCVolumetricDto, TXESDCVolumetricDto>
    {
        List<TXESDCVolumetricDto> GetAll();
        Task Destroy(string structureID, string uncertaintyLevel);
        Task<string> GenerateNewID();
        Task<TX_ESDCVolumetric> GetVolumetricTargetByStructureID(string structureID);
        Task<List<TXESDCVolumetricDto>> GetListTXESDCVolumetricByStructureID(string structureID);
    }
}
