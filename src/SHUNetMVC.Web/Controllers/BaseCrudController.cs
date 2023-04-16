using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.Services;
using SHUNetMVC.Infrastructure.Validators;
using SHUNetMVC.Web.Extensions;
using ClosedXML.Extensions;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Office2016.Excel;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Repositories;
using System.Security.Claims;

namespace SHUNetMVC.Web.Controllers
{
    public abstract class BaseCrudController<TDto, TGridModel> : Controller
    {

        protected abstract FormDefinition DefineForm(FormState formState);
        protected abstract FormDefinition DefineFormView(FormState formState, string paramID);
        protected abstract List<ColumnDefinition> DefineGrid();
        protected abstract List<ColumnDefinition> DefineGridProsResource();

        protected abstract List<ColumnDefinition> DefineGridRJPP();
        protected abstract Task<ExportExcelExploration> GetDataExportExcel(string xStructureID);

        public abstract Task<ActionResult> Create(TDto model);
        public abstract Task<ActionResult> Edit(TDto model);


        protected ICrudService<TDto, TGridModel> _crudService;
        protected ILookupService _lookupService;
        protected IUserService _userService;
        protected IHRISDevOrgUnitHierarchyService _serviceHris;
        protected HttpContextBase _httpContext;
        public BaseCrudController(ICrudService<TDto, TGridModel> crudSvc, ILookupService lookupSvc, IUserService userService, IHRISDevOrgUnitHierarchyService serviceHris, HttpContextBase httpContext)
        {
            _crudService = crudSvc;
            _lookupService = lookupSvc;
            _userService = userService;
            _serviceHris = serviceHris;
            _httpContext = httpContext;
        }
        public ActionResult Index()
        {
            var username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var model = new CrudPage
            {
                Id = "ExplorationStructure",
                Title = "Pencatatan Sumber Daya",
                SubTitle = "This is the list of Exploration Structure",
                UsernameSession = username,
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "[xStructureID] desc",
                        Page = 1,
                        Size = 20
                    },
                    UsernameSession = username
                }
            };

            return View(model);
        }

        /// <summary>
        /// Render Grid (reload, filter, sorting)
        /// </summary>
        /// <param name="filterList"></param>
        /// <returns></returns>
        public PartialViewResult GridList(FilterList filterList)
        {
            var hrisObj = "SHU";
            var username = "";
            if (filterList.UsernameSession == null)
            {
                username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                username = filterList.UsernameSession;
            }

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            if (filterList.GridId == "ExplorationStructureController_grid")
            {
                List<ColumnDefinition> columnDefinitions = DefineGrid();
                var task = Task.Run(async () => await _crudService.GetPaged(new GridParam
                {
                    ColumnDefinitions = columnDefinitions,
                    FilterList = filterList,
                    HrisRegObj = hrisObj
                }));

                var ret = task.Result;
                filterList.TotalItems = ret.TotalItems;

                GridListModel model = new GridListModel
                {
                    GridId = this.GetType().Name + "_grid",
                    GridAttr = "creator",
                    FilterList = filterList,
                    ColumnDefinitions = columnDefinitions,
                    UsernameSession = username
                };
                model.FillRows(ret.Items);
                return PartialView("Component/Grid/GridList", model);
            }
            else if(filterList.GridId == "ESDCController_grid")
            {
                List<ColumnDefinition> columnDefinitions = DefineGrid();
                var task = Task.Run(async () => await _crudService.GetPaged(new GridParam
                {
                    ColumnDefinitions = columnDefinitions,
                    FilterList = filterList,
                    HrisRegObj = hrisObj
                }));

                var ret = task.Result;
                filterList.TotalItems = ret.TotalItems;

                GridListModel model = new GridListModel
                {
                    GridId = this.GetType().Name + "_grid",
                    GridAttr = "creator",
                    FilterList = filterList,
                    ColumnDefinitions = columnDefinitions,
                    UsernameSession = username
                };
                model.FillRows(ret.Items);
                return PartialView("Component/Grid/GridList", model);
            }
            else if (filterList.GridId == "ExplorationStructureControllerReport_grid")
            {
                List<ColumnDefinition> columnDefinitions = DefineGrid();
                var task = Task.Run(async () => await _crudService.GetPagedReport(new GridParam
                {
                    ColumnDefinitions = columnDefinitions,
                    FilterList = filterList,
                    HrisRegObj = hrisObj
                }));

                var ret = task.Result;
                filterList.TotalItems = ret.TotalItems;

                GridListModel model = new GridListModel
                {
                    GridId = this.GetType().Name + "Report" + "_grid",
                    GridAttr = "reporter",
                    FilterList = filterList,
                    ColumnDefinitions = columnDefinitions
                };
                model.FillRows(ret.Items);
                return PartialView("Component/Grid/GridList", model);
            }
            else
            {
                List<ColumnDefinition> columnDefinitions = DefineGrid();
                var task = Task.Run(async () => await _crudService.GetPagedRoles(new GridParam
                {
                    ColumnDefinitions = columnDefinitions,
                    FilterList = filterList,
                    HrisRegObj = hrisObj
                }));

                var ret = task.Result;
                filterList.TotalItems = ret.TotalItems;

                GridListModel model = new GridListModel
                {
                    GridId = this.GetType().Name + "Review" + "_grid",
                    GridAttr = "reviewer",
                    FilterList = filterList,
                    ColumnDefinitions = columnDefinitions
                };
                model.FillRows(ret.Items);
                return PartialView("Component/Grid/GridList", model);
            }
        }

        public PartialViewResult GridListRoles(FilterList filterList)
        {
            var hrisObj = "SHU";
            var username = "";
            if (filterList.UsernameSession == null)
            {
                username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                username = filterList.UsernameSession;
            }

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            var task = Task.Run(async () => await _crudService.GetPagedRoles(new GridParam
            {
                ColumnDefinitions = columnDefinitions,
                FilterList = filterList,
                HrisRegObj = hrisObj
            }));

            var ret = task.Result;
            filterList.TotalItems = ret.TotalItems;

            GridListModel model = new GridListModel
            {
                GridId = this.GetType().Name + "Review" + "_grid",
                GridAttr = "reviewer",
                FilterList = filterList,
                ColumnDefinitions = columnDefinitions,
                UsernameSession = username
            };
            model.FillRows(ret.Items);
            return PartialView("Component/Grid/GridList", model);
        }
        public PartialViewResult GridListReport(FilterList filterList)
        {
            var hrisObj = "SHU";
            var username = "";
            if (filterList.UsernameSession == null)
            {
                username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                username = filterList.UsernameSession;
            }

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            var task = Task.Run(async () => await _crudService.GetPagedReport(new GridParam
            {
                ColumnDefinitions = columnDefinitions,
                FilterList = filterList,
                HrisRegObj = hrisObj
            }));

            var ret = task.Result;
            filterList.TotalItems = ret.TotalItems;

            GridListModel model = new GridListModel
            {
                GridId = this.GetType().Name + "Report" + "_grid",
                GridAttr = "approver",
                FilterList = filterList,
                ColumnDefinitions = columnDefinitions,
                UsernameSession = username
            };
            model.FillRows(ret.Items);
            return PartialView("Component/Grid/GridList", model);
        }

        /// <summary>
        /// Initial Grid Render (Parent of GridList)
        /// </summary>
        /// <param name="filterList"></param>
        /// <returns></returns>
        public PartialViewResult InitGrid(GridParam gridParam)
        {
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            gridParam.ColumnDefinitions = columnDefinitions;

            gridParam.GridId = this.GetType().Name + "_grid";
            return PartialView("Component/Grid/InitGrid", gridParam);
        }
        public PartialViewResult InitGridRoles(GridParam gridParam)
        {
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            gridParam.ColumnDefinitions = columnDefinitions;

            gridParam.GridId = this.GetType().Name + "Review" + "_grid";
            return PartialView("Component/Grid/InitGridRoles", gridParam);
        }
        public PartialViewResult InitGridReport(GridParam gridParam)
        {
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            gridParam.ColumnDefinitions = columnDefinitions;

            gridParam.GridId = this.GetType().Name + "Report" + "_grid";
            return PartialView("Component/Grid/InitGridReport", gridParam);
        }

        /// <summary>
        /// Initial Grid Render (Parent of GridList)
        /// </summary>
        /// <param name="filterList"></param>
        /// <returns></returns>
        public PartialViewResult InitLookupGrid(string orderBy)
        {
            var gridParam = new GridParam
            {
                ColumnDefinitions = DefineGrid(),
                GridId = this.GetType().Name + "_grid",
                FilterList = new FilterList
                {
                    IsForLookup = true,
                    OrderBy = orderBy,
                    Page = 1,
                    Size = 10
                }
            };


            return PartialView("Component/Grid/InitGrid", gridParam);
        }


        public PartialViewResult GetLookupText(Field field)
        {
            var lookUpText = Task.Run(async () => await _crudService.GetLookupText((string)field.Value)).Result;
            field.Value = lookUpText;
            return PartialView("Component/Form/_Field-LookupText", field);
        }

        protected async Task<ActionResult> BaseCreate<TValidator>(TDto model, TValidator validator = null) where TValidator : AbstractValidator<TDto>
        {

            if (validator != null)
            {
                ValidationResult result = validator.Validate(model);


                if (result.IsValid == false)
                {
                    var formDef = DefineForm(FormState.Create);

                    foreach (var fieldSection in formDef.FieldSections)
                    {
                        foreach (var field in fieldSection.Fields)
                        {
                            field.Value = model.GetType().GetProperty(field.Id).GetValue(model);

                            var errorField = result.Errors.FirstOrDefault(o => o.PropertyName == field.Id);
                            if (errorField != null)
                            {
                                field.ErrorMessage = errorField.ErrorMessage;
                            }

                        }
                    }

                    return PartialView("Component/Form/Form", formDef);
                }
            }


            await _crudService.Create(model);
            return new EmptyResult();


        }


        protected async Task<ActionResult> BaseUpdate<TValidator>(TDto model, TValidator validator = null) where TValidator : AbstractValidator<TDto>
        {
            if (validator != null)
            {
                ValidationResult result = validator.Validate(model);


                if (result.IsValid == false)
                {
                    var formDef = DefineForm(FormState.Edit);

                    foreach (var fieldSection in formDef.FieldSections)
                    {
                        foreach (var field in fieldSection.Fields)
                        {
                            field.Value = model.GetType().GetProperty(field.Id).GetValue(model);

                            var errorField = result.Errors.FirstOrDefault(o => o.PropertyName == field.Id);
                            if (errorField != null)
                            {
                                field.ErrorMessage = errorField.ErrorMessage;
                            }

                        }
                    }


                    return PartialView("Component/Form/Form", formDef);
                }
            }


            await _crudService.Update(model);
            return new EmptyResult();


        }


        [HttpGet]
        public ActionResult Create()
        {
            var formDef = DefineForm(FormState.Create);
            return PartialView("Component/Form/Form", formDef);
        }


        [HttpGet]
        public ActionResult CreateChild(string id)
        {
            FormDefinition formDef = DefineForm(FormState.Create);
            FormDefinition innerFormDef = null;
            foreach (var fieldSection in formDef.FieldSections)
            {
                var field = fieldSection.Fields.FirstOrDefault(o => o.Id == id);
                if (field != null)
                {
                    innerFormDef = field.FormDefinition;
                    break;
                }
                
            }

            innerFormDef.State = FormState.Create;
            return PartialView("Component/Form/Grid/_Field-Grid-Form", innerFormDef);
        }

        // GET: {Controller}/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            FormDefinition formDef = DefineForm(FormState.Edit);
            TDto model = await _crudService.GetOne(id);
            FillFormValue(model, formDef);
            return PartialView("Component/Form/Form", formDef);
        }

        private void FillFormValue<TDto>(TDto dto, FormDefinition formDef)
        {
            foreach (var fieldSection in formDef.FieldSections)
            {
                foreach (var field in fieldSection.Fields)
                {
                    var val = dto.GetType().GetProperty(field.Id).GetValue(dto);
                    if (val != null)
                    {
                        if (field.FieldType == FieldType.Date)
                        {
                            field.Value = ((DateTime)val).ToString("yyyy-MM-dd");
                        }
                        else if (field.FieldType == FieldType.DateTime)
                        {
                            field.Value = ((DateTime)val).ToString("yyyy-MM-dd HH:mm");
                        }
                        else if (field.FieldType == FieldType.MultiCheckbox)
                        {
                            try
                            {

                                foreach (var item in (IList)val)
                                {
                                    var oneCheckbox = field.LookUpList.Items.FirstOrDefault(o => o.Value == item.ToString());
                                    if (oneCheckbox != null)
                                    {
                                        oneCheckbox.Selected = true;
                                    }
                                }
                            }
                            catch (Exception e)
                            {

                                throw e;
                            }


                        }
                        else
                        {
                            field.Value = val;
                        }
                    }



                }
            }
        }

        // GET: {Controller}/Detail/5
        [HttpGet]
        public async Task<ActionResult> Detail(string id)
        {
            var formDef = DefineFormView(FormState.View, id);
            var model = await _crudService.GetOne(id);
            //FillFormValue(model, formDef);
            return PartialView("Component/Form/Form_Readonly", formDef);
        }

        [HttpGet]
        public async Task<ActionResult> DetailESDC(string id)
        {
            var formDef = DefineFormView(FormState.View, id);
            var model = await _crudService.GetOne(id);
            //FillFormValue(model, formDef);
            return PartialView("Component/Form/Form_ReadonlyESDC", formDef);
        }

        [HttpPost]
        public async Task<ActionResult> ExportToExcel(FilterList filterList)
        {
            if (filterList.GridId == "ExplorationStructureController_grid")
            {
                using (var workbook = await _crudService.ExportToExcelExplorationStructure(new GridParam
                {
                    ColumnDefinitions = DefineGrid(),
                    FilterList = filterList
                }))
                {
                    return workbook.Deliver("Report.xlsx");
                }
            }
            else if (filterList.GridId == "ExplorationStructureControllerReport_grid")
            {
                var hrisObj = "SHU";
                var username = "";
                if (filterList.UsernameSession == null)
                {
                    username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                            .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                }
                else
                {
                    username = filterList.UsernameSession;
                }

                if (!string.IsNullOrEmpty(username))
                {
                    var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                    if (!string.IsNullOrEmpty(user.OrgUnitID))
                    {
                        var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                        hrisObj = taskHRis.Result;
                    }
                }
                using (var workbook = await _crudService.ExportToExcelExplorationStructure(new GridParam
                {
                    ColumnDefinitions = DefineGridProsResource(),
                    FilterList = filterList,
                    HrisRegObj = hrisObj
                }))
                {
                    return workbook.Deliver("ProsResource.xlsx");
                }
            }
            else
            {
                using (var workbook = await _crudService.ExportToExcel(new GridParam
                {
                    ColumnDefinitions = DefineGrid(),
                    FilterList = filterList
                }))
                {
                    return workbook.Deliver("Report.xlsx");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> ExportToExcelRJPP(FilterList filterList)
        {
            var hrisObj = "SHU";
            var username = "";
            if (filterList.UsernameSession == null)
            {
                username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                username = filterList.UsernameSession;
            }

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            using (var workbook = await _crudService.ExportToExcelRJPP(new GridParam
            {
                ColumnDefinitions = DefineGridRJPP(),
                FilterList = filterList,
                HrisRegObj = hrisObj
            }))
            {
                return workbook.Deliver("Report.xlsx");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ExportToExcelESDC(FilterList filterList)
        {
            var hrisObj = "SHU";
            var username = "";
            if (filterList.UsernameSession == null)
            {
                username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                username = filterList.UsernameSession;
            }

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            using (var workbook = await _crudService.ExportToExcel(new GridParam
            {
                ColumnDefinitions = DefineGrid(),
                FilterList = filterList,
                HrisRegObj = hrisObj
            }))
            {
                return workbook.Deliver("Report.xlsx");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ExportToExcelByID(string xStructureID)
        {
            //exploration structure
            using (var workbook = await _crudService.ExportToExcelExplorationStructure(new GridParam
            {
                ColumnDefinitions = DefineGrid(),
                Data = await GetDataExportExcel(xStructureID),
                FilterList = new FilterList
                {
                    OrderBy = "CreatedDate desc",
                    Page = 1,
                    Size = 10
                }
            }))
            {
                return workbook.Deliver("Report.xlsx");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ExportToPDF(FilterList filterList, string headerText, int[] tableHeaderSizes)
        {
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            filterList.Size = 10000;

            var ret = await _crudService.GetPaged(new GridParam
            {
                ColumnDefinitions = columnDefinitions,
                FilterList = filterList
            });
            filterList.TotalItems = ret.TotalItems;

            GridListModel model = new GridListModel
            {
                GridId = this.GetType().Name + "_grid",
                FilterList = filterList,
                ColumnDefinitions = columnDefinitions
            };
            model.FillRows(ret.Items);

            var pdfByteRes = _crudService.ExportToPDF(model, headerText, tableHeaderSizes);
            return File(pdfByteRes, "application/pdf", "ReportEmployee.pdf");
        }

        public async Task<PartialViewResult> GetAdaptiveFilter(string columnId, string usernameSession)
        {
            var hrisObj = "SHU";
            var username = "";
            if (usernameSession == null)
            {
                username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            }
            else
            {
                username = usernameSession;
            }
            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            var selectList = await _crudService.GetAdaptiveFilterList(columnId, hrisObj);
            return PartialView("Component/Grid/AdaptiveFilter/CheckboxList", selectList);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _crudService.Delete(id);
            return new EmptyResult();
        }
    }
}