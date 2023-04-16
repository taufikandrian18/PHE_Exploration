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
    public interface ITXContingenResourcesService : ICrudService<TXContingenResourcesDto, TXContingenResourcesDto>
    {
        List<TXContingenResourcesDto> GetAll();
        Task Destroy(string structureID);
        void DestroyTarget(string structureID);
        Task<TX_ContingentResources> GetContResourceTargetByStructureID(string structureID);
        Task<IEnumerable<TXContResourcesExcelDto>> GetExportContResourceTargetByStructureID(string structureID);
    }
}
