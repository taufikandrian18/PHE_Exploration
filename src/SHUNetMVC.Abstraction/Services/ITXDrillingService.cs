using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXDrillingService : ICrudService<TXDrillingDto, TXDrillingDto>
    {
        List<TXDrillingDto> GetAll();
        Task Destroy(string xStructureID, string xWellID);
        void DestroyTarget(string structureID, string wellID);
        Task<List<TXDrillingDto>> GetDrillingByStructureID(string structureID);
        Task<IEnumerable<TXDrillingExcelDto>> GetExportDrillingByStructureID(string structureID);
    }
}
