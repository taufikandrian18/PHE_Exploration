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
    public class TXESDCVolumetricService : BaseCrudService<TXESDCVolumetricDto, TXESDCVolumetricDto>, ITXESDCVolumetricService
    {
        private readonly ITXESDCVolumetricRepository _repo;
        public TXESDCVolumetricService(ITXESDCVolumetricRepository repo, ILogger logger) : base(repo, logger)
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

        public List<TXESDCVolumetricDto> GetAll()
        {
            return _repo.GetAll();
        }
        public async Task<TX_ESDCVolumetric> GetVolumetricTargetByStructureID(string structureID)
        {
            return await _repo.GetVolumetricTargetByStructureID(structureID);
        }

        public async Task<List<TXESDCVolumetricDto>> GetListTXESDCVolumetricByStructureID(string structureID)
        {
            return await _repo.GetListTXESDCVolumetricByStructureID(structureID);
        }
    }
}
