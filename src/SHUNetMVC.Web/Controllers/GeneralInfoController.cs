using ASPNetMVC.Infrastructure.HttpUtils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.Constant;
using SHUNetMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class GeneralInfoController : Controller
    {
        private readonly IMDParameterListService _servicePL;
        private readonly IMDExplorationBlockService _serviceBL;
        private readonly IMDExplorationBasinService _serviceBS;
        private readonly IMDExplorationAssetService _serviceAS;
        private readonly IMDExplorationAreaService _serviceAR;
        private readonly IMDExplorationStructureService _serviceES;
        private readonly IMDEntityService _service;
        private readonly IUserService _userService;
        private readonly IHRISDevOrgUnitHierarchyService _serviceHris;
        private readonly ITXProsResourcesTargetService _servicePT;
        private readonly ITXProsResourcesService _servicePR;
        private readonly ITXContingenResourcesService _serviceCR;
        private readonly ITXDrillingService _serviceDR;
        private readonly ILGActivityService _serviceAC;
        private readonly ITXEconomicService _serviceEC;
        private readonly IMDExplorationWellService _serviceWL;
        private readonly ITXAttachmentService _serviceAT;
        private readonly IVWDIMEntityService _serviceVW;
        private readonly IVWCountryService _serviceVC;
        private readonly IMDExplorationBlockPartnerService _serviceBP;
        private readonly HttpContextBase _httpContext;
        public GeneralInfoController(IMDParameterListService crudPLSvc,
            IMDEntityService crudSvc,
            IUserService userService,
            IHRISDevOrgUnitHierarchyService serviceHris,
            IMDExplorationBlockService serviceBL,
            IMDExplorationBasinService serviceBS,
            IMDExplorationAssetService serviceAS,
            IMDExplorationAreaService serviceAR,
            IMDExplorationStructureService serviceES,
            ITXProsResourcesTargetService servicePT,
            ITXProsResourcesService servicePR,
            ITXContingenResourcesService serviceCR,
            ITXDrillingService serviceDR,
            ILGActivityService serviceAC,
            IMDExplorationWellService serviceWL,
            ITXEconomicService serviceEC,
            ITXAttachmentService serviceAT,
            IVWDIMEntityService serviceVW,
            IVWCountryService serviceVC,
            IMDExplorationBlockPartnerService serviceBP,
            HttpContextBase httpContext)
        {
            _servicePL = crudPLSvc;
            _service = crudSvc;
            _userService = userService;
            _serviceHris = serviceHris;
            _serviceBL = serviceBL;
            _serviceBS = serviceBS;
            _serviceAS = serviceAS;
            _serviceAR = serviceAR;
            _serviceES = serviceES;
            _servicePT = servicePT;
            _servicePR = servicePR;
            _serviceCR = serviceCR;
            _serviceDR = serviceDR;
            _serviceAC = serviceAC;
            _serviceWL = serviceWL;
            _serviceEC = serviceEC;
            _serviceAT = serviceAT;
            _serviceVW = serviceVW;
            _serviceVC = serviceVC;
            _serviceBP = serviceBP;
            _httpContext = httpContext;
        }
        // GET: GeneralInfo
        public ActionResult Index(string structureID)
        {
            var formDef = DefineForm(FormState.Create, "");
            if (!string.IsNullOrEmpty(structureID))
            {
                formDef = DefineForm(FormState.Edit, structureID);
            }
            return View(formDef);
        }

        public FormDefinition DefineForm(FormState formState, string param)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "Create",
                    paramID = param,
                    State = formState,
                };
                return formDef;
            }
            else
            {
                var explorationStructure = Task.Run(() => _serviceES.GetOne(param)).Result;
                var formDef = new FormDefinition
                {
                    Title = "Edit",
                    paramID = param,
                    BlockID = explorationStructure.xBlockID,
                    State = formState,
                };
                return formDef;
            }
        }

        public PartialViewResult Create()
        {
            FormState ts = FormState.Create;
            var define = DefineGeneralInfo(ts, "");
            return PartialView("Component/Form/Form", define);
        }
        public PartialViewResult Edit(string structureID)
        {
            FormState ts = FormState.Edit;
            var define = DefineGeneralInfo(ts, structureID);
            return PartialView("Component/Form/Form", define);
        }

        public ActionResult Selection_Orders_Read([DataSourceRequest] DataSourceRequest request, ClientDataModel clientData)
        {
            var task = Task.Run(async () => await _userService.GetCurrentUserInfo());
            var userTmp = task.Result;
            var hrisObj = "";

            //var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText("10120140"));
            //hrisObj = taskHRis.Result;

            //real deal
            if (!string.IsNullOrEmpty(userTmp.OrgUnitID))
            {
                var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(userTmp.OrgUnitID));
                hrisObj = taskHRis.Result;
            }

            var datas = Task.Run(() => _service.GetLookupListText(DateTime.Now.Year.ToString(), hrisObj.Trim())).Result;
            foreach (var item in datas)
            {
                item.EntityName = Task.Run(() => _serviceVW.GetLookupText(item.SubholdingID)).Result;
                item.RegionalName = Task.Run(() => _serviceVW.GetLookupText(item.RegionalID)).Result;
                item.ZonaName = Task.Run(() => _serviceVW.GetLookupText(item.ZonaID)).Result;
                item.APHName = Task.Run(() => _serviceVW.GetLookupText(item.APHID)).Result;
            }
            return Json(datas.ToDataSourceResult(request));
        }

        protected FormDefinition DefineGeneralInfo(FormState formState, string paramID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "GeneralInfo",
                    State = formState,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldGeneralInfo()
                    }
                };
                return formDef;
            }
            else
            {
                var formDef = new FormDefinition
                {
                    Title = "GeneralInfo",
                    State = formState,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldGeneralInfo(paramID)
                    }
                };
                return formDef;
            }
        }

        protected List<ColumnDefinition> DefineExplorationAreaGrid()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Area ID", nameof(MDExplorationAreaDto.xAreaID), ColumnType.String),
                new ColumnDefinition("Area Name", nameof(MDExplorationAreaDto.xAreaName), ColumnType.String),
            };
        }

        private FieldSection FieldGeneralInfo()
        {
            List<LookupItem> rkapPlanList = new List<LookupItem>();
            LookupItem rkapPlanFirst = new LookupItem();
            rkapPlanFirst.Text = "- Not Selected -";
            rkapPlanFirst.Description = "";
            rkapPlanFirst.Value = "0";
            rkapPlanList.Add(rkapPlanFirst);
            for (int i = DateTime.Now.Year; i <= 2123; i++)
            {
                LookupItem rkapPlan = new LookupItem();
                rkapPlan.Text = i.ToString();
                rkapPlan.Description = "";
                rkapPlan.Value = i.ToString();
                rkapPlanList.Add(rkapPlan);
            }

            var datas = Task.Run(() => _servicePL.GetLookupListText("ExplorationStructureStatus")).Result;
            List<LookupItem> ExpStatusList = new List<LookupItem>();
            LookupItem expStatusFirst = new LookupItem();
            expStatusFirst.Text = "- Select An Option -";
            expStatusFirst.Description = "";
            expStatusFirst.Value = "0";
            ExpStatusList.Add(expStatusFirst);
            foreach (var item in datas)
            {
                LookupItem expStatus = new LookupItem();
                expStatus.Text = item.ParamValue1Text;
                expStatus.Description = "";
                expStatus.Value = item.ParamListID;
                ExpStatusList.Add(expStatus);
            }

            List<LookupItem> ExpTypeList = new List<LookupItem>();
            LookupItem expTypeFirst = new LookupItem();
            expTypeFirst.Text = "- Select An Option -";
            expTypeFirst.Description = "";
            expTypeFirst.Value = "0";
            ExpTypeList.Add(expTypeFirst);
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    LookupItem expType = new LookupItem();
                    expType.Text = "Frontier";
                    expType.Description = "";
                    expType.Value = i.ToString();
                    ExpTypeList.Add(expType);
                }
                else
                {
                    LookupItem expType = new LookupItem();
                    expType.Text = "Nearby";
                    expType.Description = "";
                    expType.Value = i.ToString();
                    ExpTypeList.Add(expType);
                }
            }


            var model = new CrudPage
            {
                Id = "ExplorationArea",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineExplorationAreaGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "xAreaID desc",
                        Page = 1,
                        Size = 1000
                    }
                }
            };
            var dataEA = Task.Run(async () => await _serviceAR.GetPaged(model.GridParam)).Result;
            List<LookupItem> expAreaList = new List<LookupItem>();
            foreach (var item in dataEA.Items)
            {
                LookupItem expArea = new LookupItem();
                expArea.Text = item.xAreaName;
                expArea.Description = "";
                expArea.Value = item.xAreaID;
                expAreaList.Add(expArea);
            }

            var dataSM = Task.Run(() => _servicePL.GetLookupListText("SingleOrMulti")).Result;
            List<LookupItem> singleMultiList = new List<LookupItem>();
            foreach (var item in dataSM)
            {
                LookupItem singleMulti = new LookupItem();
                singleMulti.Text = item.ParamValue1Text;
                singleMulti.Description = "";
                singleMulti.Value = item.ParamListID;
                singleMultiList.Add(singleMulti);
            }

            return new FieldSection
            {
                SectionName = "Add New Draft",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureID),
                        FieldType = FieldType.Hidden
                    }, //kiri hidden
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationAreaParID),
                        Label = "Exp.Area",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = expAreaList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID ),
                        Label = "Exploration Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = ExpStatusList
                        },
                        //LookUpList = new LookupList
                        //{
                        //    Items = new List<LookupItem>
                        //    {
                        //        new LookupItem()
                        //        {
                        //        Text = "" ,
                        //        Description = "",
                        //        Value = "1"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "Lead" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                        //        Value = "2"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "Prospect" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        //        Value = "3"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "PSB" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                        //        Value = "4"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "Undevelop Discovery" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                        //        Value = "5"
                        //        },
                        //    }
                        //},
                        IsRequired = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationTypeParID),
                        Label = "Exploration Type",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = ExpTypeList
                        },
                        //LookUpList = new LookupList
                        //{
                        //    Items = new List<LookupItem>
                        //    {
                        //        new LookupItem()
                        //        {
                        //        Text = "" ,
                        //        Description = "",
                        //        Value = "1"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "Lead" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                        //        Value = "2"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "Prospect" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        //        Value = "3"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "PSB" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                        //        Value = "4"
                        //        },
                        //        new LookupItem()
                        //        {
                        //        Text = "Undevelop Discovery" ,
                        //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore",
                        //        Value = "5"
                        //        },
                        //    }
                        //},
                        IsRequired = true
                    }, //kanan
                    new Field {
                        Id = "EntityID",
                        Label = "Select Entity",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.SubholdingID),
                        Label = "Subholding ID",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubTypeParID),
                        Label = "UD Type",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDClassificationParID),
                        Label = "UD Classification",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubClassificationParID),
                        Label = "UD Sub Classification",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ZonaID),
                        Label = "Zona",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDEntityDto.EffectiveYear),
                        Label = "RKAP Plan",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = rkapPlanList
                        },
                        IsRequired = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = singleMultiList
                        },
                        IsRequired = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.APHID),
                        Label = "APH",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                        Label = "One Pager Montage - (.pptx .pdf)",
                        FieldType = FieldType.FileUpload,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StructureOutline),
                        Label = "Structure Outline - (.shp)",
                        FieldType = FieldType.FileUpload,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                }
            };
        }
        [HttpGet]
        private FieldSection FieldGeneralInfo(string paramID)
        {
            // get data exploration structure
            var explorationStructure = Task.Run(() => _serviceES.GetOne(paramID)).Result;
            // get data drilling list by structure ID
            var drillingList = Task.Run(() => _serviceDR.GetDrillingByStructureID(paramID)).Result;
            var yearRKAP = 0;
            var fileUrl1 = "";
            var fileUrl2 = "";
            var fileType1 = "";
            var fileType2 = "";
            var fileId1 = 0;
            var fileId2 = 0;
            var fileName1 = "";
            var fileName2 = "";
            var getFileAttachments = Task.Run(() => _serviceAT.GetAttachmentByStructureID(paramID)).Result;
            foreach (var item in getFileAttachments)
            {
                if (item.FileCategoryParID.Trim() == "OPM")
                {
                    fileUrl1 = FileServices.GetFileUrl(item.FileName);
                    fileType1 = System.IO.Path.GetExtension(item.FileName);
                    fileId1 = item.FileID;
                    fileName1 = item.FileName;
                }
                else
                {
                    fileUrl2 = FileServices.GetFileUrl(item.FileName);
                    fileType2 = System.IO.Path.GetExtension(item.FileName);
                    fileId2 = item.FileID;
                    fileName2 = item.FileName;
                }
            }

            List<LookupItem> rkapPlanList = new List<LookupItem>();
            LookupItem rkapPlanInitial = new LookupItem();
            rkapPlanInitial.Text = "- Non Selected -";
            rkapPlanInitial.Description = "";
            rkapPlanInitial.Value = "0";
            rkapPlanList.Add(rkapPlanInitial);
            if (drillingList.Count() > 0)
            {
                for (int i = DateTime.Now.Year; i <= 2123; i++)
                {
                    LookupItem rkapPlan = new LookupItem();
                    rkapPlan.Text = i.ToString();
                    rkapPlan.Description = "";
                    rkapPlan.Value = i.ToString();
                    if (i == drillingList[0].RKAPFiscalYear)
                    {
                        rkapPlan.Selected = true;
                        yearRKAP = drillingList[0].RKAPFiscalYear;
                    }
                    rkapPlanList.Add(rkapPlan);
                }
                //rkapPlanList.Add(new LookupItem { Text = "- Not Selected -", Description = "", Value = "0" });
            }
            else
            {
                for (int i = DateTime.Now.Year; i <= 2123; i++)
                {
                    LookupItem rkapPlan = new LookupItem();
                    rkapPlan.Text = i.ToString();
                    rkapPlan.Description = "";
                    rkapPlan.Value = i.ToString();
                    if (i == DateTime.Now.Year)
                    {
                        rkapPlan.Selected = true;
                    }
                    rkapPlanList.Add(rkapPlan);
                }
                //rkapPlanList.Add(new LookupItem { Text = "- Not Selected -", Description = "", Value = "0" });
            }

            List<LookupItem> udSubClassificationList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.UDSubClassificationParID))
            {
                for (int i = 1; i <= 2; i++)
                {
                    LookupItem udSubClass = new LookupItem();
                    if (i == 1)
                    {
                        udSubClass.Text = "K7A";
                        udSubClass.Description = "";
                        udSubClass.Value = i.ToString();
                        if (i.ToString() == explorationStructure.UDSubClassificationParID.Trim())
                        {
                            udSubClass.Selected = true;
                        }
                    }
                    else
                    {
                        udSubClass.Text = "K7B";
                        udSubClass.Description = "";
                        udSubClass.Value = i.ToString();
                        if (i.ToString() == explorationStructure.UDSubClassificationParID.Trim())
                        {
                            udSubClass.Selected = true;
                        }
                    }
                    udSubClassificationList.Add(udSubClass);
                }
            }

            var dataReg = Task.Run(() => _service.GetAdaptiveFilterList("RegionalID", "RegionalID")).Result;
            List<LookupItem> dataRegList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.RegionalID))
            {
                if (dataReg.Items.Count() > 0)
                {
                    foreach (var item in dataReg.Items)
                    {
                        LookupItem expReg = new LookupItem();
                        expReg.Text = Task.Run(() => _serviceVW.GetLookupText(item.Value.Trim())).Result;
                        expReg.Description = "";
                        expReg.Value = item.Value;
                        if (item.Value.Trim() == explorationStructure.RegionalID.Trim())
                        {
                            expReg.Selected = true;
                        }
                        dataRegList.Add(expReg);
                    }
                }
            }
            var dataSHU = Task.Run(() => _service.GetAdaptiveFilterList("SubholdingID", "SubholdingID")).Result;
            List<LookupItem> dataEntitySHUList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.SubholdingID))
            {
                if (dataSHU.Items.Count() > 0)
                {
                    foreach (var item in dataSHU.Items)
                    {
                        LookupItem expEnt = new LookupItem();
                        expEnt.Text = Task.Run(() => _serviceVW.GetLookupText(item.Value.Trim())).Result;
                        expEnt.Description = "";
                        expEnt.Value = item.Value;
                        if (item.Value.Trim() == explorationStructure.SubholdingID.Trim())
                        {
                            expEnt.Selected = true;
                        }
                        dataEntitySHUList.Add(expEnt);
                    }
                }
            }
            var dataZona = Task.Run(() => _service.GetAdaptiveFilterList("ZonaID", "ZonaID")).Result;
            List<LookupItem> dataZonaList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.ZonaID))
            {
                if (dataZona.Items.Count() > 0)
                {
                    foreach (var item in dataZona.Items)
                    {
                        LookupItem expZona = new LookupItem();
                        expZona.Text = Task.Run(() => _serviceVW.GetLookupText(item.Value.Trim())).Result;
                        expZona.Description = "";
                        expZona.Value = item.Value;
                        if (item.Value.Trim() == explorationStructure.ZonaID.Trim())
                        {
                            expZona.Selected = true;
                        }
                        dataZonaList.Add(expZona);
                    }
                }
            }
            var dataCountry = Task.Run(() => _serviceVC.GetAdaptiveFilterList("CountriesID", "CountriesID")).Result;
            List<LookupItem> dataEntityCountryList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.CountriesID))
            {
                if (dataCountry.Items.Count() > 0)
                {
                    foreach (var item in dataCountry.Items)
                    {
                        LookupItem expCountry = new LookupItem();
                        expCountry.Text = Task.Run(() => _serviceVC.GetCountryByCountryID(item.Value.Trim())).Result;
                        expCountry.Description = "";
                        expCountry.Value = item.Value;
                        if (item.Value.Trim() == explorationStructure.CountriesID.Trim())
                        {
                            expCountry.Selected = true;
                        }
                        dataEntityCountryList.Add(expCountry);
                    }
                }
            }
            var dataAPH = Task.Run(() => _service.GetAdaptiveFilterList("APHID", "APHID")).Result;
            List<LookupItem> dataAPHList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.APHID))
            {
                if (dataAPH.Items.Count() > 0)
                {
                    foreach (var item in dataAPH.Items)
                    {
                        LookupItem expAPH = new LookupItem();
                        expAPH.Text = Task.Run(() => _serviceVW.GetLookupText(item.Value.Trim())).Result;
                        expAPH.Description = "";
                        expAPH.Value = item.Value;
                        if (item.Value.Trim() == explorationStructure.APHID.Trim())
                        {
                            expAPH.Selected = true;
                        }
                        dataAPHList.Add(expAPH);
                    }
                }
            }

            var datasUD = Task.Run(() => _servicePL.GetLookupListText("SubUDStatus")).Result;
            List<LookupItem> subUDStatusList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.UDSubTypeParID))
            {
                if (datasUD.Count() > 0)
                {
                    foreach (var item in datasUD)
                    {
                        LookupItem subUD = new LookupItem();
                        subUD.Text = item.ParamValue1Text;
                        subUD.Description = "";
                        subUD.Value = item.ParamListID;
                        if (item.ParamListID.Trim() == explorationStructure.UDSubTypeParID.Trim())
                        {
                            subUD.Selected = true;
                        }
                        subUDStatusList.Add(subUD);
                    }
                }
            }

            var datasClassification = Task.Run(() => _servicePL.GetLookupListText("UDClassification")).Result;
            List<LookupItem> uDClassificationList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.UDClassificationParID))
            {
                if (datasClassification.Count() > 0)
                {
                    foreach (var item in datasClassification)
                    {
                        LookupItem uDClassification = new LookupItem();
                        uDClassification.Text = item.ParamValue1Text;
                        uDClassification.Description = "";
                        uDClassification.Value = item.ParamListID;
                        if (item.ParamListID.Trim() == explorationStructure.UDClassificationParID.Trim())
                        {
                            uDClassification.Selected = true;
                        }
                        uDClassificationList.Add(uDClassification);
                    }
                }
            }

            var datas = Task.Run(() => _servicePL.GetLookupListText("ExplorationStructureStatus")).Result;
            List<LookupItem> ExpStatusList = new List<LookupItem>();
            LookupItem expStatusFirst = new LookupItem();
            expStatusFirst.Text = "- Select An Option -";
            expStatusFirst.Description = "";
            expStatusFirst.Value = "0";
            ExpStatusList.Add(expStatusFirst);
            foreach (var item in datas)
            {
                LookupItem expStatus = new LookupItem();
                expStatus.Text = item.ParamValue1Text;
                expStatus.Description = "";
                expStatus.Value = item.ParamListID;
                if (item.ParamListID.Trim() == explorationStructure.xStructureStatusParID.Trim())
                {
                    expStatus.Selected = true;
                }
                ExpStatusList.Add(expStatus);
            }

            List<LookupItem> ExpTypeList = new List<LookupItem>();
            LookupItem expTypeFirst = new LookupItem();
            expTypeFirst.Text = "- Select An Option -";
            expTypeFirst.Description = "";
            expTypeFirst.Value = "0";
            ExpTypeList.Add(expTypeFirst);
            for (int i = 1; i <= 2; i++)
            {
                if (i == 1)
                {
                    LookupItem expType = new LookupItem();
                    expType.Text = "Frontier";
                    expType.Description = "";
                    expType.Value = i.ToString();
                    if (i.ToString().Trim() == explorationStructure.ExplorationTypeParID.Trim())
                    {
                        expType.Selected = true;
                    }
                    ExpTypeList.Add(expType);
                }
                else
                {
                    LookupItem expType = new LookupItem();
                    expType.Text = "Nearby";
                    expType.Description = "";
                    expType.Value = i.ToString();
                    if (i.ToString().Trim() == explorationStructure.ExplorationTypeParID.Trim())
                    {
                        expType.Selected = true;
                    }
                    ExpTypeList.Add(expType);
                }
            }

            var modelBlock = new CrudPage
            {
                Id = "ExplorationBlock",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineExplorationAreaGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "xBlockID asc",
                        Page = 1,
                        Size = 1000
                    }
                }
            };
            var dataBL = Task.Run(() => _serviceBL.GetPaged(modelBlock.GridParam)).Result;
            List<LookupItem> expBlockList = new List<LookupItem>();
            foreach (var item in dataBL.Items)
            {
                LookupItem expBlock = new LookupItem();
                expBlock.Text = item.xBlockName;
                expBlock.Description = "";
                expBlock.Value = item.xBlockID;
                if (item.xBlockID.Trim() == explorationStructure.xBlockID.Trim())
                {
                    expBlock.Selected = true;
                }
                expBlockList.Add(expBlock);
            }

            var modelAsset = new CrudPage
            {
                Id = "ExplorationAsset",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineExplorationAreaGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "xAssetID asc",
                        Page = 1,
                        Size = 1000
                    }
                }
            };
            var dataASS = Task.Run(() => _serviceAS.GetPaged(modelAsset.GridParam)).Result;
            List<LookupItem> expAssetList = new List<LookupItem>();
            foreach (var item in dataASS.Items)
            {
                LookupItem expAsset = new LookupItem();
                expAsset.Text = item.xAssetName;
                expAsset.Description = "";
                expAsset.Value = item.xAssetID;
                if (item.xAssetID.Trim() == explorationStructure.xAssetID.Trim())
                {
                    expAsset.Selected = true;
                }
                expAssetList.Add(expAsset);
            }

            var modelBasin = new CrudPage
            {
                Id = "ExplorationBasin",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineExplorationAreaGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "BasinID asc",
                        Page = 1,
                        Size = 1000
                    }
                }
            };
            var dataBS = Task.Run(() => _serviceBS.GetPaged(modelBasin.GridParam)).Result;
            List<LookupItem> expBasinList = new List<LookupItem>();
            foreach (var item in dataBS.Items)
            {
                LookupItem expBasin = new LookupItem();
                expBasin.Text = item.BasinName;
                expBasin.Description = "";
                expBasin.Value = item.BasinID;
                if (item.BasinID.Trim() == explorationStructure.BasinID.Trim())
                {
                    expBasin.Selected = true;
                }
                expBasinList.Add(expBasin);
            }

            var model = new CrudPage
            {
                Id = "ExplorationArea",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "_grid",
                    ColumnDefinitions = DefineExplorationAreaGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "xAreaID desc",
                        Page = 1,
                        Size = 1000
                    }
                }
            };
            var dataEA = Task.Run(() => _serviceAR.GetPaged(model.GridParam)).Result;
            List<LookupItem> expAreaList = new List<LookupItem>();
            foreach (var item in dataEA.Items)
            {
                LookupItem expArea = new LookupItem();
                expArea.Text = item.xAreaName;
                expArea.Description = "";
                expArea.Value = item.xAreaID;
                if (item.xAreaID.Trim() == explorationStructure.ExplorationAreaParID.Trim())
                {
                    expArea.Selected = true;
                }
                expAreaList.Add(expArea);
            }

            var dataSM = Task.Run(() => _servicePL.GetLookupListText("SingleOrMulti")).Result;
            List<LookupItem> singleMultiList = new List<LookupItem>();
            foreach (var item in dataSM)
            {
                LookupItem singleMulti = new LookupItem();
                singleMulti.Text = item.ParamValue1Text;
                singleMulti.Description = "";
                singleMulti.Value = item.ParamListID;
                if (item.ParamListID.Trim() == explorationStructure.SingleOrMultiParID.Trim())
                {
                    singleMulti.Selected = true;
                }
                singleMultiList.Add(singleMulti);
            }

            return new FieldSection
            {
                SectionName = "Edit Draft",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureID),
                        FieldType = FieldType.Hidden,
                        Value = paramID
                    }, //kiri hidden
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        Value = explorationStructure.xStructureName,
                        IsRequired = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationAreaParID),
                        Label = "Exp.Area",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.ExplorationAreaParID,
                        LookUpList = new LookupList
                        {
                            Items = expAreaList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID),
                        Label = "Exploration Status",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.xStructureStatusParID,
                        LookUpList = new LookupList
                        {
                            Items = ExpStatusList
                        },
                        IsRequired = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationTypeParID),
                        Label = "Exploration Type",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.ExplorationTypeParID,
                        LookUpList = new LookupList
                        {
                            Items = ExpTypeList
                        },
                        IsRequired = true
                    }, //kanan
                    new Field {
                        Id = "EntityID",
                        Label = "Select Entity",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text,
                        Value = explorationStructure.Play,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.SubholdingID),
                        Label = "Subholding ID",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.SubholdingID,
                        LookUpList = new LookupList
                        {
                            Items = dataEntitySHUList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubTypeParID),
                        Label = "UD Type",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.UDSubTypeParID,
                        LookUpList = new LookupList
                        {
                            Items = subUDStatusList
                        },
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.CountriesID,
                        LookUpList = new LookupList
                        {
                            Items = dataEntityCountryList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDClassificationParID),
                        Label = "UD Classification",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.UDClassificationParID,
                        LookUpList = new LookupList
                        {
                            Items = uDClassificationList
                        },
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.RegionalID,
                        LookUpList = new LookupList
                        {
                            Items = dataRegList
                        },
                        IsRequired = true,
                        IsDisabled = true,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubClassificationParID),
                        Label = "UD Sub Classification",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.UDSubClassificationParID,
                        LookUpList = new LookupList
                        {
                            Items = udSubClassificationList
                        },
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ZonaID),
                        Label = "Zona",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.ZonaID,
                        LookUpList = new LookupList
                        {
                            Items = dataZonaList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDEntityDto.EffectiveYear),
                        Label = "RKAP Plan",
                        FieldType = FieldType.Dropdown,
                        Value = yearRKAP.ToString(),
                        LookUpList = new LookupList
                        {
                            Items = rkapPlanList
                        },
                        IsRequired = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.xBlockID,
                        LookUpList = new LookupList
                        {
                            Items = expBlockList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = singleMultiList
                        },
                        IsRequired = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.APHID),
                        Label = "APH",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.APHID,
                        LookUpList = new LookupList
                        {
                            Items = dataAPHList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                        Label = "One Pager Montage - (.pptx .pdf)",
                        Value = fileUrl1,
                        FileID = fileId1,
                        FileType = fileType1,
                        Filename = fileName1,
                        FieldType = FieldType.FileUpload,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.xAssetID,
                        LookUpList = new LookupList
                        {
                            Items = expAssetList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StructureOutline),
                        Label = "Structure Outline  - (.shp)",
                        Value = fileUrl2,
                        FileID = fileId2,
                        FileType = fileType2,
                        Filename = fileName2,
                        FieldType = FieldType.FileUpload,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.BasinID,
                        LookUpList = new LookupList
                        {
                            Items = expBasinList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                }
            };
        }

        [HttpGet]
        public JsonResult CheckExplorationName(string xStructureName)
        {
            var datas = Task.Run(() => _serviceES.GetByStructureName(xStructureName)).Result;
            CountExplorationStructureName exploreName = new CountExplorationStructureName();
            if (datas.Count() > 0)
            {
                exploreName.IsExist = true;
            }
            else
            {
                exploreName.IsExist = false;
            }
            return new JsonResult
            {
                Data = exploreName,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public JsonResult GenerateUDType(string dropdownId, string transactionValueId)
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("SubUDStatus")).Result;
            List<LookupItem> udTypeList = new List<LookupItem>();
            foreach (var item in datas)
            {
                LookupItem udType = new LookupItem();
                udType.Text = item.ParamValue1Text;
                udType.Description = "";
                udType.Value = item.ParamListID;
                udTypeList.Add(udType);
            }
            return new JsonResult
            {
                Data = udTypeList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public JsonResult GenerateUDClassification(string dropdownId, string transactionValueId)
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("UDClassification")).Result;
            List<LookupItem> udClassificationList = new List<LookupItem>();
            foreach (var item in datas)
            {
                LookupItem udClassification = new LookupItem();
                udClassification.Text = item.ParamValue1Text;
                udClassification.Description = "";
                udClassification.Value = item.ParamListID;
                udClassificationList.Add(udClassification);
            }
            return new JsonResult
            {
                Data = udClassificationList,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public JsonResult GenerateUDSubClassification(string dropdownId, string transactionValueId)
        {
            List<LookupItem> result = new List<LookupItem>
            {
                new LookupItem()
                {
                    Text = "K7A" ,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    Value = "1"
                },
                new LookupItem()
                {
                    Text = "K7B" ,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    Value = "2"
                },
            };
            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public JsonResult GenerateGeneralInfoDropDownLst(string shuId, string regId, string zonId, string blcId, string basId, string astId, string aphId, string areaId)
        {
            MPEntityListITem result = new MPEntityListITem();
            MPEntityBlockOject resultChild = new MPEntityBlockOject();
            result.SubholdingID = shuId;
            result.EntityName = Task.Run(() => _serviceVW.GetLookupText(shuId.Trim())).Result;
            result.Region = regId;
            result.RegionName = Task.Run(() => _serviceVW.GetLookupText(regId.Trim())).Result;
            result.Zona = zonId;
            result.ZonaName = Task.Run(() => _serviceVW.GetLookupText(zonId.Trim())).Result;
            var blockDatas = Task.Run(() => _serviceBL.GetOne(blcId.Trim())).Result;
            if (blockDatas != null)
            {
                resultChild.BlockID = blockDatas.xBlockID;
                resultChild.BlockName = blockDatas.xBlockName;
                resultChild.AwardDate = blockDatas.AwardDate.HasValue ? blockDatas.AwardDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd");
                resultChild.ExpiredDate = blockDatas.ExpiredDate.HasValue ? blockDatas.ExpiredDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd");
                resultChild.BlockStatusParID = blockDatas.xBlockStatusParID;
                var dataBS = Task.Run(() => _servicePL.GetLookupListText("BlockStatus")).Result;
                foreach (var item in dataBS)
                {
                    if (item.ParamListID == blockDatas.xBlockStatusParID)
                    {
                        resultChild.BlockStatusParValueID = item.ParamValue1Text;
                    }
                }
                resultChild.OperatorshipStatusParID = blockDatas.OperatorshipStatusParID;
                var dataOS = Task.Run(() => _servicePL.GetLookupListText("Operators")).Result;
                foreach (var item in dataOS)
                {
                    if (item.ParamListID == blockDatas.OperatorshipStatusParID)
                    {
                        resultChild.OperatorshipStatusParValueID = item.ParamValue1Text;
                    }
                }
                var task = Task.Run(async () => await _serviceBP.GetLookupListText(blockDatas.xBlockID)).Result;
                resultChild.OperatorName = task.Where(x => x.PartnerName.ToLower().Contains("pertamina")).Select(x => x.PartnerName).FirstOrDefault();
                //resultChild.OperatorName = blockDatas.OperatorName;
                resultChild.Country = blockDatas.CountriesID;
                resultChild.CountryName = Task.Run(() => _serviceVC.GetCountryByCountryID(blockDatas.CountriesID)).Result;
            }
            else
            {
                resultChild.BlockID = "";
                resultChild.BlockName = "";
                resultChild.AwardDate = "";
                resultChild.ExpiredDate = "";
                resultChild.BlockStatusParID = "";
                resultChild.OperatorshipStatusParID = "";
                resultChild.OperatorName = "";
                resultChild.Country = "";
                resultChild.CountryName = "";
            }
            result.BlockObject = resultChild;
            var basinDatas = Task.Run(() => _serviceBS.GetOne(basId.Trim())).Result;
            if (basinDatas != null)
            {
                result.BasinName = basinDatas.BasinName;
            }
            else
            {
                result.BasinName = "";
            }
            var assetDatas = Task.Run(() => _serviceAS.GetOne(astId.Trim())).Result;
            if (assetDatas != null)
            {
                result.AssetName = assetDatas.xAssetName;
            }
            else
            {
                result.AssetName = "";
            }
            result.APH = aphId;
            result.APHName = Task.Run(() => _serviceVW.GetLookupText(aphId.Trim())).Result;
            var areaDatas = Task.Run(() => _serviceAR.GetOne(areaId.Trim())).Result;
            if (areaDatas != null)
            {
                result.AreaName = areaDatas.xAreaName;
            }
            else
            {
                result.AreaName = "";
            }

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles(string structureid, string fileId1, string fileId2, HttpPostedFileBase fileList)
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {

                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;
                        int byteCount = file.ContentLength;
                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        string fileExt = System.IO.Path.GetExtension(fname);
                        var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                            .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                        var attachmentObj = await _serviceAT.GetAttachmentByStructureID(structureid.Trim());
                        if (attachmentObj.Count() > 0)
                        {
                            //delete dari db
                            //for (int m = 0; m < attachmentObj.Count(); m++)
                            //{
                            //    if (attachmentObj[m].FileCategoryParID.Trim() == files.AllKeys[i].Trim())
                            //    {
                            //        await _serviceAT.Destroy(attachmentObj[m].TransactionID, attachmentObj[m].FileID);
                            //    }
                            //}

                            List<string> lstFileId = new List<string>();
                            lstFileId.Add(fileId1);
                            lstFileId.Add(fileId2);
                            foreach (var fld in lstFileId)
                            {
                                var fldTmp = fld;
                                if (fldTmp.Trim() == "")
                                {
                                    fldTmp = "0";
                                }
                                var itemAtt = attachmentObj.Where(x => x.FileID == Convert.ToInt32(fldTmp)).FirstOrDefault();
                                if (itemAtt != null)
                                {
                                    itemAtt.IsDeleted = true;
                                    await _serviceAT.Update(itemAtt);
                                }
                            }

                            //delete dari folder

                            //insert baru ke db
                            var fileID = _serviceAT.GenerateNewID();
                            TXAttachmentDto attachmentNewObj = new TXAttachmentDto();
                            attachmentNewObj.Schema = "xplore";
                            attachmentNewObj.TransactionID = structureid;
                            attachmentNewObj.Menu = "ExplorationStructure";
                            attachmentNewObj.FileNameSave = fileID + "_" + structureid + "_" + files.AllKeys[i];
                            attachmentNewObj.Remarks = files.AllKeys[i];
                            attachmentNewObj.Size = byteCount;
                            attachmentNewObj.SizeUoM = "Bytes";
                            attachmentNewObj.PathParID = "1";
                            attachmentNewObj.FileID = fileID + 1;
                            attachmentNewObj.FileCategoryParID = files.AllKeys[i];
                            attachmentNewObj.FileName = fname;
                            attachmentNewObj.IsDeleted = false;
                            attachmentNewObj.DocumentType = fileExt;
                            attachmentNewObj.CreatedDate = DateTime.Now;
                            attachmentNewObj.CreatedBy = user;
                            attachmentNewObj.UpdatedDate = DateTime.Now;
                            attachmentNewObj.UpdatedBy = user;
                            await _serviceAT.Create(attachmentNewObj);

                            //insert baru ke folder
                            // Get the complete folder path and store the file inside it.  
                            fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                            file.SaveAs(fname);
                        }
                        else
                        {
                            //insert baru ke db
                            var fileID = _serviceAT.GenerateNewID();
                            TXAttachmentDto attachmentNewObj = new TXAttachmentDto();
                            attachmentNewObj.Schema = "xplore";
                            attachmentNewObj.TransactionID = structureid;
                            attachmentNewObj.Menu = "ExplorationStructure";
                            attachmentNewObj.FileNameSave = fileID + "_" + structureid + "_" + files.AllKeys[i];
                            attachmentNewObj.Remarks = files.AllKeys[i];
                            attachmentNewObj.Size = byteCount;
                            attachmentNewObj.SizeUoM = "Bytes";
                            attachmentNewObj.PathParID = "1";
                            attachmentNewObj.FileID = fileID;
                            attachmentNewObj.FileCategoryParID = files.AllKeys[i];
                            attachmentNewObj.FileName = fname;
                            attachmentNewObj.IsDeleted = false;
                            attachmentNewObj.DocumentType = fileExt;
                            attachmentNewObj.CreatedDate = DateTime.Now;
                            attachmentNewObj.CreatedBy = user;
                            attachmentNewObj.UpdatedDate = DateTime.Now;
                            attachmentNewObj.UpdatedBy = user;
                            await _serviceAT.Create(attachmentNewObj);

                            //insert baru ke folder
                            // Get the complete folder path and store the file inside it.  
                            fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                            file.SaveAs(fname);
                        }
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                var attachmentObj = await _serviceAT.GetAttachmentByStructureID(structureid.Trim());
                if (attachmentObj.Count() > 0)
                {
                    List<string> lstFileId = new List<string>();
                    lstFileId.Add(fileId1);
                    lstFileId.Add(fileId2);
                    foreach (var fld in lstFileId)
                    {
                        var itemAtt = attachmentObj.Where(x => x.FileID == Convert.ToInt32(fld)).FirstOrDefault();
                        if (itemAtt != null)
                        {
                            itemAtt.IsDeleted = true;
                            await _serviceAT.Update(itemAtt);
                        }
                    }
                    return Json("File Uploaded Successfully!");
                }
                else
                {
                    return Json("No files selected.");
                }
            }

        }

        [HttpPost]
        [Obsolete]
        public async Task<JsonResult> SaveAll(string StructureName,
            string CountriesID,
            string SingleOrMultiParID,
            string StructureStatusParID,
            string SubholdingID,
            string ExplorationAreaParID,
            string ExplorationTypeParID,
            string Play,
            string UDSubTypeParID,
            string UDClassificationParID,
            string RegionalID,
            string UDSubClassificationParID,
            string ZonaID,
            string EffectiveYear,
            string BlockID,
            string APHID,
            string AssetID,
            string BasinID,
            string prosResourcesTarget,
            string prosResources,
            string recoverableOption,
            string currentPG,
            string expectedPG,
            string contResources,
            string drillingResources,
            string economicxBlockStatusParID,
            string economicOperatorshipStatusParID,
            string economicAwardDate,
            string economicExpiredDate,
            string economicDevConcept,
            string economicEconomicAssumption,
            string economicCAPEX,
            string economicOPEXProduction,
            string economicOPEXFacility,
            string economicASR,
            string economicEconomicResult,
            string economicContractorNPV,
            string economicIRR,
            string economicContractorPOT,
            string economicPIncome,
            string economicEMV,
            string economicNPV)
        {
            try
            {
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                var transNumber = "";
                var datasUserAuth = Task.Run(() => _servicePL.GetParamDescByParamListID("XAuth")).Result;
                if (!string.IsNullOrEmpty(datasUserAuth))
                {
                    using (var client = new HttpClient())
                    {
                        var datasAPPFK = Task.Run(() => _servicePL.GetParamDescByParamListID("AppFK")).Result;
                        var datasWF = Task.Run(() => _servicePL.GetParamDescByParamListID("WFCodePK")).Result;
                        if (!string.IsNullOrEmpty(datasAPPFK) && !string.IsNullOrEmpty(datasWF))
                        {
                            var actionStr = "Save";
                            var userData = await _userService.GetUserInfo(user);
                            foreach (var item in userData.Roles)
                            {
                                if (item.Value.Trim() == "SUPER ADMIN")
                                {
                                    actionStr = "Save4";
                                }
                            }
                            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                            {
                                Path = AimanConstant.TransactionWorkflow
                            };
                            client.DefaultRequestHeaders.Add("X-User-Auth", datasUserAuth);
                            client.BaseAddress = uriBuilder.Uri;
                            string param = string.Format(@"?appId={0}&companyCode=5000&transNo={2}&startWF={3}&action={4}&actionFor={5}&actionBy={6}&source={7}
                                                     &notes={8}&additionalData={9}",
                                                     datasAPPFK,
                                                     "5000",
                                                     "1",
                                                     datasWF,
                                                     actionStr,
                                                     user,
                                                     user,
                                                     "Web Exploration", "Save Data", "-");

                            var responseTask = client.PostAsync(uriBuilder.Uri.ToString().Split('\\')[uriBuilder.Uri.ToString().Split('\\').Length - 1] + param, null);
                            responseTask.Wait();

                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();

                                DoTransactionResult DoTransRes = JsonConvert.DeserializeObject<DoTransactionResult>(readTask.Result);
                                if (!string.IsNullOrEmpty(DoTransRes.Object.Value))
                                {
                                    transNumber = DoTransRes.Object.Value;
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed" });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Workflow Failed" });
                        }
                    }

                    if (!string.IsNullOrEmpty(transNumber))
                    {
                        //var userTmp = await _userService.GetCurrentUserInfo();
                        var structureID = await _serviceES.GenerateNewID();
                        MDExplorationStructureDto explorStructureObj = new MDExplorationStructureDto();
                        explorStructureObj.xStructureID = structureID;
                        explorStructureObj.xStructureName = StructureName;
                        explorStructureObj.xStructureStatusParID = StructureStatusParID;
                        explorStructureObj.SingleOrMultiParID = SingleOrMultiParID;
                        explorStructureObj.ExplorationTypeParID = ExplorationTypeParID;
                        explorStructureObj.SubholdingID = SubholdingID;
                        explorStructureObj.BasinID = BasinID;
                        explorStructureObj.RegionalID = RegionalID;
                        explorStructureObj.ZonaID = ZonaID;
                        explorStructureObj.APHID = APHID;
                        explorStructureObj.xBlockID = BlockID;
                        explorStructureObj.xAssetID = AssetID;
                        explorStructureObj.xAreaID = ExplorationAreaParID;
                        explorStructureObj.UDClassificationParID = UDClassificationParID;
                        explorStructureObj.UDSubClassificationParID = UDSubClassificationParID;
                        explorStructureObj.UDSubTypeParID = UDSubTypeParID;
                        explorStructureObj.ExplorationAreaParID = ExplorationAreaParID;
                        explorStructureObj.CountriesID = CountriesID;
                        explorStructureObj.Play = Play;
                        explorStructureObj.MadamTransID = transNumber;
                        explorStructureObj.StatusData = "Draft";
                        explorStructureObj.CreatedDate = DateTime.Now;
                        //explorStructureObj.CreatedBy = userTmp.Name;
                        explorStructureObj.CreatedBy = user;
                        await _serviceES.Create(explorStructureObj);
                        if (explorStructureObj.xStructureStatusParID.Trim() == "1" || explorStructureObj.xStructureStatusParID.Trim() == "2" || explorStructureObj.xStructureStatusParID.Trim() == "3")
                        {
                            Root prosResourceTarget = JsonConvert.DeserializeObject<Root>(prosResourcesTarget);
                            if (prosResourceTarget.rows.Count() > 0)
                            {
                                foreach (var pt in prosResourceTarget.rows)
                                {
                                    pt.xStructureID = structureID;
                                    pt.TargetID = await _servicePT.GenerateNewID();
                                    pt.P10InPlaceOilUoM = "MBO";
                                    pt.P90InPlaceOilUoM = "MBO";
                                    pt.P50InPlaceOilUoM = "MBO";
                                    pt.PMeanInPlaceOilUoM = "MBO";
                                    pt.P90InPlaceGasUoM = "MMSCF";
                                    pt.P50InPlaceGasUoM = "MMSCF";
                                    pt.PMeanInPlaceGasUoM = "MMSCF";
                                    pt.P10InPlaceGasUoM = "MMSCF";
                                    pt.P90InPlaceTotalUoM = "MBOE";
                                    pt.P50InPlaceTotalUoM = "MBOE";
                                    pt.PMeanInPlaceTotalUoM = "MBOE";
                                    pt.P10InPlaceTotalUoM = "MBOE";
                                    pt.P90RecoverableOilUoM = "MBO";
                                    pt.P50RecoverableOilUoM = "MBO";
                                    pt.PMeanRecoverableOilUoM = "MBO";
                                    pt.P10RecoverableOilUoM = "MBO";
                                    pt.P90RecoverableGasUoM = "MMSCF";
                                    pt.P50RecoverableGasUoM = "MMSCF";
                                    pt.PMeanRecoverableGasUoM = "MMSCF";
                                    pt.P10RecoverableGasUoM = "MMSCF";
                                    pt.P90RecoverableTotalUoM = "MBOE";
                                    pt.P50RecoverableTotalUoM = "MBOE";
                                    pt.PMeanRecoverableTotalUoM = "MBOE";
                                    pt.P10RecoverableTotalUoM = "MBOE";
                                    pt.GCFClosureUoM = "%";
                                    pt.GCFContainmentUoM = "%";
                                    pt.GCFPGTotalUoM = "%";
                                    pt.GCFReservoirUoM = "%";
                                    pt.GCFSRUoM = "%";
                                    pt.GCFTMUoM = "%";
                                    pt.RFOil = pt.RFOil / 100;
                                    pt.RFGas = pt.RFGas / 100;
                                    pt.GCFSR = pt.GCFSR / 100;
                                    pt.GCFTM = pt.GCFTM / 100;
                                    pt.GCFReservoir = pt.GCFReservoir / 100;
                                    pt.GCFClosure = pt.GCFClosure / 100;
                                    pt.GCFContainment = pt.GCFContainment / 100;
                                    pt.GCFPGTotal = pt.GCFPGTotal / 100;
                                    pt.CreatedDate = DateTime.Now;
                                    pt.CreatedBy = user;
                                    await _servicePT.Create(pt);
                                }
                            }
                            RootProsResources prosResource = JsonConvert.DeserializeObject<RootProsResources>(prosResources);
                            if (prosResource.rows.Count() > 0)
                            {
                                foreach (var pr in prosResource.rows)
                                {
                                    pr.xStructureID = structureID;
                                    pr.P10InPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceOilPRUoM = "MBO";
                                    pr.P50InPlaceOilPRUoM = "MBO";
                                    pr.PMeanInPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceGasPRUoM = "MMSCF";
                                    pr.P50InPlaceGasPRUoM = "MMSCF";
                                    pr.PMeanInPlaceGasPRUoM = "MMSCF";
                                    pr.P10InPlaceGasPRUoM = "MMSCF";
                                    pr.P90InPlaceTotalPRUoM = "MBOE";
                                    pr.P50InPlaceTotalPRUoM = "MBOE";
                                    pr.PMeanInPlaceTotalPRUoM = "MBOE";
                                    pr.P10InPlaceTotalPRUoM = "MBOE";
                                    pr.P90RROilUoM = "MBO";
                                    pr.P50RROilUoM = "MBO";
                                    pr.PMeanRROilUoM = "MBO";
                                    pr.P10RROilUoM = "MBO";
                                    pr.P90RRGasUoM = "MMSCF";
                                    pr.P50RRGasUoM = "MMSCF";
                                    pr.PMeanRRGasUoM = "MMSCF";
                                    pr.P10RRGasUoM = "MMSCF";
                                    pr.P90RRTotalUoM = "MBOE";
                                    pr.P50RRTotalUoM = "MBOE";
                                    pr.PMeanRRTotalUoM = "MBOE";
                                    pr.P10RRTotalUoM = "MBOE";
                                    pr.GCFClosurePRUoM = "%";
                                    pr.GCFContainmentPRUoM = "%";
                                    pr.GCFPGTotalPRUoM = "%";
                                    pr.GCFReservoirPRUoM = "%";
                                    pr.GCFSRPRUoM = "%";
                                    pr.GCFTMPRUoM = "%";
                                    if (expectedPG.Trim() == "")
                                    {
                                        expectedPG = "0";
                                    }
                                    if (currentPG.Trim() == "")
                                    {
                                        currentPG = "0";
                                    }
                                    pr.RFOilPR = pr.RFOilPR / 100;
                                    pr.RFGasPR = pr.RFGasPR / 100;
                                    pr.GCFSRPR = pr.GCFSRPR / 100;
                                    pr.GCFTMPR = pr.GCFTMPR / 100;
                                    pr.GCFReservoirPR = pr.GCFReservoirPR / 100;
                                    pr.GCFClosurePR = pr.GCFClosurePR / 100;
                                    pr.GCFContainmentPR = pr.GCFContainmentPR / 100;
                                    pr.GCFPGTotalPR = pr.GCFPGTotalPR / 100;
                                    pr.ExpectedPG = decimal.Parse(expectedPG, CultureInfo.InvariantCulture) / 100;
                                    pr.CurrentPG = decimal.Parse(currentPG, CultureInfo.InvariantCulture) / 100;
                                    pr.MethodParID = recoverableOption;
                                    pr.CreatedDate = DateTime.Now;
                                    pr.CreatedBy = user;
                                    await _servicePR.Create(pr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    //TXDrillingDto drlObj = new TXDrillingDto();
                                    dr.xStructureID = structureID;
                                    dr.xWellID = wellObj.xWellID;
                                    dr.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        dr.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        dr.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    dr.ExpectedPG = dr.ExpectedPG / 100;
                                    dr.CurrentPG = dr.CurrentPG / 100;
                                    dr.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    dr.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    dr.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    dr.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    dr.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    dr.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PlayOpener = true;
                                    }
                                    else
                                    {
                                        dr.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        dr.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        dr.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        dr.PotentialDelay = false;
                                    }
                                    dr.DrillingCostCurr = "MMUSD";
                                    dr.DrillingCostDHBCurr = "MMUSD";
                                    dr.P90ResourceOilUoM = "MMBO";
                                    dr.P50ResourceOilUoM = "MMBO";
                                    dr.P10ResourceOilUoM = "MMBO";
                                    dr.P90ResourceGasUoM = "BCF";
                                    dr.P50ResourceGasUoM = "BCF";
                                    dr.P10ResourceGasUoM = "BCF";
                                    dr.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.CreatedDate = DateTime.Now;
                                    dr.CreatedBy = user;
                                    await _serviceDR.Create(dr);
                                }
                            }
                        }
                        else
                        {
                            RootContResources contResource = JsonConvert.DeserializeObject<RootContResources>(contResources);
                            if (contResource.rows.Count() > 0)
                            {
                                foreach (var cr in contResource.rows)
                                {
                                    cr.xStructureID = structureID;
                                    cr.C1COilUoM = "MMBOE";
                                    cr.C2COilUoM = "MMBOE";
                                    cr.C3COilUoM = "MMBOE";
                                    cr.C1CGasUoM = "MMSCF";
                                    cr.C2CGasUoM = "MMSCF";
                                    cr.C3CGasUoM = "MMSCF";
                                    cr.C1CTotalUoM = "MBOE";
                                    cr.C2CTotalUoM = "MBOE";
                                    cr.C3CTotalUoM = "MBOE";
                                    cr.CreatedDate = DateTime.Now;
                                    cr.CreatedBy = user;
                                    await _serviceCR.Create(cr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    //TXDrillingDto drlObj = new TXDrillingDto();
                                    dr.xStructureID = structureID;
                                    dr.xWellID = wellObj.xWellID;
                                    dr.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        dr.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        dr.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    dr.ExpectedPG = dr.ExpectedPG / 100;
                                    dr.CurrentPG = dr.CurrentPG / 100;
                                    dr.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    dr.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    dr.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    dr.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    dr.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    dr.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PlayOpener = true;
                                    }
                                    else
                                    {
                                        dr.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        dr.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        dr.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        dr.PotentialDelay = false;
                                    }
                                    dr.DrillingCostCurr = "MMUSD";
                                    dr.DrillingCostDHBCurr = "MMUSD";
                                    dr.P90ResourceOilUoM = "MMBO";
                                    dr.P50ResourceOilUoM = "MMBO";
                                    dr.P10ResourceOilUoM = "MMBO";
                                    dr.P90ResourceGasUoM = "BCF";
                                    dr.P50ResourceGasUoM = "BCF";
                                    dr.P10ResourceGasUoM = "BCF";
                                    dr.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.CreatedDate = DateTime.Now;
                                    dr.CreatedBy = user;
                                    await _serviceDR.Create(dr);
                                }
                            }
                        }
                        TXEconomicDto ecoObj = new TXEconomicDto();
                        ecoObj.xStructureID = structureID;
                        ecoObj.DevConcept = economicDevConcept;
                        ecoObj.EconomicAssumption = economicEconomicAssumption;
                        if (!string.IsNullOrEmpty(economicCAPEX))
                        {
                            ecoObj.CAPEX = decimal.Parse(economicCAPEX);
                        }
                        else
                        {
                            ecoObj.CAPEX = 0;
                        }
                        ecoObj.CAPEXCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicOPEXProduction))
                        {
                            ecoObj.OPEXProduction = decimal.Parse(economicOPEXProduction);
                        }
                        else
                        {
                            ecoObj.OPEXProduction = 0;
                        }
                        ecoObj.OPEXProductionCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicOPEXFacility))
                        {
                            ecoObj.OPEXFacility = decimal.Parse(economicOPEXFacility);
                        }
                        else
                        {
                            ecoObj.OPEXFacility = 0;
                        }
                        ecoObj.OPEXFacilityCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicASR))
                        {
                            ecoObj.ASR = decimal.Parse(economicASR);
                        }
                        else
                        {
                            ecoObj.ASR = 0;
                        }
                        ecoObj.ASRCurr = "MMUSD";
                        ecoObj.EconomicResult = economicEconomicResult;
                        if (!string.IsNullOrEmpty(economicContractorNPV))
                        {
                            ecoObj.ContractorNPV = decimal.Parse(economicContractorNPV);
                        }
                        else
                        {
                            ecoObj.ContractorNPV = 0;
                        }
                        ecoObj.ContractorNPVCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicIRR))
                        {
                            ecoObj.IRR = decimal.Parse(economicIRR) / 100;
                        }
                        else
                        {
                            ecoObj.IRR = 0;
                        }
                        if (!string.IsNullOrEmpty(economicContractorPOT))
                        {
                            ecoObj.ContractorPOT = decimal.Parse(economicContractorPOT);
                        }
                        else
                        {
                            ecoObj.ContractorPOT = 0;
                        }
                        ecoObj.ContractorPOTUoM = "Years";
                        if (!string.IsNullOrEmpty(economicPIncome))
                        {
                            ecoObj.PIncome = decimal.Parse(economicPIncome);
                        }
                        else
                        {
                            ecoObj.PIncome = 0;
                        }
                        ecoObj.PIncomeCurr = "Uniteless";
                        if (!string.IsNullOrEmpty(economicEMV))
                        {
                            ecoObj.EMV = decimal.Parse(economicEMV);
                        }
                        else
                        {
                            ecoObj.EMV = 0;
                        }
                        ecoObj.EMVCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicNPV))
                        {
                            ecoObj.NPV = decimal.Parse(economicNPV);
                        }
                        else
                        {
                            ecoObj.NPV = 0;
                        }
                        ecoObj.NPVCurr = "MMUSD";
                        ecoObj.CreatedDate = DateTime.Now;
                        ecoObj.CreatedBy = user;
                        await _serviceEC.Create(ecoObj);
                        //insert log activity
                        LGActivityDto activityObj = new LGActivityDto();
                        string hostName = Dns.GetHostName();
                        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                        activityObj.IP = myIP;
                        activityObj.Menu = "Exploration Structure";
                        activityObj.Action = "Create";
                        activityObj.TransactionID = structureID;
                        activityObj.Status = explorStructureObj.StatusData;
                        activityObj.CreatedDate = DateTime.Now;
                        activityObj.CreatedBy = user;
                        await _serviceAC.Create(activityObj);
                        return Json(new { success = true, structureid = structureID });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Workflow Failed" });
                    }

                }
                else
                {
                    return Json(new { success = false, Message = "Workflow Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        [HttpPost]
        [Obsolete]
        public async Task<JsonResult> SaveUpdateAll(string StructureName,
            string StructureID,
            string CountriesID,
            string SingleOrMultiParID,
            string StructureStatusParID,
            string SubholdingID,
            string ExplorationAreaParID,
            string ExplorationTypeParID,
            string Play,
            string UDSubTypeParID,
            string UDClassificationParID,
            string RegionalID,
            string UDSubClassificationParID,
            string ZonaID,
            string EffectiveYear,
            string BlockID,
            string APHID,
            string AssetID,
            string BasinID,
            string prosResourcesTarget,
            string prosResources,
            string recoverableOption,
            string currentPG,
            string expectedPG,
            string contResources,
            string drillingResources,
            string economicxBlockStatusParID,
            string economicOperatorshipStatusParID,
            string economicAwardDate,
            string economicExpiredDate,
            string economicDevConcept,
            string economicEconomicAssumption,
            string economicCAPEX,
            string economicOPEXProduction,
            string economicOPEXFacility,
            string economicASR,
            string economicEconomicResult,
            string economicContractorNPV,
            string economicIRR,
            string economicContractorPOT,
            string economicPIncome,
            string economicEMV,
            string economicNPV)
        {
            try
            {
                //var userTmp = await _userService.GetCurrentUserInfo();
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;

                var explorationStructure = Task.Run(() => _serviceES.GetOne(StructureID)).Result;

                var transNumber = "";
                var datasUserAuth = Task.Run(() => _servicePL.GetParamDescByParamListID("XAuth")).Result;
                if (!string.IsNullOrEmpty(datasUserAuth))
                {
                    using (var client = new HttpClient())
                    {
                        var datasAPPFK = Task.Run(() => _servicePL.GetParamDescByParamListID("AppFK")).Result;
                        var datasWF = Task.Run(() => _servicePL.GetParamDescByParamListID("WFCodePK")).Result;
                        if (!string.IsNullOrEmpty(datasAPPFK) && !string.IsNullOrEmpty(datasWF))
                        {
                            var actionStr = "Save";
                            var userData = await _userService.GetUserInfo(user);
                            if (explorationStructure.StatusData.Trim() == "Reject Submitted" || explorationStructure.StatusData.Trim() == "Reject Revision")
                            {
                                actionStr = "Reject";
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER_ADMIN")
                                    {
                                        actionStr = "Reject2";
                                    }
                                }
                            }
                            else
                            {
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER_ADMIN")
                                    {
                                        actionStr = "Save4";
                                    }
                                }
                            }
                            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                            {
                                Path = AimanConstant.TransactionWorkflow
                            };
                            client.DefaultRequestHeaders.Add("X-User-Auth", datasUserAuth);
                            client.BaseAddress = uriBuilder.Uri;
                            string param = "";
                            if (!string.IsNullOrEmpty(explorationStructure.MadamTransID.Trim()))
                            {
                                param = string.Format(@"?appId={0}&companyCode=5000&transNo={2}&startWF={3}&action={4}&actionFor={5}&actionBy={6}&source={7}
                                                     &notes={8}&additionalData={9}",
                                                        datasAPPFK,
                                                        "5000",
                                                        explorationStructure.MadamTransID.Trim(),
                                                        "",
                                                        actionStr,
                                                        user,
                                                        user,
                                                        "Web Exploration", "Save Data", "-");
                            }
                            else
                            {
                                param = string.Format(@"?appId={0}&companyCode=5000&transNo={2}&startWF={3}&action={4}&actionFor={5}&actionBy={6}&source={7}
                                                     &notes={8}&additionalData={9}",
                                                        datasAPPFK,
                                                        "5000",
                                                        "1",
                                                        datasWF,
                                                        actionStr,
                                                        user,
                                                        user,
                                                        "Web Exploration", "Save Data", "-");
                            }

                            var responseTask = client.PostAsync(uriBuilder.Uri.ToString().Split('\\')[uriBuilder.Uri.ToString().Split('\\').Length - 1] + param, null);
                            responseTask.Wait();

                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();

                                DoTransactionResult DoTransRes = JsonConvert.DeserializeObject<DoTransactionResult>(readTask.Result);
                                if (!string.IsNullOrEmpty(DoTransRes.Object.Value))
                                {
                                    transNumber = DoTransRes.Object.Value;
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed" });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Workflow Failed" });
                        }
                    }

                    if (!string.IsNullOrEmpty(transNumber))
                    {
                        explorationStructure.xStructureName = StructureName;
                        explorationStructure.xStructureStatusParID = StructureStatusParID;
                        explorationStructure.SingleOrMultiParID = SingleOrMultiParID;
                        explorationStructure.ExplorationTypeParID = ExplorationTypeParID;
                        explorationStructure.SubholdingID = SubholdingID;
                        explorationStructure.BasinID = BasinID;
                        explorationStructure.RegionalID = RegionalID;
                        explorationStructure.ZonaID = ZonaID;
                        explorationStructure.APHID = APHID;
                        explorationStructure.xBlockID = BlockID;
                        explorationStructure.xAssetID = AssetID;
                        explorationStructure.xAreaID = ExplorationAreaParID;
                        explorationStructure.UDClassificationParID = UDClassificationParID;
                        explorationStructure.UDSubClassificationParID = UDSubClassificationParID;
                        explorationStructure.UDSubTypeParID = UDSubTypeParID;
                        explorationStructure.ExplorationAreaParID = ExplorationAreaParID;
                        explorationStructure.CountriesID = CountriesID;
                        explorationStructure.Play = Play;
                        explorationStructure.MadamTransID = transNumber;
                        if (explorationStructure.StatusData.Trim() == "Released" || explorationStructure.StatusData.Trim() == "Reject Revision")
                        {
                            explorationStructure.StatusData = "Released";
                        }
                        else
                        {
                            explorationStructure.StatusData = "Draft";
                        }
                        //explorationStructure.CreatedDate = DateTime.Now;
                        //explorationStructure.CreatedBy = user;
                        explorationStructure.UpdatedDate = DateTime.Now;
                        explorationStructure.UpdatedBy = user;
                        await _serviceES.Update(explorationStructure);
                        if (explorationStructure.xStructureStatusParID.Trim() == "1" || explorationStructure.xStructureStatusParID.Trim() == "2" || explorationStructure.xStructureStatusParID.Trim() == "3")
                        {
                            Root prosResourceTarget = JsonConvert.DeserializeObject<Root>(prosResourcesTarget);
                            if (prosResourceTarget.rows.Count() > 0)
                            {
                                var datas = Task.Run(() => _servicePT.GetProsResourceTargetByStructureID(StructureID)).Result;
                                foreach (var dt in datas)
                                {
                                    await _servicePT.Destroy(dt.TargetID);
                                }
                                foreach (var pt in prosResourceTarget.rows)
                                {
                                    pt.xStructureID = StructureID;
                                    pt.TargetID = await _servicePT.GenerateNewID();
                                    pt.P10InPlaceOilUoM = "MBO";
                                    pt.P90InPlaceOilUoM = "MBO";
                                    pt.P50InPlaceOilUoM = "MBO";
                                    pt.PMeanInPlaceOilUoM = "MBO";
                                    pt.P90InPlaceGasUoM = "MMSCF";
                                    pt.P50InPlaceGasUoM = "MMSCF";
                                    pt.PMeanInPlaceGasUoM = "MMSCF";
                                    pt.P10InPlaceGasUoM = "MMSCF";
                                    pt.P90InPlaceTotalUoM = "MBOE";
                                    pt.P50InPlaceTotalUoM = "MBOE";
                                    pt.PMeanInPlaceTotalUoM = "MBOE";
                                    pt.P10InPlaceTotalUoM = "MBOE";
                                    pt.P90RecoverableOilUoM = "MBO";
                                    pt.P50RecoverableOilUoM = "MBO";
                                    pt.PMeanRecoverableOilUoM = "MBO";
                                    pt.P10RecoverableOilUoM = "MBO";
                                    pt.P90RecoverableGasUoM = "MMSCF";
                                    pt.P50RecoverableGasUoM = "MMSCF";
                                    pt.PMeanRecoverableGasUoM = "MMSCF";
                                    pt.P10RecoverableGasUoM = "MMSCF";
                                    pt.P90RecoverableTotalUoM = "MBOE";
                                    pt.P50RecoverableTotalUoM = "MBOE";
                                    pt.PMeanRecoverableTotalUoM = "MBOE";
                                    pt.P10RecoverableTotalUoM = "MBOE";
                                    pt.GCFClosureUoM = "%";
                                    pt.GCFContainmentUoM = "%";
                                    pt.GCFPGTotalUoM = "%";
                                    pt.GCFReservoirUoM = "%";
                                    pt.GCFSRUoM = "%";
                                    pt.GCFTMUoM = "%";
                                    pt.RFOil = pt.RFOil / 100;
                                    pt.RFGas = pt.RFGas / 100;
                                    pt.GCFSR = pt.GCFSR / 100;
                                    pt.GCFTM = pt.GCFTM / 100;
                                    pt.GCFReservoir = pt.GCFReservoir / 100;
                                    pt.GCFClosure = pt.GCFClosure / 100;
                                    pt.GCFContainment = pt.GCFContainment / 100;
                                    pt.GCFPGTotal = pt.GCFPGTotal / 100;
                                    pt.CreatedDate = DateTime.Now;
                                    pt.CreatedBy = user;
                                    await _servicePT.Create(pt);

                                }
                            }
                            RootProsResources prosResource = JsonConvert.DeserializeObject<RootProsResources>(prosResources);
                            if (prosResource.rows.Count() > 0)
                            {
                                var datas = Task.Run(() => _servicePR.GetOne(StructureID)).Result;
                                if (!string.IsNullOrEmpty(datas.xStructureID))
                                {
                                    await _servicePR.Destroy(datas.xStructureID);
                                }
                                foreach (var pr in prosResource.rows)
                                {
                                    pr.xStructureID = StructureID;
                                    pr.P10InPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceOilPRUoM = "MBO";
                                    pr.P50InPlaceOilPRUoM = "MBO";
                                    pr.PMeanInPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceGasPRUoM = "MMSCF";
                                    pr.P50InPlaceGasPRUoM = "MMSCF";
                                    pr.PMeanInPlaceGasPRUoM = "MMSCF";
                                    pr.P10InPlaceGasPRUoM = "MMSCF";
                                    pr.P90InPlaceTotalPRUoM = "MBOE";
                                    pr.P50InPlaceTotalPRUoM = "MBOE";
                                    pr.PMeanInPlaceTotalPRUoM = "MBOE";
                                    pr.P10InPlaceTotalPRUoM = "MBOE";
                                    pr.P90RROilUoM = "MBO";
                                    pr.P50RROilUoM = "MBO";
                                    pr.PMeanRROilUoM = "MBO";
                                    pr.P10RROilUoM = "MBO";
                                    pr.P90RRGasUoM = "MMSCF";
                                    pr.P50RRGasUoM = "MMSCF";
                                    pr.PMeanRRGasUoM = "MMSCF";
                                    pr.P10RRGasUoM = "MMSCF";
                                    pr.P90RRTotalUoM = "MBOE";
                                    pr.P50RRTotalUoM = "MBOE";
                                    pr.PMeanRRTotalUoM = "MBOE";
                                    pr.P10RRTotalUoM = "MBOE";
                                    pr.GCFClosurePRUoM = "%";
                                    pr.GCFContainmentPRUoM = "%";
                                    pr.GCFPGTotalPRUoM = "%";
                                    pr.GCFReservoirPRUoM = "%";
                                    pr.GCFSRPRUoM = "%";
                                    pr.GCFTMPRUoM = "%";
                                    if (expectedPG.Trim() == "")
                                    {
                                        expectedPG = "0";
                                    }
                                    if (currentPG.Trim() == "")
                                    {
                                        currentPG = "0";
                                    }
                                    pr.RFOilPR = pr.RFOilPR / 100;
                                    pr.RFGasPR = pr.RFGasPR / 100;
                                    pr.GCFSRPR = pr.GCFSRPR / 100;
                                    pr.GCFTMPR = pr.GCFTMPR / 100;
                                    pr.GCFReservoirPR = pr.GCFReservoirPR / 100;
                                    pr.GCFClosurePR = pr.GCFClosurePR / 100;
                                    pr.GCFContainmentPR = pr.GCFContainmentPR / 100;
                                    pr.GCFPGTotalPR = pr.GCFPGTotalPR / 100;
                                    pr.ExpectedPG = decimal.Parse(expectedPG, CultureInfo.InvariantCulture) / 100;
                                    pr.CurrentPG = decimal.Parse(currentPG, CultureInfo.InvariantCulture) / 100;
                                    pr.MethodParID = recoverableOption;
                                    pr.CreatedDate = DateTime.Now;
                                    pr.CreatedBy = user;
                                    await _servicePR.Create(pr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                var datas = Task.Run(() => _serviceDR.GetDrillingByStructureID(StructureID)).Result;
                                foreach (var dt in datas)
                                {
                                    await _serviceWL.Destroy(dt.xWellID);
                                }
                                foreach (var dt in datas)
                                {
                                    await _serviceDR.Destroy(dt.xStructureID, dt.xWellID);
                                }
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    TXDrillingDto drlObj = new TXDrillingDto();
                                    drlObj.xStructureID = StructureID;
                                    drlObj.xWellID = wellObj.xWellID;
                                    drlObj.WaterDepthMeter = dr.WaterDepthMeter;
                                    drlObj.WaterDepthFeet = dr.WaterDepthFeet;
                                    drlObj.TotalDepthMeter = dr.TotalDepthMeter;
                                    drlObj.TotalDepthFeet = dr.TotalDepthFeet;
                                    drlObj.SurfaceLocationLatitude = dr.SurfaceLocationLatitude;
                                    drlObj.SurfaceLocationLongitude = dr.SurfaceLocationLongitude;
                                    drlObj.DrillingCost = dr.DrillingCost;
                                    drlObj.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        drlObj.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        drlObj.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    drlObj.DrillingCostDHB = dr.DrillingCostDHB;
                                    drlObj.OperationalContextParId = dr.OperationalContextParId;
                                    drlObj.Location = dr.Location;
                                    drlObj.DrillingCompletionPeriod = dr.DrillingCompletionPeriod;
                                    drlObj.ExpectedPG = dr.ExpectedPG / 100;
                                    drlObj.CurrentPG = dr.CurrentPG / 100;
                                    drlObj.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    drlObj.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    drlObj.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    drlObj.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    drlObj.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    drlObj.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PlayOpener = true;
                                    }
                                    else
                                    {
                                        drlObj.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        drlObj.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        drlObj.PotentialDelay = false;
                                    }
                                    drlObj.DrillingCostDHBCurr = "MMUSD";
                                    drlObj.DrillingCostCurr = "MMUSD";
                                    drlObj.P90ResourceOilUoM = "MMBO";
                                    drlObj.P50ResourceOilUoM = "MMBO";
                                    drlObj.P10ResourceOilUoM = "MMBO";
                                    drlObj.P90ResourceGasUoM = "BCF";
                                    drlObj.P50ResourceGasUoM = "BCF";
                                    drlObj.P10ResourceGasUoM = "BCF";
                                    drlObj.P90ResourceOil = dr.P90ResourceOil;
                                    drlObj.P50ResourceOil = dr.P50ResourceOil;
                                    drlObj.P10ResourceOil = dr.P10ResourceOil;
                                    drlObj.P90ResourceGas = dr.P90ResourceGas;
                                    drlObj.P50ResourceGas = dr.P50ResourceGas;
                                    drlObj.P10ResourceGas = dr.P10ResourceGas;
                                    drlObj.P90NPVProfitabilityOil = dr.P90NPVProfitabilityOil;
                                    drlObj.P50NPVProfitabilityOil = dr.P50NPVProfitabilityOil;
                                    drlObj.P10NPVProfitabilityOil = dr.P10NPVProfitabilityOil;
                                    drlObj.P90NPVProfitabilityGas = dr.P90NPVProfitabilityGas;
                                    drlObj.P50NPVProfitabilityGas = dr.P50NPVProfitabilityGas;
                                    drlObj.P10NPVProfitabilityGas = dr.P10NPVProfitabilityGas;
                                    drlObj.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.CreatedDate = DateTime.Now;
                                    drlObj.CreatedBy = user;
                                    await _serviceDR.Create(drlObj);
                                }
                            }
                        }
                        else
                        {
                            RootContResources contResource = JsonConvert.DeserializeObject<RootContResources>(contResources);
                            if (contResource.rows.Count() > 0)
                            {
                                var datas = Task.Run(() => _serviceCR.GetContResourceTargetByStructureID(StructureID)).Result;
                                if (datas != null)
                                {
                                    await _serviceCR.Destroy(datas.xStructureID);
                                }
                                foreach (var cr in contResource.rows)
                                {
                                    cr.xStructureID = StructureID;
                                    cr.C1COilUoM = "MMBOE";
                                    cr.C2COilUoM = "MMBOE";
                                    cr.C3COilUoM = "MMBOE";
                                    cr.C1CGasUoM = "MMSCF";
                                    cr.C2CGasUoM = "MMSCF";
                                    cr.C3CGasUoM = "MMSCF";
                                    cr.C1CTotalUoM = "MBOE";
                                    cr.C2CTotalUoM = "MBOE";
                                    cr.C3CTotalUoM = "MBOE";
                                    cr.CreatedDate = DateTime.Now;
                                    cr.CreatedBy = user;
                                    await _serviceCR.Create(cr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                var datas = Task.Run(() => _serviceDR.GetDrillingByStructureID(StructureID)).Result;
                                foreach (var dt in datas)
                                {
                                    await _serviceWL.Destroy(dt.xWellID);
                                }
                                foreach (var dt in datas)
                                {
                                    await _serviceDR.Destroy(dt.xStructureID, dt.xWellID);
                                }
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    TXDrillingDto drlObj = new TXDrillingDto();
                                    drlObj.xStructureID = StructureID;
                                    drlObj.xWellID = wellObj.xWellID;
                                    drlObj.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    drlObj.WaterDepthMeter = dr.WaterDepthMeter;
                                    drlObj.WaterDepthFeet = dr.WaterDepthFeet;
                                    drlObj.TotalDepthMeter = dr.TotalDepthMeter;
                                    drlObj.TotalDepthFeet = dr.TotalDepthFeet;
                                    drlObj.SurfaceLocationLatitude = dr.SurfaceLocationLatitude;
                                    drlObj.SurfaceLocationLongitude = dr.SurfaceLocationLongitude;
                                    drlObj.DrillingCost = dr.DrillingCost;
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        drlObj.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        drlObj.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    drlObj.DrillingCostDHB = dr.DrillingCostDHB;
                                    drlObj.OperationalContextParId = dr.OperationalContextParId;
                                    drlObj.Location = dr.Location;
                                    drlObj.DrillingCompletionPeriod = dr.DrillingCompletionPeriod;
                                    drlObj.ExpectedPG = dr.ExpectedPG / 100;
                                    drlObj.CurrentPG = dr.CurrentPG / 100;
                                    drlObj.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    drlObj.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    drlObj.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    drlObj.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    drlObj.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    drlObj.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PlayOpener = true;
                                    }
                                    else
                                    {
                                        drlObj.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        drlObj.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        drlObj.PotentialDelay = false;
                                    }
                                    drlObj.DrillingCostDHBCurr = "MMUSD";
                                    drlObj.DrillingCostCurr = "MMUSD";
                                    drlObj.P90ResourceOilUoM = "MMBO";
                                    drlObj.P50ResourceOilUoM = "MMBO";
                                    drlObj.P10ResourceOilUoM = "MMBO";
                                    drlObj.P90ResourceGasUoM = "BCF";
                                    drlObj.P50ResourceGasUoM = "BCF";
                                    drlObj.P10ResourceGasUoM = "BCF";
                                    drlObj.P90ResourceOil = dr.P90ResourceOil;
                                    drlObj.P50ResourceOil = dr.P50ResourceOil;
                                    drlObj.P10ResourceOil = dr.P10ResourceOil;
                                    drlObj.P90ResourceGas = dr.P90ResourceGas;
                                    drlObj.P50ResourceGas = dr.P50ResourceGas;
                                    drlObj.P10ResourceGas = dr.P10ResourceGas;
                                    drlObj.P90NPVProfitabilityOil = dr.P90NPVProfitabilityOil;
                                    drlObj.P50NPVProfitabilityOil = dr.P50NPVProfitabilityOil;
                                    drlObj.P10NPVProfitabilityOil = dr.P10NPVProfitabilityOil;
                                    drlObj.P90NPVProfitabilityGas = dr.P90NPVProfitabilityGas;
                                    drlObj.P50NPVProfitabilityGas = dr.P50NPVProfitabilityGas;
                                    drlObj.P10NPVProfitabilityGas = dr.P10NPVProfitabilityGas;
                                    drlObj.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.CreatedDate = DateTime.Now;
                                    drlObj.CreatedBy = user;
                                    await _serviceDR.Create(drlObj);
                                }
                            }
                        }

                        var explorationEco = Task.Run(() => _serviceEC.GetOne(StructureID)).Result;
                        if (string.IsNullOrEmpty(explorationEco.xStructureID))
                        {
                            TXEconomicDto ecoObj = new TXEconomicDto();
                            ecoObj.xStructureID = StructureID;
                            ecoObj.DevConcept = economicDevConcept;
                            ecoObj.EconomicAssumption = economicEconomicAssumption;
                            ecoObj.CAPEX = decimal.Parse(economicCAPEX);
                            ecoObj.CAPEXCurr = "MMUSD";
                            ecoObj.OPEXProduction = decimal.Parse(economicOPEXProduction);
                            ecoObj.OPEXProductionCurr = "MMUSD";
                            ecoObj.OPEXFacility = decimal.Parse(economicOPEXFacility);
                            ecoObj.OPEXFacilityCurr = "MMUSD";
                            ecoObj.ASR = decimal.Parse(economicASR);
                            ecoObj.ASRCurr = "MMUSD";
                            ecoObj.EconomicResult = economicEconomicResult;
                            ecoObj.ContractorNPV = decimal.Parse(economicContractorNPV);
                            ecoObj.ContractorNPVCurr = "MMUSD";
                            ecoObj.IRR = decimal.Parse(economicIRR) / 100;
                            ecoObj.ContractorPOT = decimal.Parse(economicContractorPOT);
                            ecoObj.ContractorPOTUoM = "Years";
                            ecoObj.PIncome = decimal.Parse(economicPIncome);
                            ecoObj.PIncomeCurr = "Uniteless";
                            ecoObj.EMV = decimal.Parse(economicEMV);
                            ecoObj.EMVCurr = "MMUSD";
                            ecoObj.NPV = decimal.Parse(economicNPV);
                            ecoObj.NPVCurr = "MMUSD";
                            ecoObj.CreatedDate = DateTime.Now;
                            ecoObj.CreatedBy = user;
                            await _serviceEC.Create(ecoObj);
                        }
                        else
                        {
                            explorationEco.DevConcept = economicDevConcept;
                            explorationEco.EconomicAssumption = economicEconomicAssumption;
                            explorationEco.CAPEX = decimal.Parse(economicCAPEX);
                            explorationEco.CAPEXCurr = "MMUSD";
                            explorationEco.OPEXProduction = decimal.Parse(economicOPEXProduction);
                            explorationEco.OPEXProductionCurr = "MMUSD";
                            explorationEco.OPEXFacility = decimal.Parse(economicOPEXFacility);
                            explorationEco.OPEXFacilityCurr = "MMUSD";
                            explorationEco.ASR = decimal.Parse(economicASR);
                            explorationEco.ASRCurr = "MMUSD";
                            explorationEco.EconomicResult = economicEconomicResult;
                            explorationEco.ContractorNPV = decimal.Parse(economicContractorNPV);
                            explorationEco.ContractorNPVCurr = "MMUSD";
                            explorationEco.IRR = decimal.Parse(economicIRR) / 100;
                            explorationEco.ContractorPOT = decimal.Parse(economicContractorPOT);
                            explorationEco.ContractorPOTUoM = "Years";
                            explorationEco.PIncome = decimal.Parse(economicPIncome);
                            explorationEco.PIncomeCurr = "Uniteless";
                            explorationEco.EMV = decimal.Parse(economicEMV);
                            explorationEco.EMVCurr = "MMUSD";
                            explorationEco.NPV = decimal.Parse(economicNPV);
                            explorationEco.NPVCurr = "MMUSD";
                            explorationEco.CreatedDate = DateTime.Now;
                            explorationEco.CreatedBy = user;
                            await _serviceEC.Update(explorationEco);
                        }
                        //insert log activity
                        LGActivityDto activityObj = new LGActivityDto();
                        string hostName = Dns.GetHostName();
                        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                        activityObj.IP = myIP;
                        activityObj.Menu = "Exploration Structure";
                        activityObj.Action = "Update";
                        activityObj.TransactionID = StructureID;
                        activityObj.Status = explorationStructure.StatusData;
                        activityObj.CreatedDate = DateTime.Now;
                        activityObj.CreatedBy = user;
                        await _serviceAC.Create(activityObj);
                        return Json(new { success = true, structureid = StructureID });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Workflow Failed" });
                    }
                }
                else
                {
                    return Json(new { success = false, Message = "Workflow Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        [HttpPost]
        [Obsolete]
        public async Task<JsonResult> SubmitAll(string StructureName,
            string CountriesID,
            string SingleOrMultiParID,
            string StructureStatusParID,
            string SubholdingID,
            string ExplorationAreaParID,
            string ExplorationTypeParID,
            string Play,
            string UDSubTypeParID,
            string UDClassificationParID,
            string RegionalID,
            string UDSubClassificationParID,
            string ZonaID,
            string EffectiveYear,
            string BlockID,
            string APHID,
            string AssetID,
            string BasinID,
            string prosResourcesTarget,
            string prosResources,
            string recoverableOption,
            string currentPG,
            string expectedPG,
            string contResources,
            string drillingResources,
            string economicxBlockStatusParID,
            string economicOperatorshipStatusParID,
            string economicAwardDate,
            string economicExpiredDate,
            string economicDevConcept,
            string economicEconomicAssumption,
            string economicCAPEX,
            string economicOPEXProduction,
            string economicOPEXFacility,
            string economicASR,
            string economicEconomicResult,
            string economicContractorNPV,
            string economicIRR,
            string economicContractorPOT,
            string economicPIncome,
            string economicEMV,
            string economicNPV)
        {
            try
            {
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;

                var transNumber = "";
                var datasUserAuth = Task.Run(() => _servicePL.GetParamDescByParamListID("XAuth")).Result;
                if (!string.IsNullOrEmpty(datasUserAuth))
                {
                    using (var client = new HttpClient())
                    {
                        var datasAPPFK = Task.Run(() => _servicePL.GetParamDescByParamListID("AppFK")).Result;
                        var datasWF = Task.Run(() => _servicePL.GetParamDescByParamListID("WFCodePK")).Result;
                        if (!string.IsNullOrEmpty(datasAPPFK) && !string.IsNullOrEmpty(datasWF))
                        {
                            var actionStr = "Submit";
                            var userData = await _userService.GetUserInfo(user);
                            foreach (var item in userData.Roles)
                            {
                                if (item.Value.Trim() == "SUPER ADMIN")
                                {
                                    actionStr = "Submit4";
                                }
                            }
                            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                            {
                                Path = AimanConstant.TransactionWorkflow
                            };
                            client.DefaultRequestHeaders.Add("X-User-Auth", datasUserAuth);
                            client.BaseAddress = uriBuilder.Uri;
                            string param = string.Format(@"?appId={0}&companyCode=5000&transNo={2}&startWF={3}&action={4}&actionFor={5}&actionBy={6}&source={7}
                                                     &notes={8}&additionalData={9}",
                                                     datasAPPFK,
                                                     "5000",
                                                     "1",
                                                     datasWF,
                                                     actionStr,
                                                     user,
                                                     user,
                                                     "Web Exploration", "Submit Data", "-");

                            var responseTask = client.PostAsync(uriBuilder.Uri.ToString().Split('\\')[uriBuilder.Uri.ToString().Split('\\').Length - 1] + param, null);
                            responseTask.Wait();

                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();

                                DoTransactionResult DoTransRes = JsonConvert.DeserializeObject<DoTransactionResult>(readTask.Result);
                                if (!string.IsNullOrEmpty(DoTransRes.Object.Value))
                                {
                                    transNumber = DoTransRes.Object.Value;
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed" });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Workflow Failed" });
                        }
                    }

                    if (!string.IsNullOrEmpty(transNumber))
                    {
                        //var userTmp = await _userService.GetCurrentUserInfo();
                        var structureID = await _serviceES.GenerateNewID();
                        MDExplorationStructureDto explorStructureObj = new MDExplorationStructureDto();
                        explorStructureObj.xStructureID = structureID;
                        explorStructureObj.xStructureName = StructureName;
                        explorStructureObj.xStructureStatusParID = StructureStatusParID;
                        explorStructureObj.SingleOrMultiParID = SingleOrMultiParID;
                        explorStructureObj.ExplorationTypeParID = ExplorationTypeParID;
                        explorStructureObj.SubholdingID = SubholdingID;
                        explorStructureObj.BasinID = BasinID;
                        explorStructureObj.RegionalID = RegionalID;
                        explorStructureObj.ZonaID = ZonaID;
                        explorStructureObj.APHID = APHID;
                        explorStructureObj.xBlockID = BlockID;
                        explorStructureObj.xAssetID = AssetID;
                        explorStructureObj.xAreaID = ExplorationAreaParID;
                        explorStructureObj.UDClassificationParID = UDClassificationParID;
                        explorStructureObj.UDSubClassificationParID = UDSubClassificationParID;
                        explorStructureObj.UDSubTypeParID = UDSubTypeParID;
                        explorStructureObj.ExplorationAreaParID = ExplorationAreaParID;
                        explorStructureObj.CountriesID = CountriesID;
                        explorStructureObj.Play = Play;
                        explorStructureObj.MadamTransID = transNumber;
                        explorStructureObj.StatusData = "Submitted";
                        explorStructureObj.CreatedDate = DateTime.Now;
                        //explorStructureObj.CreatedBy = userTmp.Name;
                        explorStructureObj.CreatedBy = user;
                        await _serviceES.Create(explorStructureObj);
                        if (explorStructureObj.xStructureStatusParID.Trim() == "1" || explorStructureObj.xStructureStatusParID.Trim() == "2" || explorStructureObj.xStructureStatusParID.Trim() == "3")
                        {
                            Root prosResourceTarget = JsonConvert.DeserializeObject<Root>(prosResourcesTarget);
                            if (prosResourceTarget.rows.Count() > 0)
                            {
                                foreach (var pt in prosResourceTarget.rows)
                                {
                                    pt.xStructureID = structureID;
                                    pt.TargetID = await _servicePT.GenerateNewID();
                                    pt.P10InPlaceOilUoM = "MBO";
                                    pt.P90InPlaceOilUoM = "MBO";
                                    pt.P50InPlaceOilUoM = "MBO";
                                    pt.PMeanInPlaceOilUoM = "MBO";
                                    pt.P90InPlaceGasUoM = "MMSCF";
                                    pt.P50InPlaceGasUoM = "MMSCF";
                                    pt.PMeanInPlaceGasUoM = "MMSCF";
                                    pt.P10InPlaceGasUoM = "MMSCF";
                                    pt.P90InPlaceTotalUoM = "MBOE";
                                    pt.P50InPlaceTotalUoM = "MBOE";
                                    pt.PMeanInPlaceTotalUoM = "MBOE";
                                    pt.P10InPlaceTotalUoM = "MBOE";
                                    pt.P90RecoverableOilUoM = "MBO";
                                    pt.P50RecoverableOilUoM = "MBO";
                                    pt.PMeanRecoverableOilUoM = "MBO";
                                    pt.P10RecoverableOilUoM = "MBO";
                                    pt.P90RecoverableGasUoM = "MMSCF";
                                    pt.P50RecoverableGasUoM = "MMSCF";
                                    pt.PMeanRecoverableGasUoM = "MMSCF";
                                    pt.P10RecoverableGasUoM = "MMSCF";
                                    pt.P90RecoverableTotalUoM = "MBOE";
                                    pt.P50RecoverableTotalUoM = "MBOE";
                                    pt.PMeanRecoverableTotalUoM = "MBOE";
                                    pt.P10RecoverableTotalUoM = "MBOE";
                                    pt.GCFClosureUoM = "%";
                                    pt.GCFContainmentUoM = "%";
                                    pt.GCFPGTotalUoM = "%";
                                    pt.GCFReservoirUoM = "%";
                                    pt.GCFSRUoM = "%";
                                    pt.GCFTMUoM = "%";
                                    pt.RFOil = pt.RFOil / 100;
                                    pt.RFGas = pt.RFGas / 100;
                                    pt.GCFSR = pt.GCFSR / 100;
                                    pt.GCFTM = pt.GCFTM / 100;
                                    pt.GCFReservoir = pt.GCFReservoir / 100;
                                    pt.GCFClosure = pt.GCFClosure / 100;
                                    pt.GCFContainment = pt.GCFContainment / 100;
                                    pt.GCFPGTotal = pt.GCFPGTotal / 100;
                                    pt.CreatedDate = DateTime.Now;
                                    pt.CreatedBy = user;
                                    await _servicePT.Create(pt);

                                }
                            }
                            RootProsResources prosResource = JsonConvert.DeserializeObject<RootProsResources>(prosResources);
                            if (prosResource.rows.Count() > 0)
                            {
                                foreach (var pr in prosResource.rows)
                                {
                                    pr.xStructureID = structureID;
                                    pr.P10InPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceOilPRUoM = "MBO";
                                    pr.P50InPlaceOilPRUoM = "MBO";
                                    pr.PMeanInPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceGasPRUoM = "MMSCF";
                                    pr.P50InPlaceGasPRUoM = "MMSCF";
                                    pr.PMeanInPlaceGasPRUoM = "MMSCF";
                                    pr.P10InPlaceGasPRUoM = "MMSCF";
                                    pr.P90InPlaceTotalPRUoM = "MBOE";
                                    pr.P50InPlaceTotalPRUoM = "MBOE";
                                    pr.PMeanInPlaceTotalPRUoM = "MBOE";
                                    pr.P10InPlaceTotalPRUoM = "MBOE";
                                    pr.P90RROilUoM = "MBO";
                                    pr.P50RROilUoM = "MBO";
                                    pr.PMeanRROilUoM = "MBO";
                                    pr.P10RROilUoM = "MBO";
                                    pr.P90RRGasUoM = "MMSCF";
                                    pr.P50RRGasUoM = "MMSCF";
                                    pr.PMeanRRGasUoM = "MMSCF";
                                    pr.P10RRGasUoM = "MMSCF";
                                    pr.P90RRTotalUoM = "MBOE";
                                    pr.P50RRTotalUoM = "MBOE";
                                    pr.PMeanRRTotalUoM = "MBOE";
                                    pr.P10RRTotalUoM = "MBOE";
                                    pr.GCFClosurePRUoM = "%";
                                    pr.GCFContainmentPRUoM = "%";
                                    pr.GCFPGTotalPRUoM = "%";
                                    pr.GCFReservoirPRUoM = "%";
                                    pr.GCFSRPRUoM = "%";
                                    pr.GCFTMPRUoM = "%";
                                    if (expectedPG.Trim() == "")
                                    {
                                        expectedPG = "0";
                                    }
                                    if (currentPG.Trim() == "")
                                    {
                                        currentPG = "0";
                                    }
                                    pr.RFOilPR = pr.RFOilPR / 100;
                                    pr.RFGasPR = pr.RFGasPR / 100;
                                    pr.GCFSRPR = pr.GCFSRPR / 100;
                                    pr.GCFTMPR = pr.GCFTMPR / 100;
                                    pr.GCFReservoirPR = pr.GCFReservoirPR / 100;
                                    pr.GCFClosurePR = pr.GCFClosurePR / 100;
                                    pr.GCFContainmentPR = pr.GCFContainmentPR / 100;
                                    pr.GCFPGTotalPR = pr.GCFPGTotalPR / 100;
                                    pr.ExpectedPG = decimal.Parse(expectedPG, CultureInfo.InvariantCulture) / 100;
                                    pr.CurrentPG = decimal.Parse(currentPG, CultureInfo.InvariantCulture) / 100;
                                    pr.MethodParID = recoverableOption;
                                    pr.CreatedDate = DateTime.Now;
                                    pr.CreatedBy = user;
                                    await _servicePR.Create(pr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    //TXDrillingDto drlObj = new TXDrillingDto();
                                    dr.xStructureID = structureID;
                                    dr.xWellID = wellObj.xWellID;
                                    dr.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        dr.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        dr.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    dr.ExpectedPG = dr.ExpectedPG / 100;
                                    dr.CurrentPG = dr.CurrentPG / 100;
                                    dr.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    dr.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    dr.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    dr.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    dr.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    dr.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PlayOpener = true;
                                    }
                                    else
                                    {
                                        dr.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        dr.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        dr.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        dr.PotentialDelay = false;
                                    }
                                    dr.DrillingCostDHBCurr = "MMUSD";
                                    dr.DrillingCostCurr = "MMUSD";
                                    dr.P90ResourceOilUoM = "MMBO";
                                    dr.P50ResourceOilUoM = "MMBO";
                                    dr.P10ResourceOilUoM = "MMBO";
                                    dr.P90ResourceGasUoM = "BCF";
                                    dr.P50ResourceGasUoM = "BCF";
                                    dr.P10ResourceGasUoM = "BCF";
                                    dr.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.CreatedDate = DateTime.Now;
                                    dr.CreatedBy = user;
                                    await _serviceDR.Create(dr);
                                }
                            }
                        }
                        else
                        {
                            RootContResources contResource = JsonConvert.DeserializeObject<RootContResources>(contResources);
                            if (contResource.rows.Count() > 0)
                            {
                                foreach (var cr in contResource.rows)
                                {
                                    cr.xStructureID = structureID;
                                    cr.C1COilUoM = "MMBOE";
                                    cr.C2COilUoM = "MMBOE";
                                    cr.C3COilUoM = "MMBOE";
                                    cr.C1CGasUoM = "MMSCF";
                                    cr.C2CGasUoM = "MMSCF";
                                    cr.C3CGasUoM = "MMSCF";
                                    cr.C1CTotalUoM = "MBOE";
                                    cr.C2CTotalUoM = "MBOE";
                                    cr.C3CTotalUoM = "MBOE";
                                    cr.CreatedDate = DateTime.Now;
                                    cr.CreatedBy = user;
                                    await _serviceCR.Create(cr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    //TXDrillingDto drlObj = new TXDrillingDto();
                                    dr.xStructureID = structureID;
                                    dr.xWellID = wellObj.xWellID;
                                    dr.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        dr.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        dr.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    dr.ExpectedPG = dr.ExpectedPG / 100;
                                    dr.CurrentPG = dr.CurrentPG / 100;
                                    dr.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    dr.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    dr.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    dr.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    dr.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    dr.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PlayOpener = true;
                                    }
                                    else
                                    {
                                        dr.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        dr.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        dr.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        dr.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        dr.PotentialDelay = false;
                                    }
                                    dr.DrillingCostDHBCurr = "MMUSD";
                                    dr.DrillingCostCurr = "MMUSD";
                                    dr.P90ResourceOilUoM = "MMBO";
                                    dr.P50ResourceOilUoM = "MMBO";
                                    dr.P10ResourceOilUoM = "MMBO";
                                    dr.P90ResourceGasUoM = "BCF";
                                    dr.P50ResourceGasUoM = "BCF";
                                    dr.P10ResourceGasUoM = "BCF";
                                    dr.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    dr.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    dr.CreatedDate = DateTime.Now;
                                    dr.CreatedBy = user;
                                    await _serviceDR.Create(dr);
                                }
                            }
                        }
                        TXEconomicDto ecoObj = new TXEconomicDto();
                        ecoObj.xStructureID = structureID;
                        ecoObj.DevConcept = economicDevConcept;
                        ecoObj.EconomicAssumption = economicEconomicAssumption;
                        if (!string.IsNullOrEmpty(economicCAPEX))
                        {
                            ecoObj.CAPEX = decimal.Parse(economicCAPEX);
                        }
                        else
                        {
                            ecoObj.CAPEX = 0;
                        }
                        ecoObj.CAPEXCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicOPEXProduction))
                        {
                            ecoObj.OPEXProduction = decimal.Parse(economicOPEXProduction);
                        }
                        else
                        {
                            ecoObj.OPEXProduction = 0;
                        }
                        ecoObj.OPEXProductionCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicOPEXFacility))
                        {
                            ecoObj.OPEXFacility = decimal.Parse(economicOPEXFacility);
                        }
                        else
                        {
                            ecoObj.OPEXFacility = 0;
                        }
                        ecoObj.OPEXFacilityCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicASR))
                        {
                            ecoObj.ASR = decimal.Parse(economicASR);
                        }
                        else
                        {
                            ecoObj.ASR = 0;
                        }
                        ecoObj.ASRCurr = "MMUSD";
                        ecoObj.EconomicResult = economicEconomicResult;
                        if (!string.IsNullOrEmpty(economicContractorNPV))
                        {
                            ecoObj.ContractorNPV = decimal.Parse(economicContractorNPV);
                        }
                        else
                        {
                            ecoObj.ContractorNPV = 0;
                        }
                        ecoObj.ContractorNPVCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicIRR))
                        {
                            ecoObj.IRR = decimal.Parse(economicIRR) / 100;
                        }
                        else
                        {
                            ecoObj.IRR = 0;
                        }
                        if (!string.IsNullOrEmpty(economicContractorPOT))
                        {
                            ecoObj.ContractorPOT = decimal.Parse(economicContractorPOT);
                        }
                        else
                        {
                            ecoObj.ContractorPOT = 0;
                        }
                        ecoObj.ContractorPOTUoM = "Years";
                        if (!string.IsNullOrEmpty(economicPIncome))
                        {
                            ecoObj.PIncome = decimal.Parse(economicPIncome);
                        }
                        else
                        {
                            ecoObj.PIncome = 0;
                        }
                        ecoObj.PIncomeCurr = "Uniteless";
                        if (!string.IsNullOrEmpty(economicEMV))
                        {
                            ecoObj.EMV = decimal.Parse(economicEMV);
                        }
                        else
                        {
                            ecoObj.EMV = 0;
                        }
                        ecoObj.EMVCurr = "MMUSD";
                        if (!string.IsNullOrEmpty(economicNPV))
                        {
                            ecoObj.NPV = decimal.Parse(economicNPV);
                        }
                        else
                        {
                            ecoObj.NPV = 0;
                        }
                        ecoObj.NPVCurr = "MMUSD";
                        ecoObj.CreatedDate = DateTime.Now;
                        ecoObj.CreatedBy = user;
                        await _serviceEC.Create(ecoObj);
                        //insert log activity
                        LGActivityDto activityObj = new LGActivityDto();
                        string hostName = Dns.GetHostName();
                        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                        activityObj.IP = myIP;
                        activityObj.Menu = "Exploration Structure";
                        activityObj.Action = "Create";
                        activityObj.TransactionID = structureID;
                        activityObj.Status = explorStructureObj.StatusData;
                        activityObj.CreatedDate = DateTime.Now;
                        activityObj.CreatedBy = user;
                        await _serviceAC.Create(activityObj);
                        return Json(new { success = true, structureid = structureID });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Workflow Failed" });
                    }
                }
                else
                {
                    return Json(new { success = false, Message = "Workflow Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        [HttpPost]
        [Obsolete]
        public async Task<JsonResult> UpdateAll(string StructureName,
            string StructureID,
            string CountriesID,
            string SingleOrMultiParID,
            string StructureStatusParID,
            string SubholdingID,
            string ExplorationAreaParID,
            string ExplorationTypeParID,
            string Play,
            string UDSubTypeParID,
            string UDClassificationParID,
            string RegionalID,
            string UDSubClassificationParID,
            string ZonaID,
            string EffectiveYear,
            string BlockID,
            string APHID,
            string AssetID,
            string BasinID,
            string prosResourcesTarget,
            string prosResources,
            string recoverableOption,
            string currentPG,
            string expectedPG,
            string contResources,
            string drillingResources,
            string economicxBlockStatusParID,
            string economicOperatorshipStatusParID,
            string economicAwardDate,
            string economicExpiredDate,
            string economicDevConcept,
            string economicEconomicAssumption,
            string economicCAPEX,
            string economicOPEXProduction,
            string economicOPEXFacility,
            string economicASR,
            string economicEconomicResult,
            string economicContractorNPV,
            string economicIRR,
            string economicContractorPOT,
            string economicPIncome,
            string economicEMV,
            string economicNPV)
        {
            try
            {
                //var userTmp = await _userService.GetCurrentUserInfo();
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;

                var explorationStructure = Task.Run(() => _serviceES.GetOne(StructureID)).Result;

                var transNumber = "";
                var datasUserAuth = Task.Run(() => _servicePL.GetParamDescByParamListID("XAuth")).Result;
                if (!string.IsNullOrEmpty(datasUserAuth))
                {
                    using (var client = new HttpClient())
                    {
                        var datasAPPFK = Task.Run(() => _servicePL.GetParamDescByParamListID("AppFK")).Result;
                        var datasWF = Task.Run(() => _servicePL.GetParamDescByParamListID("WFCodePK")).Result;
                        if (!string.IsNullOrEmpty(datasAPPFK) && !string.IsNullOrEmpty(datasWF))
                        {
                            var actionStr = "Submit";
                            var userData = await _userService.GetUserInfo(user);
                            if (explorationStructure.StatusData.Trim() == "Reject Submitted" || explorationStructure.StatusData.Trim() == "Reject Revision")
                            {
                                actionStr = "Approve";
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER_ADMIN")
                                    {
                                        actionStr = "Approve2";
                                    }
                                }
                            }
                            else 
                            {
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER ADMIN")
                                    {
                                        actionStr = "Submit4";
                                    }
                                }
                            }
                            UriBuilder uriBuilder = new UriBuilder(AimanConstant.Uri)
                            {
                                Path = AimanConstant.TransactionWorkflow
                            };
                            client.DefaultRequestHeaders.Add("X-User-Auth", datasUserAuth);
                            client.BaseAddress = uriBuilder.Uri;
                            string param = string.Format(@"?appId={0}&companyCode=5000&transNo={2}&startWF={3}&action={4}&actionFor={5}&actionBy={6}&source={7}
                                                     &notes={8}&additionalData={9}",
                                                     datasAPPFK,
                                                     "5000",
                                                     explorationStructure.MadamTransID.Trim(),
                                                     "",
                                                     actionStr,
                                                     user,
                                                     user,
                                                     "Web Exploration", "Submit Data", "-");

                            var responseTask = client.PostAsync(uriBuilder.Uri.ToString().Split('\\')[uriBuilder.Uri.ToString().Split('\\').Length - 1] + param, null);
                            responseTask.Wait();

                            var result = responseTask.Result;
                            if (result.IsSuccessStatusCode)
                            {
                                var readTask = result.Content.ReadAsStringAsync();
                                readTask.Wait();

                                DoTransactionResult DoTransRes = JsonConvert.DeserializeObject<DoTransactionResult>(readTask.Result);
                                if (!string.IsNullOrEmpty(DoTransRes.Object.Value))
                                {
                                    transNumber = DoTransRes.Object.Value;
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed" });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed" });
                            }
                        }
                        else
                        {
                            return Json(new { success = false, Message = "Workflow Failed" });
                        }
                    }

                    if (!string.IsNullOrEmpty(transNumber))
                    {
                        explorationStructure.xStructureName = StructureName;
                        explorationStructure.xStructureStatusParID = StructureStatusParID;
                        explorationStructure.SingleOrMultiParID = SingleOrMultiParID;
                        explorationStructure.ExplorationTypeParID = ExplorationTypeParID;
                        explorationStructure.SubholdingID = SubholdingID;
                        explorationStructure.BasinID = BasinID;
                        explorationStructure.RegionalID = RegionalID;
                        explorationStructure.ZonaID = ZonaID;
                        explorationStructure.APHID = APHID;
                        explorationStructure.xBlockID = BlockID;
                        explorationStructure.xAssetID = AssetID;
                        explorationStructure.xAreaID = ExplorationAreaParID;
                        explorationStructure.UDClassificationParID = UDClassificationParID;
                        explorationStructure.UDSubClassificationParID = UDSubClassificationParID;
                        explorationStructure.UDSubTypeParID = UDSubTypeParID;
                        explorationStructure.ExplorationAreaParID = ExplorationAreaParID;
                        explorationStructure.CountriesID = CountriesID;
                        explorationStructure.Play = Play;
                        explorationStructure.MadamTransID = transNumber;
                        if (explorationStructure.StatusData.Trim() == "Released" || explorationStructure.StatusData.Trim() == "Reject Revision")
                        {
                            explorationStructure.StatusData = "Revision";
                        }
                        else
                        {
                            explorationStructure.StatusData = "Submitted";
                        }
                        //explorationStructure.CreatedDate = DateTime.Now;
                        //explorationStructure.CreatedBy = user;
                        explorationStructure.UpdatedDate = DateTime.Now;
                        explorationStructure.UpdatedBy = user;
                        await _serviceES.Update(explorationStructure);
                        if (explorationStructure.xStructureStatusParID.Trim() == "1" || explorationStructure.xStructureStatusParID.Trim() == "2" || explorationStructure.xStructureStatusParID.Trim() == "3")
                        {
                            Root prosResourceTarget = JsonConvert.DeserializeObject<Root>(prosResourcesTarget);
                            if (prosResourceTarget.rows.Count() > 0)
                            {
                                var datas = Task.Run(() => _servicePT.GetProsResourceTargetByStructureID(StructureID)).Result;
                                foreach (var dt in datas)
                                {
                                    await _servicePT.Destroy(dt.TargetID);
                                }
                                foreach (var pt in prosResourceTarget.rows)
                                {
                                    pt.xStructureID = StructureID;
                                    pt.TargetID = await _servicePT.GenerateNewID();
                                    pt.P10InPlaceOilUoM = "MBO";
                                    pt.P90InPlaceOilUoM = "MBO";
                                    pt.P50InPlaceOilUoM = "MBO";
                                    pt.PMeanInPlaceOilUoM = "MBO";
                                    pt.P90InPlaceGasUoM = "MMSCF";
                                    pt.P50InPlaceGasUoM = "MMSCF";
                                    pt.PMeanInPlaceGasUoM = "MMSCF";
                                    pt.P10InPlaceGasUoM = "MMSCF";
                                    pt.P90InPlaceTotalUoM = "MBOE";
                                    pt.P50InPlaceTotalUoM = "MBOE";
                                    pt.PMeanInPlaceTotalUoM = "MBOE";
                                    pt.P10InPlaceTotalUoM = "MBOE";
                                    pt.P90RecoverableOilUoM = "MBO";
                                    pt.P50RecoverableOilUoM = "MBO";
                                    pt.PMeanRecoverableOilUoM = "MBO";
                                    pt.P10RecoverableOilUoM = "MBO";
                                    pt.P90RecoverableGasUoM = "MMSCF";
                                    pt.P50RecoverableGasUoM = "MMSCF";
                                    pt.PMeanRecoverableGasUoM = "MMSCF";
                                    pt.P10RecoverableGasUoM = "MMSCF";
                                    pt.P90RecoverableTotalUoM = "MBOE";
                                    pt.P50RecoverableTotalUoM = "MBOE";
                                    pt.PMeanRecoverableTotalUoM = "MBOE";
                                    pt.P10RecoverableTotalUoM = "MBOE";
                                    pt.GCFClosureUoM = "%";
                                    pt.GCFContainmentUoM = "%";
                                    pt.GCFPGTotalUoM = "%";
                                    pt.GCFReservoirUoM = "%";
                                    pt.GCFSRUoM = "%";
                                    pt.GCFTMUoM = "%";
                                    pt.RFOil = pt.RFOil / 100;
                                    pt.RFGas = pt.RFGas / 100;
                                    pt.GCFSR = pt.GCFSR / 100;
                                    pt.GCFTM = pt.GCFTM / 100;
                                    pt.GCFReservoir = pt.GCFReservoir / 100;
                                    pt.GCFClosure = pt.GCFClosure / 100;
                                    pt.GCFContainment = pt.GCFContainment / 100;
                                    pt.GCFPGTotal = pt.GCFPGTotal / 100;
                                    pt.CreatedDate = DateTime.Now;
                                    pt.CreatedBy = user;
                                    await _servicePT.Create(pt);

                                }
                            }
                            RootProsResources prosResource = JsonConvert.DeserializeObject<RootProsResources>(prosResources);
                            if (prosResource.rows.Count() > 0)
                            {
                                var datas = Task.Run(() => _servicePR.GetOne(StructureID)).Result;
                                if (!string.IsNullOrEmpty(datas.xStructureID))
                                {
                                    await _servicePR.Destroy(datas.xStructureID);
                                }
                                foreach (var pr in prosResource.rows)
                                {
                                    pr.xStructureID = StructureID;
                                    pr.P10InPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceOilPRUoM = "MBO";
                                    pr.P50InPlaceOilPRUoM = "MBO";
                                    pr.PMeanInPlaceOilPRUoM = "MBO";
                                    pr.P90InPlaceGasPRUoM = "MMSCF";
                                    pr.P50InPlaceGasPRUoM = "MMSCF";
                                    pr.PMeanInPlaceGasPRUoM = "MMSCF";
                                    pr.P10InPlaceGasPRUoM = "MMSCF";
                                    pr.P90InPlaceTotalPRUoM = "MBOE";
                                    pr.P50InPlaceTotalPRUoM = "MBOE";
                                    pr.PMeanInPlaceTotalPRUoM = "MBOE";
                                    pr.P10InPlaceTotalPRUoM = "MBOE";
                                    pr.P90RROilUoM = "MBO";
                                    pr.P50RROilUoM = "MBO";
                                    pr.PMeanRROilUoM = "MBO";
                                    pr.P10RROilUoM = "MBO";
                                    pr.P90RRGasUoM = "MMSCF";
                                    pr.P50RRGasUoM = "MMSCF";
                                    pr.PMeanRRGasUoM = "MMSCF";
                                    pr.P10RRGasUoM = "MMSCF";
                                    pr.P90RRTotalUoM = "MBOE";
                                    pr.P50RRTotalUoM = "MBOE";
                                    pr.PMeanRRTotalUoM = "MBOE";
                                    pr.P10RRTotalUoM = "MBOE";
                                    pr.GCFClosurePRUoM = "%";
                                    pr.GCFContainmentPRUoM = "%";
                                    pr.GCFPGTotalPRUoM = "%";
                                    pr.GCFReservoirPRUoM = "%";
                                    pr.GCFSRPRUoM = "%";
                                    pr.GCFTMPRUoM = "%";
                                    if (expectedPG.Trim() == "")
                                    {
                                        expectedPG = "0";
                                    }
                                    if (currentPG.Trim() == "")
                                    {
                                        currentPG = "0";
                                    }
                                    pr.RFOilPR = pr.RFOilPR / 100;
                                    pr.RFGasPR = pr.RFGasPR / 100;
                                    pr.GCFSRPR = pr.GCFSRPR / 100;
                                    pr.GCFTMPR = pr.GCFTMPR / 100;
                                    pr.GCFReservoirPR = pr.GCFReservoirPR / 100;
                                    pr.GCFClosurePR = pr.GCFClosurePR / 100;
                                    pr.GCFContainmentPR = pr.GCFContainmentPR / 100;
                                    pr.GCFPGTotalPR = pr.GCFPGTotalPR / 100;
                                    pr.ExpectedPG = decimal.Parse(expectedPG, CultureInfo.InvariantCulture) / 100;
                                    pr.CurrentPG = decimal.Parse(currentPG, CultureInfo.InvariantCulture) / 100;
                                    pr.MethodParID = recoverableOption;
                                    pr.CreatedDate = DateTime.Now;
                                    pr.CreatedBy = user;
                                    await _servicePR.Create(pr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                var datas = Task.Run(() => _serviceDR.GetDrillingByStructureID(StructureID)).Result;
                                foreach (var dt in datas)
                                {
                                    await _serviceWL.Destroy(dt.xWellID);
                                }
                                foreach (var dt in datas)
                                {
                                    await _serviceDR.Destroy(dt.xStructureID, dt.xWellID);
                                }
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    TXDrillingDto drlObj = new TXDrillingDto();
                                    drlObj.xStructureID = StructureID;
                                    drlObj.xWellID = wellObj.xWellID;
                                    drlObj.WaterDepthMeter = dr.WaterDepthMeter;
                                    drlObj.WaterDepthFeet = dr.WaterDepthFeet;
                                    drlObj.TotalDepthMeter = dr.TotalDepthMeter;
                                    drlObj.TotalDepthFeet = dr.TotalDepthFeet;
                                    drlObj.SurfaceLocationLatitude = dr.SurfaceLocationLatitude;
                                    drlObj.SurfaceLocationLongitude = dr.SurfaceLocationLongitude;
                                    drlObj.DrillingCost = dr.DrillingCost;
                                    drlObj.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        drlObj.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        drlObj.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    drlObj.DrillingCostDHB = dr.DrillingCostDHB;
                                    drlObj.OperationalContextParId = dr.OperationalContextParId;
                                    drlObj.Location = dr.Location;
                                    drlObj.DrillingCompletionPeriod = dr.DrillingCompletionPeriod;
                                    drlObj.ExpectedPG = dr.ExpectedPG / 100;
                                    drlObj.CurrentPG = dr.CurrentPG / 100;
                                    drlObj.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    drlObj.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    drlObj.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    drlObj.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    drlObj.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    drlObj.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PlayOpener = true;
                                    }
                                    else
                                    {
                                        drlObj.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        drlObj.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        drlObj.PotentialDelay = false;
                                    }
                                    drlObj.DrillingCostDHBCurr = "MMUSD";
                                    drlObj.DrillingCostCurr = "MMUSD";
                                    drlObj.P90ResourceOilUoM = "MMBO";
                                    drlObj.P50ResourceOilUoM = "MMBO";
                                    drlObj.P10ResourceOilUoM = "MMBO";
                                    drlObj.P90ResourceGasUoM = "BCF";
                                    drlObj.P50ResourceGasUoM = "BCF";
                                    drlObj.P10ResourceGasUoM = "BCF";
                                    drlObj.P90ResourceOil = dr.P90ResourceOil;
                                    drlObj.P50ResourceOil = dr.P50ResourceOil;
                                    drlObj.P10ResourceOil = dr.P10ResourceOil;
                                    drlObj.P90ResourceGas = dr.P90ResourceGas;
                                    drlObj.P50ResourceGas = dr.P50ResourceGas;
                                    drlObj.P10ResourceGas = dr.P10ResourceGas;
                                    drlObj.P90NPVProfitabilityOil = dr.P90NPVProfitabilityOil;
                                    drlObj.P50NPVProfitabilityOil = dr.P50NPVProfitabilityOil;
                                    drlObj.P10NPVProfitabilityOil = dr.P10NPVProfitabilityOil;
                                    drlObj.P90NPVProfitabilityGas = dr.P90NPVProfitabilityGas;
                                    drlObj.P50NPVProfitabilityGas = dr.P50NPVProfitabilityGas;
                                    drlObj.P10NPVProfitabilityGas = dr.P10NPVProfitabilityGas;
                                    drlObj.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.CreatedDate = DateTime.Now;
                                    drlObj.CreatedBy = user;
                                    await _serviceDR.Create(drlObj);
                                }
                            }
                        }
                        else
                        {
                            RootContResources contResource = JsonConvert.DeserializeObject<RootContResources>(contResources);
                            if (contResource.rows.Count() > 0)
                            {
                                var datas = Task.Run(() => _serviceCR.GetContResourceTargetByStructureID(StructureID)).Result;
                                if (datas != null)
                                {
                                    await _serviceCR.Destroy(datas.xStructureID);
                                }
                                foreach (var cr in contResource.rows)
                                {
                                    cr.xStructureID = StructureID;
                                    cr.C1COilUoM = "MMBOE";
                                    cr.C2COilUoM = "MMBOE";
                                    cr.C3COilUoM = "MMBOE";
                                    cr.C1CGasUoM = "MMSCF";
                                    cr.C2CGasUoM = "MMSCF";
                                    cr.C3CGasUoM = "MMSCF";
                                    cr.C1CTotalUoM = "MBOE";
                                    cr.C2CTotalUoM = "MBOE";
                                    cr.C3CTotalUoM = "MBOE";
                                    cr.CreatedDate = DateTime.Now;
                                    cr.CreatedBy = user;
                                    await _serviceCR.Create(cr);

                                }
                            }
                            RootDrilling drllingResource = JsonConvert.DeserializeObject<RootDrilling>(drillingResources);
                            if (drllingResource.rows.Count > 0)
                            {
                                var datas = Task.Run(() => _serviceDR.GetDrillingByStructureID(StructureID)).Result;
                                foreach (var dt in datas)
                                {
                                    await _serviceWL.Destroy(dt.xWellID);
                                }
                                foreach (var dt in datas)
                                {
                                    await _serviceDR.Destroy(dt.xStructureID, dt.xWellID);
                                }
                                foreach (var dr in drllingResource.rows)
                                {
                                    MDExplorationWellDto wellObj = new MDExplorationWellDto();
                                    wellObj.xWellID = await _serviceWL.GenerateNewID();
                                    wellObj.xWellName = dr.xWellName;
                                    wellObj.DrillingLocation = dr.DrillingLocation;
                                    wellObj.RigTypeParID = dr.RigTypeParID;
                                    wellObj.WellTypeParID = dr.WellTypeParID;
                                    wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                                    wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                                    await _serviceWL.Create(wellObj);
                                    TXDrillingDto drlObj = new TXDrillingDto();
                                    drlObj.xStructureID = StructureID;
                                    drlObj.xWellID = wellObj.xWellID;
                                    drlObj.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                                    drlObj.WaterDepthMeter = dr.WaterDepthMeter;
                                    drlObj.WaterDepthFeet = dr.WaterDepthFeet;
                                    drlObj.TotalDepthMeter = dr.TotalDepthMeter;
                                    drlObj.TotalDepthFeet = dr.TotalDepthFeet;
                                    drlObj.SurfaceLocationLatitude = dr.SurfaceLocationLatitude;
                                    drlObj.SurfaceLocationLongitude = dr.SurfaceLocationLongitude;
                                    drlObj.DrillingCost = dr.DrillingCost;
                                    if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                                    {
                                        string[] drilDate = dr.ExpectedDrillingDate.Split('-');
                                        string date = drilDate[0];
                                        if (Convert.ToInt32(date) < 10)
                                        {
                                            date = "0" + Convert.ToInt32(date);
                                        }
                                        string month = "";
                                        if (drilDate[1].ToLower().Trim() == "jan")
                                        {
                                            month = "01";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "feb")
                                        {
                                            month = "02";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mar")
                                        {
                                            month = "03";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "apr")
                                        {
                                            month = "04";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "mei")
                                        {
                                            month = "05";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jun")
                                        {
                                            month = "06";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "jul")
                                        {
                                            month = "07";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "agu")
                                        {
                                            month = "08";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "sep")
                                        {
                                            month = "09";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "okt")
                                        {
                                            month = "10";
                                        }
                                        else if (drilDate[1].ToLower().Trim() == "nov")
                                        {
                                            month = "11";
                                        }
                                        else
                                        {
                                            month = "12";
                                        }
                                        var drillingDateStr = date.Trim() + "/" + month + "/" + drilDate[2].Trim();
                                        var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                        drlObj.ExpectedDrillingDate = dateDrl.Date.ToString();
                                    }
                                    else
                                    {
                                        drlObj.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                                    }
                                    drlObj.DrillingCostDHB = dr.DrillingCostDHB;
                                    drlObj.OperationalContextParId = dr.OperationalContextParId;
                                    drlObj.Location = dr.Location;
                                    drlObj.DrillingCompletionPeriod = dr.DrillingCompletionPeriod;
                                    drlObj.ExpectedPG = dr.ExpectedPG / 100;
                                    drlObj.CurrentPG = dr.CurrentPG / 100;
                                    drlObj.NetRevenueInterest = dr.NetRevenueInterest / 100;
                                    drlObj.ChanceComponentClosure = dr.ChanceComponentClosure / 100;
                                    drlObj.ChanceComponentContainment = dr.ChanceComponentContainment / 100;
                                    drlObj.ChanceComponentReservoir = dr.ChanceComponentReservoir / 100;
                                    drlObj.ChanceComponentSource = dr.ChanceComponentSource / 100;
                                    drlObj.ChanceComponentTiming = dr.ChanceComponentTiming / 100;
                                    if (dr.PlayOpenerBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PlayOpener = true;
                                    }
                                    else
                                    {
                                        drlObj.PlayOpener = false;
                                    }
                                    if (dr.CommitmentWellBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.CommitmentWell = true;
                                    }
                                    else
                                    {
                                        drlObj.CommitmentWell = false;
                                    }
                                    if (dr.PotentialDelayBit.ToLower().Trim() == "yes")
                                    {
                                        drlObj.PotentialDelay = true;
                                    }
                                    else
                                    {
                                        drlObj.PotentialDelay = false;
                                    }
                                    drlObj.DrillingCostDHBCurr = "MMUSD";
                                    drlObj.DrillingCostCurr = "MMUSD";
                                    drlObj.P90ResourceOilUoM = "MMBO";
                                    drlObj.P50ResourceOilUoM = "MMBO";
                                    drlObj.P10ResourceOilUoM = "MMBO";
                                    drlObj.P90ResourceGasUoM = "BCF";
                                    drlObj.P50ResourceGasUoM = "BCF";
                                    drlObj.P10ResourceGasUoM = "BCF";
                                    drlObj.P90ResourceOil = dr.P90ResourceOil;
                                    drlObj.P50ResourceOil = dr.P50ResourceOil;
                                    drlObj.P10ResourceOil = dr.P10ResourceOil;
                                    drlObj.P90ResourceGas = dr.P90ResourceGas;
                                    drlObj.P50ResourceGas = dr.P50ResourceGas;
                                    drlObj.P10ResourceGas = dr.P10ResourceGas;
                                    drlObj.P90NPVProfitabilityOil = dr.P90NPVProfitabilityOil;
                                    drlObj.P50NPVProfitabilityOil = dr.P50NPVProfitabilityOil;
                                    drlObj.P10NPVProfitabilityOil = dr.P10NPVProfitabilityOil;
                                    drlObj.P90NPVProfitabilityGas = dr.P90NPVProfitabilityGas;
                                    drlObj.P50NPVProfitabilityGas = dr.P50NPVProfitabilityGas;
                                    drlObj.P10NPVProfitabilityGas = dr.P10NPVProfitabilityGas;
                                    drlObj.P90NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P50NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P10NPVProfitabilityOilCurr = "USD/Bbl";
                                    drlObj.P90NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P50NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.P10NPVProfitabilityGasCurr = "USD/Mcf";
                                    drlObj.CreatedDate = DateTime.Now;
                                    drlObj.CreatedBy = user;
                                    await _serviceDR.Create(drlObj);
                                }
                            }
                        }

                        var explorationEco = Task.Run(() => _serviceEC.GetOne(StructureID)).Result;
                        if (string.IsNullOrEmpty(explorationEco.xStructureID))
                        {
                            TXEconomicDto ecoObj = new TXEconomicDto();
                            ecoObj.xStructureID = StructureID;
                            ecoObj.DevConcept = economicDevConcept;
                            ecoObj.EconomicAssumption = economicEconomicAssumption;
                            if (!string.IsNullOrEmpty(economicCAPEX))
                            {
                                ecoObj.CAPEX = decimal.Parse(economicCAPEX);
                            }
                            else
                            {
                                ecoObj.CAPEX = 0;
                            }
                            ecoObj.CAPEXCurr = "MMUSD";
                            if (!string.IsNullOrEmpty(economicOPEXProduction))
                            {
                                ecoObj.OPEXProduction = decimal.Parse(economicOPEXProduction);
                            }
                            else
                            {
                                ecoObj.OPEXProduction = 0;
                            }
                            ecoObj.OPEXProductionCurr = "MMUSD";
                            if (!string.IsNullOrEmpty(economicOPEXFacility))
                            {
                                ecoObj.OPEXFacility = decimal.Parse(economicOPEXFacility);
                            }
                            else
                            {
                                ecoObj.OPEXFacility = 0;
                            }
                            ecoObj.OPEXFacilityCurr = "MMUSD";
                            if (!string.IsNullOrEmpty(economicASR))
                            {
                                ecoObj.ASR = decimal.Parse(economicASR);
                            }
                            else
                            {
                                ecoObj.ASR = 0;
                            }
                            ecoObj.ASRCurr = "MMUSD";
                            ecoObj.EconomicResult = economicEconomicResult;
                            if (!string.IsNullOrEmpty(economicContractorNPV))
                            {
                                ecoObj.ContractorNPV = decimal.Parse(economicContractorNPV);
                            }
                            else
                            {
                                ecoObj.ContractorNPV = 0;
                            }
                            ecoObj.ContractorNPVCurr = "MMUSD";
                            if (!string.IsNullOrEmpty(economicIRR))
                            {
                                ecoObj.IRR = decimal.Parse(economicIRR) / 100;
                            }
                            else
                            {
                                ecoObj.IRR = 0;
                            }
                            if (!string.IsNullOrEmpty(economicContractorPOT))
                            {
                                ecoObj.ContractorPOT = decimal.Parse(economicContractorPOT);
                            }
                            else
                            {
                                ecoObj.ContractorPOT = 0;
                            }
                            ecoObj.ContractorPOTUoM = "Years";
                            if (!string.IsNullOrEmpty(economicPIncome))
                            {
                                ecoObj.PIncome = decimal.Parse(economicPIncome);
                            }
                            else
                            {
                                ecoObj.PIncome = 0;
                            }
                            ecoObj.PIncomeCurr = "Uniteless";
                            if (!string.IsNullOrEmpty(economicEMV))
                            {
                                ecoObj.EMV = decimal.Parse(economicEMV);
                            }
                            else
                            {
                                ecoObj.EMV = 0;
                            }
                            ecoObj.EMVCurr = "MMUSD";
                            if (!string.IsNullOrEmpty(economicNPV))
                            {
                                ecoObj.NPV = decimal.Parse(economicNPV);
                            }
                            else
                            {
                                ecoObj.NPV = 0;
                            }
                            ecoObj.NPVCurr = "MMUSD";
                            ecoObj.CreatedDate = DateTime.Now;
                            ecoObj.CreatedBy = user;
                            await _serviceEC.Create(ecoObj);
                        }
                        else
                        {
                            explorationEco.DevConcept = economicDevConcept;
                            explorationEco.EconomicAssumption = economicEconomicAssumption;
                            explorationEco.CAPEX = decimal.Parse(economicCAPEX);
                            explorationEco.CAPEXCurr = "MMUSD";
                            explorationEco.OPEXProduction = decimal.Parse(economicOPEXProduction);
                            explorationEco.OPEXProductionCurr = "MMUSD";
                            explorationEco.OPEXFacility = decimal.Parse(economicOPEXFacility);
                            explorationEco.OPEXFacilityCurr = "MMUSD";
                            explorationEco.ASR = decimal.Parse(economicASR);
                            explorationEco.ASRCurr = "MMUSD";
                            explorationEco.EconomicResult = economicEconomicResult;
                            explorationEco.ContractorNPV = decimal.Parse(economicContractorNPV);
                            explorationEco.ContractorNPVCurr = "MMUSD";
                            explorationEco.IRR = decimal.Parse(economicIRR) / 100;
                            explorationEco.ContractorPOT = decimal.Parse(economicContractorPOT);
                            explorationEco.ContractorPOTUoM = "Years";
                            explorationEco.PIncome = decimal.Parse(economicPIncome);
                            explorationEco.PIncomeCurr = "Uniteless";
                            explorationEco.EMV = decimal.Parse(economicEMV);
                            explorationEco.EMVCurr = "MMUSD";
                            explorationEco.NPV = decimal.Parse(economicNPV);
                            explorationEco.NPVCurr = "MMUSD";
                            explorationEco.CreatedDate = DateTime.Now;
                            explorationEco.CreatedBy = user;
                            await _serviceEC.Update(explorationEco);
                        }
                        //insert log activity
                        LGActivityDto activityObj = new LGActivityDto();
                        string hostName = Dns.GetHostName();
                        string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                        activityObj.IP = myIP;
                        activityObj.Menu = "Exploration Structure";
                        activityObj.Action = "Update";
                        activityObj.TransactionID = StructureID;
                        activityObj.Status = explorationStructure.StatusData;
                        activityObj.CreatedDate = DateTime.Now;
                        activityObj.CreatedBy = user;
                        await _serviceAC.Create(activityObj);
                        return Json(new { success = true, structureid = explorationStructure.xStructureID });
                    }
                    else
                    {
                        return Json(new { success = false, Message = "Workflow Failed" });
                    }
                }
                else
                {
                    return Json(new { success = false, Message = "Workflow Failed" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        public sealed class ClientDataModel
        {
            public string Data { get; set; }
        }

        public class Root
        {
            public List<TXProsResourcesTargetDto> rows { get; set; }
        }

        public class RootProsResources
        {
            public List<TXProsResourceDto> rows { get; set; }
        }

        public class RootContResources
        {
            public List<TXContingenResourcesDto> rows { get; set; }
        }

        public class RootDrilling
        {
            public List<TXDrillingDto> rows { get; set; }
        }

        public class CustomViewModel
        {
            public string Name { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
        }

        public class UploadFilesRequestModel
        {
            public string structureid { get; set; }
        }
    }
}