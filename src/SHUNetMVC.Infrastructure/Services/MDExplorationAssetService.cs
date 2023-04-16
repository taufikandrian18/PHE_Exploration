using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;

namespace SHUNetMVC.Infrastructure.Services
{
    public class MDExplorationAssetService : BaseCrudService<MDExplorationAssetDto, MDExplorationAssetDto>, IMDExplorationAssetService
    {
        private readonly IMDExplorationAssetRepository _explorationAssetRepo;
        public MDExplorationAssetService(IMDExplorationAssetRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationAssetRepo = repo;
        }
    }
}
