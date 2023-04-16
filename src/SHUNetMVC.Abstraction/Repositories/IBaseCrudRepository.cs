using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface ICrudRepository<TDto, TGridModel>
    {
        Task Create(TDto model);
        Task Update(TDto model);
        Task Delete(int id);
        Task<Paged<TGridModel>> GetPaged(GridParam param);
        Task<Paged<ProsResourceExcelDto>> GetPagedProsResource(GridParam param);
        Task<Paged<ExploreRJPPExcelDto>> GetPagedRJPP(GridParam param);
        Task<Paged<TGridModel>> GetPagedAll();
        Task<Paged<TGridModel>> GetPagedRoles(GridParam param);
        Task<Paged<TGridModel>> GetPagedReport(GridParam param);
        Task<ExportExcelESDC> GetPagedExcelESDC(GridParam param);
        Task<TDto> GetOne(string id);
        Task<string> GetLookupText(string id);
        Task<IEnumerable<TDto>> GetLookupListText(string id);
        Task<IEnumerable<TDto>> GetLookupListText(string id, string secondId);
        Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession);
    }
}
