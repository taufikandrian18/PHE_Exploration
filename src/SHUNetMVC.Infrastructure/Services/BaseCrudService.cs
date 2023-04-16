using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.Helpers;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2010.Excel;
using Serilog;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;
using QuestPDF.Drawing;
using System.Collections.Generic;

namespace SHUNetMVC.Infrastructure.Services
{
    public class BaseCrudService<TDto, TGridModel> : ICrudService<TDto,TGridModel>
    {
        private readonly ICrudRepository<TDto, TGridModel> _repo;
        private readonly ILogger _logger;

        public BaseCrudService(ICrudRepository<TDto, TGridModel> repo, ILogger logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public Task<Paged<TGridModel>> GetPaged(GridParam param)
        {
            _logger.Information($"Get exploration structure list with filter page {param.FilterList.Page}");
            return _repo.GetPaged(param);
        }

        public Task<Paged<TGridModel>> GetPagedAll()
        {
            _logger.Information("Get exploration structure list");
            return _repo.GetPagedAll();
        }

        public Task<LookupList> GetAdaptiveFilterList(string columnId, string usernameSession)
        {
            _logger.Information($"Get adaptive filter list column {columnId}");
            return _repo.GetAdaptiveFilterList(columnId, usernameSession);
        }

        public async Task<XLWorkbook> ExportToExcel(GridParam param)
        {
            _logger.Information($"Get exploration structure list report");

            param.FilterList.Page = 1;
            param.FilterList.Size = 10000;
            //param.FilterList.OrderBy = "CreatedDate DESC";

            //var data = await _repo.GetPaged(param);
            var data = await _repo.GetPagedExcelESDC(param);
            return SpreadsheetGenerator.Generate("Exploration Structure", data, param.ColumnDefinitions, param.FilterList);
        }

        public async Task<XLWorkbook> ExportToExcelExplorationStructure(GridParam param)
        {
            _logger.Information($"Get exploration structure list report");

            param.FilterList.Page = 1;
            param.FilterList.Size = 10000;

            //var data = await _repo.GetPaged(param);
            //var data = await _repo.GetPagedExcelESDC(param);
            var data = await _repo.GetPagedProsResource(param);
            return SpreadsheetGenerator.GenerateExploration("ProsResourceReport", data.Items, param.ColumnDefinitions, param.FilterList);
        }

        public async Task<XLWorkbook> ExportToExcelRJPP(GridParam param)
        {
            _logger.Information($"Get RJPP list report");

            param.FilterList.Page = 1;
            param.FilterList.Size = 10000;

            //var data = await _repo.GetPaged(param);
            //var data = await _repo.GetPagedExcelESDC(param);
            var data = await _repo.GetPagedRJPP(param);
            return SpreadsheetGenerator.GenerateExplorationRJPP("RJPP", data.Items, param.ColumnDefinitions, param.FilterList);
        }

        public byte[] ExportToPDF(GridListModel gridList, string headerText, int[] tableHeaderSizes)
        {
            _logger.Information($"Get exploration structure list PDF report");

            PDFTableGenerator pdfTable = new PDFTableGenerator(gridList, headerText, tableHeaderSizes);
            return pdfTable.GeneratePdf();
        }

        public async Task Create(TDto model)
        {
            await _repo.Create(model);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<TDto> GetOne(string id)
        {
            return await _repo.GetOne(id);
        }

        public async Task Update(TDto model)
        {
            await _repo.Update(model);
        }

        public async Task<string> GetLookupText(string id)
        {
            return await _repo.GetLookupText(id);
        }

        public async Task<IEnumerable<TDto>> GetLookupListText(string id)
        {
            return await _repo.GetLookupListText(id);
        }

        public async Task<IEnumerable<TDto>> GetLookupListText(string id, string secondId)
        {
            return await _repo.GetLookupListText(id, secondId);
        }

        public Task<Paged<TGridModel>> GetPagedRoles(GridParam param)
        {
            _logger.Information($"Get exploration structure list with filter page {param.FilterList.Page}");
            return _repo.GetPagedRoles(param);
        }

        public Task<Paged<TGridModel>> GetPagedReport(GridParam param)
        {
            _logger.Information($"Get exploration structure list with filter page {param.FilterList.Page}");
            return _repo.GetPagedReport(param);
        }

        public Task<ExportExcelESDC> GetPagedExcelESDC(GridParam param)
        {
            _logger.Information("Get ESDC Excel list");
            return _repo.GetPagedExcelESDC(param);
        }

        public Task<Paged<ProsResourceExcelDto>> GetPagedProsResource(GridParam param)
        {
            _logger.Information($"Get Pros Resource Excel list with filter page {param.FilterList.Page}");
            return _repo.GetPagedProsResource(param);
        }

        public Task<Paged<ExploreRJPPExcelDto>> GetPagedRJPP(GridParam param)
        {
            _logger.Information($"Get RJPP Excel list with filter page {param.FilterList.Page}");
            return _repo.GetPagedRJPP(param);
        }
    }
}
