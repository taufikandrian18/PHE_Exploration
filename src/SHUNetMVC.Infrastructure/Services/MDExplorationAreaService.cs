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
    public class MDExplorationAreaService : BaseCrudService<MDExplorationAreaDto, MDExplorationAreaDto>, IMDExplorationAreaService
    {
        private readonly IMDExplorationAreaRepository _explorationAreaRepository;
        public MDExplorationAreaService(IMDExplorationAreaRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationAreaRepository = repo;
        }
    }
}
