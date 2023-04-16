using ASPNetMVC.Infrastructure.HttpUtils;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class ESDCGeneralInfoController : Controller
    {
        private readonly IMDExplorationStructureService _serviceES;
        private readonly IMDParameterListService _servicePL;
        private readonly IMDExplorationAreaService _serviceAR;
        private readonly ITXDrillingService _serviceDR;
        private readonly IMDEntityService _service;
        private readonly IUserService _userService;
        private readonly IHRISDevOrgUnitHierarchyService _serviceHris;
        private readonly IVWDIMEntityService _serviceVW;
        private readonly ITXESDCService _serviceESDC;
        private readonly IMDExplorationBlockService _serviceBL;
        private readonly IMDExplorationBasinService _serviceBS;
        private readonly IMDExplorationAssetService _serviceAS;
        private readonly ITXAttachmentService _serviceAT;
        private readonly ITXESDCProductionService _serviceProd;
        private readonly ITXESDCVolumetricService _serviceVol;
        private readonly ITXESDCForecastService _serviceFor;
        private readonly ITXESDCDiscrepancyService _serviceDis;
        private readonly ITXESDCInPlaceService _serviceIP;
        private readonly ILGActivityService _serviceAC;
        private readonly IVWCountryService _serviceVC;
        private readonly HttpContextBase _httpContext;
        public ESDCGeneralInfoController(IMDExplorationStructureService serviceES,
            IMDParameterListService servicePL,
            IMDExplorationAreaService serviceAR,
            ITXDrillingService serviceDR,
            IMDEntityService service,
            IUserService userService,
            IHRISDevOrgUnitHierarchyService serviceHris,
            IVWDIMEntityService serviceVW,
            ITXESDCService serviceESDC,
            IMDExplorationBlockService serviceBL,
            IMDExplorationBasinService serviceBS,
            IMDExplorationAssetService serviceAS,
            ITXAttachmentService serviceAT,
            ITXESDCProductionService serviceProd,
            ITXESDCVolumetricService serviceVol,
            ITXESDCForecastService serviceFor,
            ITXESDCDiscrepancyService serviceDis,
            ITXESDCInPlaceService serviceIP,
            ILGActivityService serviceAC,
            IVWCountryService serviceVC,
            HttpContextBase httpContext)
        {
            _serviceES = serviceES;
            _servicePL = servicePL;
            _serviceAR = serviceAR;
            _serviceDR = serviceDR;
            _service = service;
            _userService = userService;
            _serviceHris = serviceHris;
            _serviceVW = serviceVW;
            _serviceESDC = serviceESDC;
            _serviceBL = serviceBL;
            _serviceBS = serviceBS;
            _serviceAS = serviceAS;
            _serviceAT = serviceAT;
            _serviceProd = serviceProd;
            _serviceVol = serviceVol;
            _serviceFor = serviceFor;
            _serviceDis = serviceDis;
            _serviceIP = serviceIP;
            _serviceAC = serviceAC;
            _httpContext = httpContext;
            _serviceVC = serviceVC;
        }
        // GET: ESDCGeneralInfo
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

        protected FormDefinition DefineGeneralInfo(FormState formState, string paramID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "ESDCGeneralInfo",
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
                    Title = "ESDCGeneralInfo",
                    State = formState,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldGeneralInfo(paramID)
                    }
                };
                return formDef;
            }
        }
        private FieldSection FieldGeneralInfo()
        {
            var hrisObj = "SHU";
            var username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }

            List<LookupItem> rkapPlanList = new List<LookupItem>();
            LookupItem rkapPlanFirst = new LookupItem();
            rkapPlanFirst.Text = "- Select An Option -";
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
                    },
                    HrisRegObj = hrisObj
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
                        Id = "EntityID",
                        Label = "Select Exploration Structure",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationTypeParID),
                        Label = "Exploration Type",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = ExpTypeList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureID),
                        Label = "xStructure ID",
                        FieldType = FieldType.Text,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
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
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID                  ),
                        Label = "Exploration Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = ExpStatusList
                        },
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
                        Id = nameof(MDExplorationStructureDto.SubholdingID),
                        Label = "Subholding ID",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubClassificationParID),
                        Label = "UD Sub Classification",
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
                        Id = nameof(MDEntityDto.EffectiveYear),
                        Label = "RKAP Plan",
                        FieldType = FieldType.Dropdown,
                        Value = 0,
                        LookUpList = new LookupList
                        {
                            Items = rkapPlanList
                        },
                        IsRequired = true,
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
                        Id = nameof(MDExplorationStructureDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = singleMultiList
                        },
                        IsRequired = true,
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
                        Id = nameof(TXESDCDto.ESDCFieldID),
                        Label = "Field ID",
                        FieldType = FieldType.Text,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(TXESDCDto.ESDCProjectID),
                        Label = "Project ID",
                        FieldType = FieldType.Text,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.APHID),
                        Label = "APH",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(TXESDCDto.ESDCProjectName),
                        Label = "Project Name",
                        FieldType = FieldType.Text,
                        IsRequired = true,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                        Label = "One Pager Montage",
                        FileUploadESDC = true,
                        FieldType = FieldType.FileUpload,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Dropdown,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StructureOutline),
                        Label = "Structure Outline",
                        FileUploadESDC = true,
                        FieldType = FieldType.FileUpload,
                    }, //kanan
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
                    }, //kiri
                }
            };
        }
        [HttpGet]
        private FieldSection FieldGeneralInfo(string paramID)
        {
            var hrisObj = "SHU";
            var username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                        .FirstOrDefault(c => c.Type == "preferred_username")?.Value;

            if (!string.IsNullOrEmpty(username))
            {
                var user = Task.Run(() => _userService.GetUserInfo(username)).Result;
                if (!string.IsNullOrEmpty(user.OrgUnitID))
                {
                    var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(user.OrgUnitID));
                    hrisObj = taskHRis.Result;
                }
            }
            // get data exploration structure
            var explorationStructure = Task.Run(() => _serviceES.GetOne(paramID)).Result;
            var expESDC = Task.Run(() => _serviceESDC.GetOne(paramID)).Result;
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
                    },
                    HrisRegObj = hrisObj
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
                    },
                    HrisRegObj = hrisObj
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
                    },
                    HrisRegObj = hrisObj
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
                    },
                    HrisRegObj = hrisObj
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
                        Id = "EntityID",
                        Label = "Select Exploration Structure",
                        FieldType = FieldType.Dropdown,
                        IsDisabled = true
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
                        IsRequired = true,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureID),
                        Label = "xStructure ID",
                        FieldType = FieldType.Text,
                        Value = paramID,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text,
                        Value = explorationStructure.Play,
                        IsDisabled = true
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        Value = explorationStructure.xStructureName,
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
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID                  ),
                        Label = "Exploration Status",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.xStructureStatusParID,
                        LookUpList = new LookupList
                        {
                            Items = ExpStatusList
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
                        Id = nameof(MDEntityDto.EffectiveYear),
                        Label = "RKAP Plan",
                        FieldType = FieldType.Dropdown,
                        Value = yearRKAP.ToString(),
                        LookUpList = new LookupList
                        {
                            Items = rkapPlanList
                        },
                        IsRequired = true,
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
                        Id = nameof(MDExplorationStructureDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        Value = explorationStructure.SingleOrMultiParID,
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = singleMultiList
                        },
                        IsRequired = true,
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
                        Id = nameof(TXESDCDto.ESDCFieldID),
                        Label = "Field ID",
                        Value = expESDC.ESDCFieldID,
                        FieldType = FieldType.Text,
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
                        Id = nameof(TXESDCDto.ESDCProjectID),
                        Label = "Project ID",
                        Value = expESDC.ESDCProjectID,
                        FieldType = FieldType.Text,
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
                        Id = nameof(TXESDCDto.ESDCProjectName),
                        Label = "Project Name",
                        Value = expESDC.ESDCProjectName,
                        FieldType = FieldType.Text,
                        IsRequired = true,
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
                        Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                        Label = "One Pager Montage",
                        Value = fileUrl1,
                        FileID = fileId1,
                        FileType = fileType1,
                        Filename = fileName1,
                        FieldType = FieldType.FileUpload,
                        FileUploadESDC = true,
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
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StructureOutline),
                        Label = "Structure Outline",
                        Value = fileUrl2,
                        FileID = fileId2,
                        FileType = fileType2,
                        Filename = fileName2,
                        FieldType = FieldType.FileUpload,
                        FileUploadESDC = true,
                    }, //kanan
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
                    }, //kiri
                }
            };
        }
        protected List<ColumnDefinition> DefineExplorationAreaGrid()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Area ID", nameof(MDExplorationAreaDto.xAreaID), ColumnType.String),
                new ColumnDefinition("Area Name", nameof(MDExplorationAreaDto.xAreaName), ColumnType.String),
            };
        }
        [HttpGet]
        public JsonResult GenerateESDCGeneralInfoDropDownLst(string strctrId, string shuId, string regId, string zonId, string blcId, string basId, string astId, string aphId, string areaId, string effYear, string strUDTypeId, string strUDClassId, string strSubUDClassId, string strPlay)
        {
            MPESDCEntityListITem result = new MPESDCEntityListITem();
            MPEntityBlockOject resultChild = new MPEntityBlockOject();
            if(int.Parse(effYear) > 0)
            {
                result.RKAPYearText = effYear;
                result.RKAPYearValue = effYear;
            }
            else
            {
                result.RKAPYearText = "- Select An Option -";
                result.RKAPYearValue = effYear;
            }
            if(!string.IsNullOrEmpty(strUDTypeId))
            {
                var datas = Task.Run(() => _servicePL.GetLookupListText("SubUDStatus")).Result;
                foreach(var item in datas)
                {
                    if(item.ParamListID.Trim() == strUDTypeId)
                    {
                        result.UDTypeText = item.ParamValue1Text;
                    }
                }
                result.UDTypeValue = strUDTypeId;
            }
            else
            {
                result.UDTypeText = "";
                result.UDTypeValue = "0";
            }
            if (!string.IsNullOrEmpty(strUDClassId))
            {
                var datas = Task.Run(() => _servicePL.GetLookupListText("UDClassification")).Result;
                foreach (var item in datas)
                {
                    if (item.ParamListID.Trim() == strUDClassId)
                    {
                        result.UDClassText = item.ParamValue1Text;
                    }
                }
                result.UDClassValue = strUDClassId;
            }
            else
            {
                result.UDClassText = "";
                result.UDClassValue = "0";
            }
            if (!string.IsNullOrEmpty(strSubUDClassId))
            {
                if(strSubUDClassId.Trim() == "1")
                {
                    result.UDSubClassValue = "K7A";
                    result.UDSubClassText = "K7A";
                }
                else
                {
                    result.UDSubClassValue = "K7B";
                    result.UDSubClassText = "K7B";
                }
            }
            else
            {
                result.UDSubClassValue = "";
                result.UDSubClassText = "0";
            }
            if(!string.IsNullOrEmpty(strPlay))
            {
                result.Play = strPlay;
            }
            else
            {
                result.Play = "";
            }
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
                resultChild.OperatorshipStatusParID = blockDatas.OperatorshipStatusParID;
                resultChild.OperatorName = blockDatas.OperatorName;
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
            var getFileAttachments = Task.Run(() => _serviceAT.GetAttachmentByStructureID(strctrId)).Result;
            foreach (var item in getFileAttachments)
            {
                if (item.FileCategoryParID.Trim() == "OPM")
                {
                    result.fileUrl1 = FileServices.GetFileUrl(item.FileName);
                    result.fileType1 = System.IO.Path.GetExtension(item.FileName);
                    result.fileId1 = item.FileID;
                    result.fileName1 = item.FileName;
                }
                else
                {
                    result.fileUrl2 = FileServices.GetFileUrl(item.FileName);
                    result.fileType2 = System.IO.Path.GetExtension(item.FileName);
                    result.fileId2 = item.FileID;
                    result.fileName2 = item.FileName;
                }
            }

            return new JsonResult
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult Selection_Orders_Read_ESDC([DataSourceRequest] DataSourceRequest request)
        {
            //var datas = Task.Run(() => _serviceESDC.GetPaged(new GridParam
            //{
            //    FilterList = new FilterList
            //    {
            //        Page = 1,
            //        Size = 100,
            //        OrderBy = "es.[xStructureID] ASC"
            //    }

            //})).Result;
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

            var datas = Task.Run(() => _serviceESDC.GetLookupListText(DateTime.Now.Year.ToString(), hrisObj.Trim())).Result;
            //foreach (var item in datas)
            //{
            //    item.EntityName = Task.Run(() => _serviceVW.GetLookupText(item.SubholdingID)).Result;
            //    item.RegionalName = Task.Run(() => _serviceVW.GetLookupText(item.RegionalID)).Result;
            //    item.ZonaName = Task.Run(() => _serviceVW.GetLookupText(item.ZonaID)).Result;
            //    item.APHName = Task.Run(() => _serviceVW.GetLookupText(item.APHID)).Result;
            //}
            return Json(datas.ToDataSourceResult(request));
        }

        public async Task<JsonResult> SubmitESDCAll(string StructureID,
            string StructureName,
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
            string ESDCFieldID,
            string ESDCProjectID,
            string ESDCProjectName,
            string ESDCProduction,
            string ESDCVolumetric,
            string ESDCForecast,
            string ESDCDiscrepancy,
            string ESDCInPlace)
        {
            try
            {
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                TXESDCDto esdcObj = new TXESDCDto();
                esdcObj.xStructureID = StructureID;
                esdcObj.ESDCFieldID = ESDCFieldID;
                esdcObj.ESDCProjectID = ESDCProjectID;
                esdcObj.ESDCProjectName = ESDCProjectName;
                esdcObj.StatusData = "Submitted";
                esdcObj.CreatedDate = DateTime.Now;
                esdcObj.CreatedBy = user;
                esdcObj.UpdatedDate = DateTime.Now;
                esdcObj.UpdatedBy = user;
                await _serviceESDC.Create(esdcObj);

                RootProduction listProduction = JsonConvert.DeserializeObject<RootProduction>(ESDCProduction);
                if (listProduction.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceProd.GetListTXESDCProductionByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        await _serviceProd.Destroy(StructureID);
                    }
                    foreach (var pt in listProduction.rows)
                    {
                        pt.xStructureID = StructureID;
                        pt.CreatedDate = DateTime.Now;
                        pt.CreatedBy = user;
                        pt.UpdatedDate = DateTime.Now;
                        pt.UpdatedBy = user;
                        await _serviceProd.Create(pt);

                    }
                }
                RootVolumetric listVolumetric = JsonConvert.DeserializeObject<RootVolumetric>(ESDCVolumetric);
                if (listVolumetric.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceVol.GetListTXESDCVolumetricByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var volum in datas)
                        {
                            await _serviceVol.Destroy(StructureID, volum.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listVolumetric.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceVol.Create(pr);

                    }
                }
                RootForecast listForecast = JsonConvert.DeserializeObject<RootForecast>(ESDCForecast);
                if (listForecast.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceFor.GetForecastByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var fc in datas)
                        {
                            await _serviceFor.Destroy(StructureID, fc.Year);
                        }
                    }
                    foreach (var pr in listForecast.rows)
                    {
                        pr.xStructureID = StructureID;
                        await _serviceFor.Create(pr);

                    }
                }
                RootDiscrepancy listDiscrepancy = JsonConvert.DeserializeObject<RootDiscrepancy>(ESDCDiscrepancy);
                if (listDiscrepancy.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceDis.GetListTXESDCDiscrepancyByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var dis in datas)
                        {
                            await _serviceDis.Destroy(StructureID, dis.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listDiscrepancy.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceDis.Create(pr);

                    }
                }
                RootInPlace listInPlace = JsonConvert.DeserializeObject<RootInPlace>(ESDCInPlace);
                if (listInPlace.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceIP.GetListTXESDCInPlaceByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var inp in datas)
                        {
                            await _serviceIP.Destroy(StructureID, inp.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listInPlace.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceIP.Create(pr);

                    }
                }

                return Json(new { success = true, structureid = StructureID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        public async Task<JsonResult> SubmitUPDTESDCAll(string StructureID,
            string StructureName,
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
            string ESDCFieldID,
            string ESDCProjectID,
            string ESDCProjectName,
            string ESDCProduction,
            string ESDCVolumetric,
            string ESDCForecast,
            string ESDCDiscrepancy,
            string ESDCInPlace)
        {
            try
            {
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                TXESDCDto esdcObj = new TXESDCDto();
                esdcObj.xStructureID = StructureID;
                esdcObj.ESDCFieldID = ESDCFieldID;
                esdcObj.ESDCProjectID = ESDCProjectID;
                esdcObj.ESDCProjectName = ESDCProjectName;
                esdcObj.StatusData = "Submitted";
                //esdcObj.CreatedDate = DateTime.Now;
                //esdcObj.CreatedBy = user;
                esdcObj.UpdatedDate = DateTime.Now;
                esdcObj.UpdatedBy = user;
                await _serviceESDC.Update(esdcObj);

                RootProduction listProduction = JsonConvert.DeserializeObject<RootProduction>(ESDCProduction);
                if (listProduction.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceProd.GetListTXESDCProductionByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {

                        await _serviceProd.Destroy(StructureID);
                    }
                    foreach (var pt in listProduction.rows)
                    {
                        pt.xStructureID = StructureID;
                        pt.CreatedDate = DateTime.Now;
                        pt.CreatedBy = user;
                        pt.UpdatedDate = DateTime.Now;
                        pt.UpdatedBy = user;
                        await _serviceProd.Create(pt);

                    }
                }
                RootVolumetric listVolumetric = JsonConvert.DeserializeObject<RootVolumetric>(ESDCVolumetric);
                if (listVolumetric.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceVol.GetListTXESDCVolumetricByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach(var volum in datas)
                        {
                            await _serviceVol.Destroy(StructureID,volum.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listVolumetric.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceVol.Create(pr);

                    }
                }
                RootForecast listForecast = JsonConvert.DeserializeObject<RootForecast>(ESDCForecast);
                if (listForecast.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceFor.GetForecastByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach(var fc in datas)
                        {
                            await _serviceFor.Destroy(StructureID, fc.Year);
                        }
                    }
                    foreach (var pr in listForecast.rows)
                    {
                        pr.xStructureID = StructureID;
                        await _serviceFor.Create(pr);

                    }
                }
                RootDiscrepancy listDiscrepancy = JsonConvert.DeserializeObject<RootDiscrepancy>(ESDCDiscrepancy);
                if (listDiscrepancy.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceDis.GetListTXESDCDiscrepancyByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach(var dis in datas)
                        {
                            await _serviceDis.Destroy(StructureID, dis.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listDiscrepancy.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceDis.Create(pr);

                    }
                }
                RootInPlace listInPlace = JsonConvert.DeserializeObject<RootInPlace>(ESDCInPlace);
                if (listInPlace.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceIP.GetListTXESDCInPlaceByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach(var inp in datas)
                        {
                            await _serviceIP.Destroy(StructureID, inp.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listInPlace.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceIP.Create(pr);

                    }
                }

                return Json(new { success = true, structureid = StructureID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        [Obsolete]
        public async Task<JsonResult> SaveESDCAll(string StructureID,
            string StructureName,
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
            string ESDCFieldID,
            string ESDCProjectID,
            string ESDCProjectName,
            string ESDCProduction,
            string ESDCVolumetric,
            string ESDCForecast,
            string ESDCDiscrepancy,
            string ESDCInPlace)
        {
            try
            {
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                TXESDCDto esdcObj = new TXESDCDto();
                esdcObj.xStructureID = StructureID;
                esdcObj.ESDCFieldID = ESDCFieldID;
                esdcObj.ESDCProjectID = ESDCProjectID;
                esdcObj.ESDCProjectName = ESDCProjectName;
                esdcObj.StatusData = "Saved";
                esdcObj.CreatedDate = DateTime.Now;
                esdcObj.CreatedBy = user;
                esdcObj.UpdatedDate = DateTime.Now;
                esdcObj.UpdatedBy = user;
                await _serviceESDC.Create(esdcObj);

                RootProduction listProduction = JsonConvert.DeserializeObject<RootProduction>(ESDCProduction);
                if (listProduction.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceProd.GetListTXESDCProductionByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        await _serviceProd.Destroy(StructureID);
                    }
                    foreach (var pt in listProduction.rows)
                    {
                        pt.xStructureID = StructureID;
                        pt.CreatedDate = DateTime.Now;
                        pt.CreatedBy = user;
                        pt.UpdatedDate = DateTime.Now;
                        pt.UpdatedBy = user;
                        await _serviceProd.Create(pt);

                    }
                }
                RootVolumetric listVolumetric = JsonConvert.DeserializeObject<RootVolumetric>(ESDCVolumetric);
                if (listVolumetric.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceVol.GetListTXESDCVolumetricByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var volum in datas)
                        {
                            await _serviceVol.Destroy(StructureID, volum.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listVolumetric.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceVol.Create(pr);

                    }
                }
                RootForecast listForecast = JsonConvert.DeserializeObject<RootForecast>(ESDCForecast);
                if (listForecast.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceFor.GetForecastByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var fc in datas)
                        {
                            await _serviceFor.Destroy(StructureID, fc.Year);
                        }
                    }
                    foreach (var pr in listForecast.rows)
                    {
                        pr.xStructureID = StructureID;
                        await _serviceFor.Create(pr);

                    }
                }
                RootDiscrepancy listDiscrepancy = JsonConvert.DeserializeObject<RootDiscrepancy>(ESDCDiscrepancy);
                if (listDiscrepancy.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceDis.GetListTXESDCDiscrepancyByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var dis in datas)
                        {
                            await _serviceDis.Destroy(StructureID, dis.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listDiscrepancy.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceDis.Create(pr);

                    }
                }
                RootInPlace listInPlace = JsonConvert.DeserializeObject<RootInPlace>(ESDCInPlace);
                if (listInPlace.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceIP.GetListTXESDCInPlaceByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var inp in datas)
                        {
                            await _serviceIP.Destroy(StructureID, inp.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listInPlace.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceIP.Create(pr);

                    }
                }
                //insert log activity
                LGActivityDto activityObj = new LGActivityDto();
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                activityObj.IP = myIP;
                activityObj.Menu = "eSDC";
                activityObj.Action = "Create";
                activityObj.TransactionID = StructureID;
                activityObj.Status = esdcObj.StatusData;
                activityObj.CreatedDate = DateTime.Now;
                activityObj.CreatedBy = user;
                await _serviceAC.Create(activityObj);
                return Json(new { success = true, structureid = StructureID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        [Obsolete]
        public async Task<JsonResult> SaveUPDTESDCAll(string StructureID,
            string StructureName,
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
            string ESDCFieldID,
            string ESDCProjectID,
            string ESDCProjectName,
            string ESDCProduction,
            string ESDCVolumetric,
            string ESDCForecast,
            string ESDCDiscrepancy,
            string ESDCInPlace)
        {
            try
            {
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                TXESDCDto esdcObj = new TXESDCDto();
                esdcObj.xStructureID = StructureID;
                esdcObj.ESDCFieldID = ESDCFieldID;
                esdcObj.ESDCProjectID = ESDCProjectID;
                esdcObj.ESDCProjectName = ESDCProjectName;
                esdcObj.StatusData = "Saved";
                //esdcObj.CreatedDate = DateTime.Now;
                //esdcObj.CreatedBy = user;
                esdcObj.UpdatedDate = DateTime.Now;
                esdcObj.UpdatedBy = user;
                await _serviceESDC.Update(esdcObj);

                RootProduction listProduction = JsonConvert.DeserializeObject<RootProduction>(ESDCProduction);
                if (listProduction.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceProd.GetListTXESDCProductionByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        await _serviceProd.Destroy(StructureID);
                    }
                    foreach (var pt in listProduction.rows)
                    {
                        pt.xStructureID = StructureID;
                        pt.CreatedDate = DateTime.Now;
                        pt.CreatedBy = user;
                        pt.UpdatedDate = DateTime.Now;
                        pt.UpdatedBy = user;
                        await _serviceProd.Create(pt);

                    }
                }
                RootVolumetric listVolumetric = JsonConvert.DeserializeObject<RootVolumetric>(ESDCVolumetric);
                if (listVolumetric.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceVol.GetListTXESDCVolumetricByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var volum in datas)
                        {
                            await _serviceVol.Destroy(StructureID, volum.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listVolumetric.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceVol.Create(pr);

                    }
                }
                RootForecast listForecast = JsonConvert.DeserializeObject<RootForecast>(ESDCForecast);
                if (listForecast.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceFor.GetForecastByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var fc in datas)
                        {
                            await _serviceFor.Destroy(StructureID, fc.Year);
                        }
                    }
                    foreach (var pr in listForecast.rows)
                    {
                        pr.xStructureID = StructureID;
                        await _serviceFor.Create(pr);

                    }
                }
                RootDiscrepancy listDiscrepancy = JsonConvert.DeserializeObject<RootDiscrepancy>(ESDCDiscrepancy);
                if (listDiscrepancy.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceDis.GetListTXESDCDiscrepancyByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var dis in datas)
                        {
                            await _serviceDis.Destroy(StructureID, dis.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listDiscrepancy.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceDis.Create(pr);

                    }
                }
                RootInPlace listInPlace = JsonConvert.DeserializeObject<RootInPlace>(ESDCInPlace);
                if (listInPlace.rows.Count() > 0)
                {
                    var datas = Task.Run(() => _serviceIP.GetListTXESDCInPlaceByStructureID(StructureID)).Result;
                    if (datas.Count() > 0)
                    {
                        foreach (var inp in datas)
                        {
                            await _serviceIP.Destroy(StructureID, inp.UncertaintyLevel.Trim());
                        }
                    }
                    foreach (var pr in listInPlace.rows)
                    {
                        pr.xStructureID = StructureID;
                        pr.CreatedDate = DateTime.Now;
                        pr.CreatedBy = user;
                        pr.UpdatedDate = DateTime.Now;
                        pr.UpdatedBy = user;
                        await _serviceIP.Create(pr);

                    }
                }
                //insert log activity
                LGActivityDto activityObj = new LGActivityDto();
                string hostName = Dns.GetHostName();
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                activityObj.IP = myIP;
                activityObj.Menu = "eSDC";
                activityObj.Action = "Update";
                activityObj.TransactionID = StructureID;
                activityObj.Status = esdcObj.StatusData;
                activityObj.CreatedDate = DateTime.Now;
                activityObj.CreatedBy = user;
                await _serviceAC.Create(activityObj);
                return Json(new { success = true, structureid = StructureID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, ex.Message });
            }
        }

        public class RootProduction
        {
            public List<TXESDCProductionDto> rows { get; set; }
        }

        public class RootVolumetric
        {
            public List<TXESDCVolumetricDto> rows { get; set; }
        }

        public class RootForecast
        {
            public List<TXESDCForecastDto> rows { get; set; }
        }
        public class RootDiscrepancy
        {
            public List<TXESDCDiscrepancyDto> rows { get; set; }
        }
        public class RootInPlace
        {
            public List<TXESDCInPlaceDto> rows { get; set; }
        }
    }
}
