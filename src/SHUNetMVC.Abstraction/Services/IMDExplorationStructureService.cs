using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface IMDExplorationStructureService : ICrudService<MDExplorationStructureDto, MDExplorationStructureWithAdditionalFields>
    {
        List<MD_ExplorationStructure> GetByStructureName(string structureName);
        Task<string> GenerateNewID();
        Task<IEnumerable<MDExplorationStructureExcelDto>> GetExportExplorationStructureByStructureID(string structureID);
        Task<LookupList> GetAdaptiveFilterListView(string columnId, string usernameSession);
        Task<LookupList> GetAdaptiveFilterListReport(string columnId, string usernameSession);
    }
}
