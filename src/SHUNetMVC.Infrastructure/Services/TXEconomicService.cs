using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Services
{
    public class TXEconomicService : BaseCrudService<TXEconomicDto, TXEconomicDto>, ITXEconomicService
    {
        private readonly ITXEconomicRepository _repo;
        public TXEconomicService(ITXEconomicRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string structureID)
        {
            return _repo.Destroy(structureID);
        }

        public List<TXEconomicDto> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<TXEconomicExcelDto>> GetExportEconomicByStructureID(string structureID)
        {
            return await _repo.GetExportEconomicByStructureID(structureID);
        }
    }
}
