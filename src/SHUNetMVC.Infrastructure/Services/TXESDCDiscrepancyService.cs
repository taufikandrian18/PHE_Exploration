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
    public class TXESDCDiscrepancyService : BaseCrudService<TXESDCDiscrepancyDto, TXESDCDiscrepancyDto>, ITXESDCDiscrepancyService
    {
        private readonly ITXESDCDiscrepancyRepository _repo;
        public TXESDCDiscrepancyService(ITXESDCDiscrepancyRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string structureID, string uncertaintyLevel)
        {
            return _repo.Destroy(structureID, uncertaintyLevel);
        }

        public async Task<string> GenerateNewID()
        {
            return await _repo.GenerateNewID();
        }

        public List<TXESDCDiscrepancyDto> GetAll()
        {
            return _repo.GetAll();
        }
        public Task<TX_ESDCDiscrepancy> GetDiscrepancyTargetByStructureID(string structureID)
        {
            return _repo.GetDiscrepancyTargetByStructureID(structureID);
        }

        public async Task<List<TXESDCDiscrepancyDto>> GetListTXESDCDiscrepancyByStructureID(string structureID)
        {
            return await _repo.GetListTXESDCDiscrepancyByStructureID(structureID);
        }
    }
}
