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
    public class MDExplorationBlockPartnerService : BaseCrudService<MDExplorationBlockPartnerDto, MDExplorationBlockPartnerDto>, IMDExplorationBlockPartnerService
    {
        private readonly IMDExplorationBlockPartnerRepository _explorationBlockPartnerRepository;
        public MDExplorationBlockPartnerService(IMDExplorationBlockPartnerRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationBlockPartnerRepository = repo;
        }
    }
}
