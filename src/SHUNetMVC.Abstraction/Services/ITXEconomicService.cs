using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXEconomicService : ICrudService<TXEconomicDto, TXEconomicDto>
    {
        List<TXEconomicDto> GetAll();
        Task Destroy(string structureID);
        Task<IEnumerable<TXEconomicExcelDto>> GetExportEconomicByStructureID(string structureID);
    }
}
