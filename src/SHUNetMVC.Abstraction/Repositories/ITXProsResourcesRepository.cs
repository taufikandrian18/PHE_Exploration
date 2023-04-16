using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface ITXProsResourcesRepository : ICrudRepository<TXProsResourceDto, TXProsResourceDto>
    {
        List<TXProsResourceDto> GetAll();
        Task Destroy(string structureID);
        void DestroyTarget(string structureID);
        Task<IEnumerable<TXProsResourcesExcelDto>> GetExportProsResourceByStructureID(string structureID);
    }
}
