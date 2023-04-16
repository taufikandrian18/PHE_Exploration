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
    public class MDExplorationBasinService : BaseCrudService<MDExplorationBasinDto, MDExplorationBasinDto>, IMDExplorationBasinService
    {
        private readonly IMDExplorationBasinRepository _explorationBasinRepository;
        public MDExplorationBasinService(IMDExplorationBasinRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationBasinRepository = repo;
        }
    }
}
