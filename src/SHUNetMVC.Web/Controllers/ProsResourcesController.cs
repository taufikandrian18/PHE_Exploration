﻿using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Web.Controllers;
using SHUNetMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class ProsResourcesController : Controller
    {
        private readonly ITXProsResourcesTargetService _service;
        private readonly ITXProsResourcesService _servicePR;
        private readonly IMDParameterListService _servicePL;
        public ProsResourcesController(ITXProsResourcesTargetService crudSvc, ITXProsResourcesService crudPRSvc, IMDParameterListService crudPLSvc)
        {
            _service = crudSvc;
            _servicePR = crudPRSvc;
            _servicePL = crudPLSvc;
        }
        // GET: ProsResources

        public ActionResult EditingCustom()
        {
            PopulateCategories();
            var define = DefineProsResources(FormState.Create, "");
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomEdit(string structureID)
        {
            PopulateCategories();
            var define = DefineProsResources(FormState.Edit, structureID);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        protected FormDefinition DefineProsResources(FormState formState, string structureID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "ProsResources",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldProspectiveResources()
                    }
                };
                return formDef;
            }
            else
            {
                var datas = Task.Run(() => _servicePR.GetOne(structureID)).Result;
                datas.ExpectedPG = decimal.Parse(string.Format("{0:0.##}", datas.ExpectedPG * 100));
                datas.CurrentPG = decimal.Parse(string.Format("{0:0.##}", datas.CurrentPG * 100));
                var formDef = new FormDefinition
                {
                    Title = "ProsResources",
                    State = formState,
                    paramID = structureID,
                    Data = datas,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldProspectiveResources()
                    }
                };
                return formDef;
            }
        }
        private FieldSection FieldProspectiveResources()
        {
            return new FieldSection
            {
                SectionName = "Add New Draft",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureID),
                        FieldType = FieldType.Hidden
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubClassificationParID),
                        Label = "Sub UD Type",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationTypeParID),
                        Label = "Exploration Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDClassificationParID),
                        Label = "UD Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.APHID),
                        Label = "APH",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ZonaID),
                        Label = "Zona",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationAreaParID),
                        Label = "Exp.Area",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = new List<LookupItem>
                            {
                                new LookupItem()
                                {
                                Text = "" ,
                                Description = "",
                                Value = "1"
                                },
                                new LookupItem()
                                {
                                Text = "Lead" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                                Value = "2"
                                },
                                new LookupItem()
                                {
                                Text = "Staff" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                                Value = "3"
                                },
                                new LookupItem()
                                {
                                Text = "Junior" ,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                                Value = "4"
                                },
                            }
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                        Label = "One Pager Montage",
                        FieldType = FieldType.FileUpload,
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StructureOutline),
                        Label = "Structure Outline",
                        FieldType = FieldType.FileUpload,
                    },
                }
            };
        }
        public ActionResult EditingCustom_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ExplorationStructure",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                List<TXPropectiveResourceTargetWthFields> txProsTargetObj = new List<TXPropectiveResourceTargetWthFields>();
                return Json(txProsTargetObj.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ExplorationStructure",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                //var datas = await _service.GetPaged(model.GridParam);
                var datas = Task.Run(() => _service.GetProsResourceTargetByStructureID(clientData.Data)).Result;
                foreach(var item in datas)
                {
                    item.RFOil = item.RFOil * 100;
                    item.RFGas = item.RFGas * 100;
                    item.GCFSR = item.GCFSR * 100;
                    item.GCFTM = item.GCFTM * 100;
                    item.GCFReservoir = item.GCFReservoir * 100;
                    item.GCFClosure = item.GCFClosure * 100;
                    item.GCFContainment = item.GCFContainment * 100;
                    item.GCFPGTotal = item.GCFPGTotal * 100;
                }
                return Json(datas.ToDataSourceResult(request));
            }
        }
        public async Task<ActionResult> EditingCustomView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ExplorationStructure",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                List<TXPropectiveResourceTargetWthFields> txProsTargetObj = new List<TXPropectiveResourceTargetWthFields>();
                return Json(txProsTargetObj.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ExplorationStructure",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                //var datas = await _service.GetPaged(model.GridParam);
                var datas = await _service.GetProsResourceTargetByStructureID(clientData.Data);
                if (datas == null || datas.Count() == 0)
                {
                    List<TXPropectiveResourceTargetWthFields> txProsTargetObj = new List<TXPropectiveResourceTargetWthFields>();
                    return Json(txProsTargetObj.ToDataSourceResult(request));
                }
                else
                {
                    foreach (var item in datas)
                    {
                        item.RFOil = item.RFOil * 100;
                        item.RFGas = item.RFGas * 100;
                        item.GCFSR = item.GCFSR * 100;
                        item.GCFTM = item.GCFTM * 100;
                        item.GCFReservoir = item.GCFReservoir * 100;
                        item.GCFClosure = item.GCFClosure * 100;
                        item.GCFContainment = item.GCFContainment * 100;
                        item.GCFPGTotal = item.GCFPGTotal * 100;
                    }
                    return Json(datas.ToDataSourceResult(request));
                }
            }
        }

        public ActionResult EditingCustomPR_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXProsResourceDto> PRLst = new List<TXProsResourceDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ProsResources",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "pr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };

                TXProsResourceDto PRObj = new TXProsResourceDto
                {
                    xStructureID = "0",
                    ExplorationStructureName = "Structure Name",
                    P90InPlaceOilPR = 0,
                    P50InPlaceOilPR = 0,
                    PMeanInPlaceOilPR = 0,
                    P10InPlaceOilPR = 0,
                    P90InPlaceGasPR = 0,
                    P50InPlaceGasPR = 0,
                    PMeanInPlaceGasPR = 0,
                    P10InPlaceGasPR = 0,
                    RFOilPR = 0,
                    RFGasPR = 0,
                    P90RROil = 0,
                    P50RROil = 0,
                    PMeanRROil = 0,
                    P10RROil = 0,
                    P90RRGas = 0,
                    P50RRGas = 0,
                    PMeanRRGas = 0,
                    P10RRGas = 0,
                    P90RRTotal = 0,
                    P50RRTotal = 0,
                    PMeanRRTotal = 0,
                    P10RRTotal = 0,
                    HydrocarbonTypePRParID = "",
                    GCFSRPR = 0,
                    GCFTMPR = 0,
                    GCFClosurePR = 0,
                    GCFContainmentPR = 0,
                    GCFReservoirPR = 0,
                    GCFPGTotalPR = 0
                };
                PRLst.Add(PRObj);
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ProsResources",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "pr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                var datas = Task.Run(() => _servicePR.GetOne(clientData.Data)).Result;
                datas.RFOilPR = datas.RFOilPR * 100;
                datas.RFGasPR = datas.RFGasPR * 100;
                datas.GCFSRPR = datas.GCFSRPR * 100;
                datas.GCFTMPR = datas.GCFTMPR * 100;
                datas.GCFReservoirPR = datas.GCFReservoirPR * 100;
                datas.GCFClosurePR = datas.GCFClosurePR * 100;
                datas.GCFContainmentPR = datas.GCFContainmentPR * 100;
                datas.GCFPGTotalPR = datas.GCFPGTotalPR * 100;
                PRLst.Add(datas);
                return Json(PRLst.ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> EditingCustomPRView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXProsResourceDto> PRLst = new List<TXProsResourceDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ProsResources",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "pr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };

                TXProsResourceDto PRObj = new TXProsResourceDto
                {
                    xStructureID = "0",
                    ExplorationStructureName = "Structure Name",
                    P90InPlaceOilPR = 0,
                    P50InPlaceOilPR = 0,
                    PMeanInPlaceOilPR = 0,
                    P10InPlaceOilPR = 0,
                    P90InPlaceGasPR = 0,
                    P50InPlaceGasPR = 0,
                    PMeanInPlaceGasPR = 0,
                    P10InPlaceGasPR = 0,
                    RFOilPR = 0,
                    RFGasPR = 0,
                    P90RROil = 0,
                    P50RROil = 0,
                    PMeanRROil = 0,
                    P10RROil = 0,
                    P90RRGas = 0,
                    P50RRGas = 0,
                    PMeanRRGas = 0,
                    P10RRGas = 0,
                    P90RRTotal = 0,
                    P50RRTotal = 0,
                    PMeanRRTotal = 0,
                    P10RRTotal = 0,
                    HydrocarbonTypePRParID = "",
                    GCFSRPR = 0,
                    GCFTMPR = 0,
                    GCFClosurePR = 0,
                    GCFContainmentPR = 0,
                    GCFReservoirPR = 0,
                    GCFPGTotalPR = 0
                };
                PRLst.Add(PRObj);
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ProsResources",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "pr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                var datas = await  _servicePR.GetOne(clientData.Data);
                if(datas == null)
                {
                    TXProsResourceDto PRObj = new TXProsResourceDto
                    {
                        xStructureID = "0",
                        ExplorationStructureName = "Structure Name",
                        P90InPlaceOilPR = 0,
                        P50InPlaceOilPR = 0,
                        PMeanInPlaceOilPR = 0,
                        P10InPlaceOilPR = 0,
                        P90InPlaceGasPR = 0,
                        P50InPlaceGasPR = 0,
                        PMeanInPlaceGasPR = 0,
                        P10InPlaceGasPR = 0,
                        RFOilPR = 0,
                        RFGasPR = 0,
                        P90RROil = 0,
                        P50RROil = 0,
                        PMeanRROil = 0,
                        P10RROil = 0,
                        P90RRGas = 0,
                        P50RRGas = 0,
                        PMeanRRGas = 0,
                        P10RRGas = 0,
                        P90RRTotal = 0,
                        P50RRTotal = 0,
                        PMeanRRTotal = 0,
                        P10RRTotal = 0,
                        HydrocarbonTypePRParID = "",
                        GCFSRPR = 0,
                        GCFTMPR = 0,
                        GCFClosurePR = 0,
                        GCFContainmentPR = 0,
                        GCFReservoirPR = 0,
                        GCFPGTotalPR = 0
                    };
                    PRLst.Add(PRObj);
                    return Json(PRLst.ToDataSourceResult(request));
                }
                else
                {
                    datas.RFOilPR = datas.RFOilPR * 100;
                    datas.RFGasPR = datas.RFGasPR * 100;
                    datas.GCFSRPR = datas.GCFSRPR * 100;
                    datas.GCFTMPR = datas.GCFTMPR * 100;
                    datas.GCFReservoirPR = datas.GCFReservoirPR * 100;
                    datas.GCFClosurePR = datas.GCFClosurePR * 100;
                    datas.GCFContainmentPR = datas.GCFContainmentPR * 100;
                    datas.GCFPGTotalPR = datas.GCFPGTotalPR * 100;
                    PRLst.Add(datas);
                    return Json(PRLst.ToDataSourceResult(request));
                }
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomPR_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXProsResourceDto> prosResources)
        {
            if (prosResources != null && ModelState.IsValid)
            {
                foreach (var product in prosResources)
                {
                    _servicePR.Update(product);
                }
            }

            return Json(prosResources.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomPR_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXProsResourceDto> prosResources)
        {
            var results = new List<TXProsResourceDto>();

            if (prosResources != null && ModelState.IsValid)
            {
                foreach (var item in prosResources)
                {
                    _servicePR.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomPR_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXProsResourceDto> prosResources)
        {
            foreach (var item in prosResources)
            {
                _servicePR.Destroy(item.xStructureID);
            }

            return Json(prosResources.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Update([DataSourceRequest] DataSourceRequest request, 
            [Bind(Prefix = "models")] IEnumerable<TXProsResourcesTargetDto> prosResourcesTarget)
        {
            if (prosResourcesTarget != null && ModelState.IsValid)
            {
                foreach (var product in prosResourcesTarget)
                {
                    _service.Update(product);
                }
            }

            return Json(prosResourcesTarget.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXProsResourcesTargetDto> prosResourcesTarget)
        {
            var results = new List<TXProsResourcesTargetDto>();

            if (prosResourcesTarget != null && ModelState.IsValid)
            {
                foreach (var item in prosResourcesTarget)
                {
                    _service.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustom_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXProsResourcesTargetDto> prosResourcesTarget)
        {
            foreach (var item in prosResourcesTarget)
            {
                _service.Destroy(item.TargetID);
            }

            return Json(prosResourcesTarget.ToDataSourceResult(request, ModelState));
        }
        private void PopulateCategories()
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("HCType")).Result;
            List<MDEntityWithView> hcTypeList = new List<MDEntityWithView>();
            foreach (var item in datas)
            {
                MDEntityWithView hcType = new MDEntityWithView();
                hcType.EntityName = item.ParamValue1Text;
                hcTypeList.Add(hcType);
            }
            ViewData["categories"] = hcTypeList.ToList();
            ViewData["defaultCategory"] = hcTypeList.First();
        }

        public sealed class ClientDataModel
        {
            public string Data { get; set; }
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
    }
}