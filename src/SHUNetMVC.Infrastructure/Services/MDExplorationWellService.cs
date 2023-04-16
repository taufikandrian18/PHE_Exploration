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
    public class MDExplorationWellService : BaseCrudService<MDExplorationWellDto, MDExplorationWellDto>, IMDExplorationWellService
    {
        private readonly IMDExplorationWellRepository _explorationWellRepository;
        public MDExplorationWellService(IMDExplorationWellRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationWellRepository = repo;
        }

        public Task Destroy(string wellID)
        {
            return _explorationWellRepository.Destroy(wellID);
        }

        public void DestroyWell(string wellId)
        {
            _explorationWellRepository.DestroyWell(wellId);
        }

        public async Task<string> GenerateNewID()
        {
            return await _explorationWellRepository.GenerateNewID();
        }
    }
}
