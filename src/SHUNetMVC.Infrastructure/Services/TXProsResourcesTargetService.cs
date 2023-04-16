using ASPNetMVC.Abstraction.Model.Entities;
using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
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
    public class TXProsResourcesTargetService : BaseCrudService<TXProsResourcesTargetDto, TXPropectiveResourceTargetWthFields>, ITXProsResourcesTargetService
    {
        private readonly ITXProsResourcesTargetRepository _repo;
        public TXProsResourcesTargetService(ITXProsResourcesTargetRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string targetID)
        {
            return _repo.Destroy(targetID);
        }

        public void DestroyTarget(string targetID)
        {
            _repo.DestroyTarget(targetID);
        }

        public async Task<string> GenerateNewID()
        {
            return await _repo.GenerateNewID();
        }

        public List<TXProsResourcesTargetDto> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<IEnumerable<TXProsResourcesTargetExcelDto>> GetExportProsResourceTargetByStructureID(string structureID)
        {
            return await _repo.GetExportProsResourceTargetByStructureID(structureID);
        }

        public async Task<List<TXProsResourcesTargetDto>> GetProsResourceTargetByStructureID(string structureID)
        {
            return await _repo.GetProsResourceTargetByStructureID(structureID);
        }
    }
}
