using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface ILGExplorationStructureRepository : ICrudRepository<LGExplorationStructureDto, LGExplorationStructureDto>
    {
        List<LG_ExplorationStructure> GetByStructureName(string structureName);
        Task<string> GenerateNewID();
        List<LG_ExplorationStructure> GetByStructureID(string structureID);
    }
}
