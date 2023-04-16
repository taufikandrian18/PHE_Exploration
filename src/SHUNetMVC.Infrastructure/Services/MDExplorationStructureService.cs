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
    public class MDExplorationStructureService : BaseCrudService<MDExplorationStructureDto, MDExplorationStructureWithAdditionalFields>, IMDExplorationStructureService
    {
        private readonly IMDExplorationStructureRepository _explorationStructureRepository;
        public MDExplorationStructureService(IMDExplorationStructureRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationStructureRepository = repo;
        }

        public async Task<string> GenerateNewID()
        {
            return await _explorationStructureRepository.GenerateNewID();
        }

        public async Task<LookupList> GetAdaptiveFilterListReport(string columnId, string usernameSession)
        {
            return await _explorationStructureRepository.GetAdaptiveFilterListReport(columnId, usernameSession);
        }

        public async Task<LookupList> GetAdaptiveFilterListView(string columnId, string usernameSession)
        {
            return await _explorationStructureRepository.GetAdaptiveFilterListView(columnId, usernameSession);
        }

        public List<MD_ExplorationStructure> GetByStructureName(string structureName)
        {
            return _explorationStructureRepository.GetByStructureName(structureName);
        }

        public Task<IEnumerable<MDExplorationStructureExcelDto>> GetExportExplorationStructureByStructureID(string structureID)
        {
            return _explorationStructureRepository.GetExportExplorationStructureByStructureID(structureID);
        }
    }
}
