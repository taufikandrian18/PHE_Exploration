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
    public class TXProsResourcesService : BaseCrudService<TXProsResourceDto, TXProsResourceDto>, ITXProsResourcesService
    {
        private readonly ITXProsResourcesRepository _repo;
        public TXProsResourcesService(ITXProsResourcesRepository repo, ILogger logger) : base(repo, logger)
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

        public List<TXProsResourceDto> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<TXProsResourcesExcelDto>> GetExportProsResourceByStructureID(string structureID)
        {
            return await _repo.GetExportProsResourceByStructureID(structureID);
        }
    }
}
