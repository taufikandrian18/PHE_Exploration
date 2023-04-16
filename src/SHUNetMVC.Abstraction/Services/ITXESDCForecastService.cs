using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ITXESDCForecastService : ICrudService<TXESDCForecastDto, TXESDCForecastDto>
    {
        List<TXESDCForecastDto> GetAll();
        Task Destroy(string structureID, int year);
        Task<string> GenerateNewID();
        Task<List<TXESDCForecastDto>> GetForecastByStructureID(string structureID);
        Task<TX_ESDCForecast> GetForecastTargetByStructureID(string structureID);
    }
}
