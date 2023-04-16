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
    public class LGExplorationStructureService : BaseCrudService<LGExplorationStructureDto, LGExplorationStructureDto>, ILGExplorationStructureService
    {
        private readonly ILGExplorationStructureRepository _explorationStructureRepository;
        public LGExplorationStructureService(ILGExplorationStructureRepository repo, ILogger logger) : base(repo, logger)
        {
            _explorationStructureRepository = repo;
        }

        public async Task<string> GenerateNewID()
        {
            return await _explorationStructureRepository.GenerateNewID();
        }

        public List<LG_ExplorationStructure> GetByStructureName(string structureName)
        {
            return _explorationStructureRepository.GetByStructureName(structureName);
        }
        public List<LG_ExplorationStructure> GetByStructureID(string structureID)
        {
            return _explorationStructureRepository.GetByStructureID(structureID);
        }
    }
}
