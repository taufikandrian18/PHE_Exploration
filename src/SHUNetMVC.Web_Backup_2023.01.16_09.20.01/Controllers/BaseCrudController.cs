﻿using SHUNetMVC.Abstraction.Model.View;
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

namespace SHUNetMVC.Web.Controllers
{
    public abstract class BaseCrudController<TDto, TGridModel> : Controller
    {

        protected abstract FormDefinition DefineForm(FormState formState);
        protected abstract List<ColumnDefinition> DefineGrid();

        public abstract Task<ActionResult> Create(TDto model);
        public abstract Task<ActionResult> Edit(TDto model);


        protected ICrudService<TDto, TGridModel> _crudService;
        protected ILookupService _lookupService;
        public BaseCrudController(ICrudService<TDto, TGridModel> crudSvc, ILookupService lookupSvc)
        {
            _crudService = crudSvc;
            _lookupService = lookupSvc;
        }
        public ActionResult Index()
        {

            var model = new CrudPage
            {
                Id = "ExplorationStructure",
                Title = "Pencatatan Sumber Daya",
                SubTitle = "This is the list of Exploration Structure",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "CreatedDate desc",
                        Page = 1,
                        Size = 10
                    }
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
            List<ColumnDefinition> columnDefinitions = DefineGrid();
            var task = Task.Run(async () => await _crudService.GetPaged(new GridParam
            {
                ColumnDefinitions = columnDefinitions,
                FilterList = filterList
            }));

            var ret = task.Result;
            filterList.TotalItems = ret.TotalItems;
            
            GridListModel model = new GridListModel
            {
                GridId = this.GetType().Name + "_grid",
                FilterList = filterList,
                ColumnDefinitions = columnDefinitions
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
            var formDef = DefineForm(FormState.View);
            var model = await _crudService.GetOne(id);
            FillFormValue(model, formDef);
            return PartialView("Component/Form/Form_Readonly", formDef);
        }


        [HttpPost]
        public async Task<ActionResult> ExportToExcel(FilterList filterList)
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

        public async Task<PartialViewResult> GetAdaptiveFilter(string columnId)
        {
            var selectList = await _crudService.GetAdaptiveFilterList(columnId);
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