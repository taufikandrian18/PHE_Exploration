using ASPNetMVC.Abstraction.Model.Entities;
using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Services
{
    public class TXESDCForecastService : BaseCrudService<TXESDCForecastDto, TXESDCForecastDto>, ITXESDCForecastService
    {
        private readonly ITXESDCForecastRepository _repo;
        public TXESDCForecastService(ITXESDCForecastRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string structureID, int year)
        {
            return _repo.Destroy(structureID, year);
        }

        public async Task<string> GenerateNewID()
        {
            return await _repo.GenerateNewID();
        }

        public List<TXESDCForecastDto> GetAll()
        {
            return _repo.GetAll();
        }
        public async Task<TX_ESDCForecast> GetForecastTargetByStructureID(string structureID)
        {
            return await _repo.GetForecastTargetByStructureID(structureID);
        }
        public async Task<List<TXESDCForecastDto>> GetForecastByStructureID(string structureID)
        {
            return await _repo.GetForecastByStructureID(structureID);
        }
    }
}
