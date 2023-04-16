using ASPNetMVC.Abstraction.Model.Entities;
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
    public class TXContingenResourcesService : BaseCrudService<TXContingenResourcesDto, TXContingenResourcesDto>, ITXContingenResourcesService
    {
        private readonly ITXContingenResourcesRepository _repo;
        public TXContingenResourcesService(ITXContingenResourcesRepository repo, ILogger logger) : base(repo, logger)
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

        public List<TXContingenResourcesDto> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<TX_ContingentResources> GetContResourceTargetByStructureID(string structureID)
        {
            return await _repo.GetContResourceTargetByStructureID(structureID);
        }

        public async Task<IEnumerable<TXContResourcesExcelDto>> GetExportContResourceTargetByStructureID(string structureID)
        {
            return await _repo.GetExportContResourceTargetByStructureID(structureID);
        }
    }
}
