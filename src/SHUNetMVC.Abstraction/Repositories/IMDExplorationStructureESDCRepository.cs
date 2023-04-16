using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface IMDExplorationStructureESDCRepository : ICrudRepository<MDExplorationStructureESDCDto, MDExplorationStructureWithAdditionalFields>
    {
        List<MD_ExplorationStructure> GetByStructureName(string structureName);
        Task<string> GenerateNewID();
        Task<LookupList> GetAdaptiveFilterListESDC(string columnId, string usernameSession);
    }
}
