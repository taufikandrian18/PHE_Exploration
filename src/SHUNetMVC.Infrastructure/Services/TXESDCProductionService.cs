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
    public class TXESDCProductionService : BaseCrudService<TXESDCProductionDto, TXESDCProductionDto>, ITXESDCProductionService
    {
        private readonly ITXESDCProductionRepository _repo;
        public TXESDCProductionService(ITXESDCProductionRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }
        public Task Destroy(string structureID)
        {
            return _repo.Destroy(structureID);
        }

        public void DestroyTarget(string structureID)
        {
            _repo.DestroyTarget(structureID);
        }

        public List<TXESDCProductionDto> GetAll()
        {
            return _repo.GetAll();
        }
        public async Task<TX_ESDCProd> GetProdTargetByStructureID(string structureID)
        {
            return await _repo.GetProdTargetByStructureID(structureID);
        }

        public async Task<List<TXESDCProductionDto>> GetListTXESDCProductionByStructureID(string structureID)
        {
            return await _repo.GetListTXESDCProductionByStructureID(structureID);
        }
    }
}
