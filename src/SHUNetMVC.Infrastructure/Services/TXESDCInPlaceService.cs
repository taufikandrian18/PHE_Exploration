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
    public class TXESDCInPlaceService : BaseCrudService<TXESDCInPlaceDto, TXESDCInPlaceDto>, ITXESDCInPlaceService
    {
        private readonly ITXESDCInPlaceRepository _repo;
        public TXESDCInPlaceService(ITXESDCInPlaceRepository repo, ILogger logger) : base(repo, logger)
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

        public List<TXESDCInPlaceDto> GetAll()
        {
            return _repo.GetAll();
        }
        public Task<TX_ESDCInPlace> GetInPlaceTargetByStructureID(string structureID)
        {
            return _repo.GetInPlaceTargetByStructureID(structureID);
        }

        public async Task<List<TXESDCInPlaceDto>> GetListTXESDCInPlaceByStructureID(string structureID)
        {
            return await _repo.GetListTXESDCInPlaceByStructureID(structureID);
        }
    }
}
