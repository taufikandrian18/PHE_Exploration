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
    public class TXDrillingService : BaseCrudService<TXDrillingDto, TXDrillingDto>, ITXDrillingService
    {
        private readonly ITXDrillingRepository _repo;
        public TXDrillingService(ITXDrillingRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string xStructureID, string xWellID)
        {
            return _repo.Destroy(xStructureID, xWellID);
        }

        public void DestroyTarget(string structureID, string wellID)
        {
            _repo.DestroyTarget(structureID, wellID);
        }

        public List<TXDrillingDto> GetAll()
        {
            return _repo.GetAll();
        }

        public Task<List<TXDrillingDto>> GetDrillingByStructureID(string structureID)
        {
            return _repo.GetDrillingByStructureID(structureID);
        }

        public async Task<IEnumerable<TXDrillingExcelDto>> GetExportDrillingByStructureID(string structureID)
        {
            return await _repo.GetExportDrillingByStructureID(structureID);
        }
    }
}
