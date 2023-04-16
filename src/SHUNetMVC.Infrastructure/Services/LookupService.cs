using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using Serilog;

namespace SHUNetMVC.Infrastructure.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILookupRepository _repo;
        private readonly ILogger _logger;

        public LookupService(ILookupRepository repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }
 

        public LookupList GetExplorationAssets()
        {
            return _repo.GetExplorationAssets();
        }

        public LookupList GetExplorationBlocks()
        {
            return _repo.GetExplorationBlocks();
        }
        public LookupList GetExplorationBasins()
        {
            return _repo.GetExplorationBasins();
        }
    }
}
