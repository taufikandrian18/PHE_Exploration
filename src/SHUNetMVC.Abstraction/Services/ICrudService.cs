using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using ClosedXML.Excel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SHUNetMVC.Abstraction.Services
{
    public interface ICrudService<TEntity, TGridModel>
    {
        Task<TEntity> GetOne(string id);
        Task Update(TEntity model);
        Task Delete(int id);
        Task Create(TEntity model);
        Task<string> GetLookupText(string id);
        Task<IEnumerable<TEntity>> GetLookupListText(string id);
        Task<IEnumerable<TEntity>> GetLookupListText(string id, string secondId);
        Task<Paged<TGridModel>> GetPaged(GridParam param);
        Task<Paged<ProsResourceExcelDto>> GetPagedProsResource(GridParam param);
        Task<Paged<ExploreRJPPExcelDto>> GetPagedRJPP(GridParam param);
        Task<Paged<TGridModel>> GetPagedAll();
        Task<Paged<TGridModel>> GetPagedRoles(GridParam param);
        Task<Paged<TGridModel>> GetPagedReport(GridParam param);
        Task<ExportExcelESDC> GetPagedExcelESDC(GridParam param);
        Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession);
        Task<XLWorkbook> ExportToExcel(GridParam param);
        Task<XLWorkbook> ExportToExcelRJPP(GridParam param);
        Task<XLWorkbook> ExportToExcelExplorationStructure(GridParam param);
        byte[] ExportToPDF(GridListModel model, string headerText, int[] tableHeaderSizes);
    }
}
