using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXESDCProductionService : ICrudService<TXESDCProductionDto, TXESDCProductionDto>
    {
        List<TXESDCProductionDto> GetAll();
        Task Destroy(string structureID);
        void DestroyTarget(string structureID);
        Task<TX_ESDCProd> GetProdTargetByStructureID(string structureID);
        Task<List<TXESDCProductionDto>> GetListTXESDCProductionByStructureID(string structureID);
    }
}
