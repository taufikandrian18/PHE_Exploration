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
    public class MDExplorationStructureESDCService : BaseCrudService<MDExplorationStructureESDCDto, MDExplorationStructureWithAdditionalFields>, IMDExplorationStructureESDCService
    {
        private readonly IMDExplorationStructureESDCRepository _explorationStructureRepository;
        public MDExplorationStructureESDCService(IMDExplorationStructureESDCRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationStructureRepository = repo;
        }

        public async Task<string> GenerateNewID()
        {
            return await _explorationStructureRepository.GenerateNewID();
        }

        public async Task<LookupList> GetAdaptiveFilterListESDC(string columnId, string usernameSession)
        {
            return await _explorationStructureRepository.GetAdaptiveFilterListESDC(columnId, usernameSession);
        }

        public List<MD_ExplorationStructure> GetByStructureName(string structureName)
        {
            return _explorationStructureRepository.GetByStructureName(structureName);
        }
    }
}
