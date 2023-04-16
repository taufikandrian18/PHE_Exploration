using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class ESDCController : BaseCrudController<MDExplorationStructureESDCDto, MDExplorationStructureWithAdditionalFields>
    {
        private readonly IMDExplorationStructureESDCService _esdcService;
        private readonly IMDParameterListService _servicePL;
        private readonly IMDExplorationBlockService _serviceBL;
        private readonly IMDExplorationBasinService _serviceBS;
        private readonly IMDExplorationAssetService _serviceAS;
        private readonly IMDExplorationAreaService _serviceAR;
        private readonly ITXDrillingService _serviceDR;
        private readonly ITXProsResourcesService _servicePR;
        private readonly ITXProsResourcesTargetService _servicePT;
        private readonly ITXContingenResourcesService _serviceCR;
        private readonly IMDEntityService _service;
        private readonly ITXEconomicService _serviceEC;
        private readonly IMDExplorationStructureService _serviceES;
        private readonly ITXESDCProductionService _serviceProd;
        private readonly ITXESDCVolumetricService _serviceVol;
        private readonly ITXESDCForecastService _serviceFor;
        private readonly ITXESDCDiscrepancyService _serviceDis;
        private readonly ITXESDCInPlaceService _serviceIP;
        private readonly IVWDIMEntityService _serviceVW;
        private readonly IVWCountryService _serviceVC;
        private readonly HttpContextBase _httpContextBase;
        private readonly IMDExplorationStructureService _explorationStructureService;
        private readonly IUserService _userAccountService;
        private readonly IHRISDevOrgUnitHierarchyService _serviceHrObj;
        public ESDCController(IMDExplorationStructureESDCService crudSvc,
            ILookupService lookupSvc,
            IMDEntityService service,
            IMDParameterListService servicePL,
            IMDExplorationBlockService serviceBL,
            IMDExplorationBasinService serviceBS,
            IMDExplorationAssetService serviceAS,
            IMDExplorationAreaService serviceAR,
            ITXDrillingService serviceDR,
            ITXProsResourcesService servicePR,
            ITXProsResourcesTargetService servicePT,
            ITXContingenResourcesService serviceCR,
            IMDExplorationStructureService serviceES,
            ITXESDCProductionService serviceProd,
            ITXESDCVolumetricService serviceVol,
            ITXESDCForecastService serviceFor,
            ITXESDCDiscrepancyService serviceDis,
            ITXESDCInPlaceService serviceIP,
            IVWDIMEntityService serviceVW,
            IVWCountryService serviceVC,
            IMDExplorationStructureService explorationStructureService,
            ITXEconomicService serviceEC,
            IHRISDevOrgUnitHierarchyService serviceHrObj,
            HttpContextBase httpContextBase,
            IUserService userService) : base(crudSvc, lookupSvc, userService, serviceHrObj, httpContextBase)
        {
            _esdcService = crudSvc;
            _service = service;
            _servicePL = servicePL;
            _serviceBL = serviceBL;
            _serviceBS = serviceBS;
            _serviceAS = serviceAS;
            _serviceAR = serviceAR;
            _serviceDR = serviceDR;
            _servicePR = servicePR;
            _serviceEC = serviceEC;
            _servicePT = servicePT;
            _serviceCR = serviceCR;
            _serviceES = serviceES;
            _serviceProd = serviceProd;
            _serviceVol = serviceVol;
            _serviceFor = serviceFor;
            _serviceDis = serviceDis;
            _serviceIP = serviceIP;
            _serviceVW = serviceVW;
            _serviceVC = serviceVC;
            _explorationStructureService = explorationStructureService;
            _userAccountService = userService;
            _serviceHrObj = serviceHrObj;
            _httpContextBase = httpContextBase;
        }
        public override Task<ActionResult> Create(MDExplorationStructureESDCDto model)
        {
            throw new NotImplementedException();
        }

        public override Task<ActionResult> Edit(MDExplorationStructureESDCDto model)
        {
            throw new NotImplementedException();
        }

        protected override FormDefinition DefineForm(FormState formState)
        {
            var formDef = new FormDefinition
            {
                Title = "Pencatatan Sumber Daya",
                State = formState,
                FieldSections = new List<FieldSection>()
                {
                    GeneralField()
                }
            };
            return formDef;
        }
        private FieldSection GeneralField()
        {
            return new FieldSection
            {
                SectionName = "ESDC",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xStructureID),
                        FieldType = FieldType.Hidden
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xStructureStatusParID),
                        Label = "Exploration Status",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.ZonaID),
                        Label = "Zona",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.StatusData),
                        Label = "Data Status",
                        FieldType = FieldType.Text
                    },
                }
            };
        }

        protected override FormDefinition DefineFormView(FormState formState, string paramID)
        {
            var formDef = new FormDefinition
            {
                Title = "Pencatatan Sumber Daya",
                State = formState,
                paramID = paramID,
                DataESDCProd = Task.Run(() => _serviceProd.GetListTXESDCProductionByStructureID(paramID)).Result,
                DataESDCVol = Task.Run(() => _serviceVol.GetListTXESDCVolumetricByStructureID(paramID)).Result,
                DataESDCFor = Task.Run(() => _serviceFor.GetForecastByStructureID(paramID)).Result,
                DataESDCDis = Task.Run(() => _serviceDis.GetListTXESDCDiscrepancyByStructureID(paramID)).Result,
                DataESDCIn = Task.Run(() => _serviceIP.GetListTXESDCInPlaceByStructureID(paramID)).Result,
                FieldSections = new List<FieldSection>()
                {
                    FieldESDCGeneralInfo(paramID),
                    FieldESDCProduction(),
                    FieldESDCVolumetric(),
                    FieldESDCForecast(),
                    FieldESDCDiscrepancy(),
                    FieldESDCInPlace(),

                }
            };
            return formDef;
        }
        private FieldSection FieldESDCGeneralInfo(string paramID)
        {
            // get data exploration structure
            var esdc = Task.Run(() => _esdcService.GetOne(paramID)).Result;
            var explorationStructure = Task.Run(() => _explorationStructureService.GetOne(paramID)).Result;
            // get data drilling list by structure ID
            var drillingList = Task.Run(() => _serviceDR.GetDrillingByStructureID(paramID)).Result;
            var valueEffective = 0;
            var expStatusName = "";
            var expStatusTypeName = "";
            var expSingleMultiName = "";
            var expUDTypeName = "";
            var expClassification = "";
            var expSubClassification = "";
            if (drillingList.Count() > 0)
            {
                valueEffective = drillingList[0].RKAPFiscalYear;
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
                        valueEffective = drillingList[0].RKAPFiscalYear;
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
            if (!string.IsNullOrEmpty(esdc.UDSubClassificationParID))
            {
                for (int i = 1; i <= 2; i++)
                {
                    LookupItem udSubClass = new LookupItem();
                    if (i == 1)
                    {
                        udSubClass.Text = "K7A";
                        udSubClass.Description = "";
                        udSubClass.Value = i.ToString();
                        if (i.ToString() == esdc.UDSubClassificationParID.Trim())
                        {
                            udSubClass.Selected = true;
                            expSubClassification = "K7A";
                        }
                    }
                    else
                    {
                        udSubClass.Text = "K7B";
                        udSubClass.Description = "";
                        udSubClass.Value = i.ToString();
                        if (i.ToString() == esdc.UDSubClassificationParID.Trim())
                        {
                            udSubClass.Selected = true;
                            expSubClassification = "K7B";
                        }
                    }
                    udSubClassificationList.Add(udSubClass);
                }
            }

            var dataReg = Task.Run(() => _service.GetAdaptiveFilterList("RegionalID", "RegionalID")).Result;
            List<LookupItem> dataEntityREGList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(explorationStructure.SubholdingID))
            {
                if (dataReg.Items.Count() > 0)
                {
                    foreach (var item in dataReg.Items)
                    {
                        LookupItem expEnt = new LookupItem();
                        expEnt.Text = Task.Run(() => _serviceVW.GetLookupText(item.Value.Trim())).Result;
                        expEnt.Description = "";
                        expEnt.Value = item.Value;
                        if (item.Value.Trim() == explorationStructure.RegionalID.Trim())
                        {
                            expEnt.Selected = true;
                        }
                        dataEntityREGList.Add(expEnt);
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
            var dataCountry = Task.Run(() => _serviceBL.GetAdaptiveFilterList("CountriesID", "CountriesID")).Result;
            var fullNameCountry = Task.Run(() => _serviceVC.GetCountryByCountryID(explorationStructure.CountriesID)).Result;
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
            if (!string.IsNullOrEmpty(esdc.UDSubTypeParID))
            {
                if (datasUD.Count() > 0)
                {
                    foreach (var item in datasUD)
                    {
                        LookupItem subUD = new LookupItem();
                        subUD.Text = item.ParamValue1Text;
                        subUD.Description = "";
                        subUD.Value = item.ParamListID;
                        if (item.ParamListID.Trim() == esdc.UDSubTypeParID.Trim())
                        {
                            subUD.Selected = true;
                            expUDTypeName = item.ParamValue1Text;
                        }
                        subUDStatusList.Add(subUD);
                    }
                }
            }

            var datasClassification = Task.Run(() => _servicePL.GetLookupListText("UDClassification")).Result;
            List<LookupItem> uDClassificationList = new List<LookupItem>();
            if (!string.IsNullOrEmpty(esdc.UDClassificationParID))
            {
                if (datasClassification.Count() > 0)
                {
                    foreach (var item in datasClassification)
                    {
                        LookupItem uDClassification = new LookupItem();
                        uDClassification.Text = item.ParamValue1Text;
                        uDClassification.Description = "";
                        uDClassification.Value = item.ParamListID;
                        if (item.ParamListID.Trim() == esdc.UDClassificationParID.Trim())
                        {
                            uDClassification.Selected = true;
                            expClassification = item.ParamValue1Text;
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
                if (item.ParamListID.Trim() == esdc.xStructureStatusParID.Trim())
                {
                    expStatus.Selected = true;
                    expStatusName = item.ParamValue1Text;
                }
                ExpStatusList.Add(expStatus);
            }

            var expBlockName = "";
            var expBasinName = "";
            var expAssetName = "";
            var expAreaName = "";

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
                    if (i.ToString().Trim() == esdc.ExplorationTypeParID.Trim())
                    {
                        expType.Selected = true;
                        expStatusTypeName = "Frontier";
                    }
                    ExpTypeList.Add(expType);
                }
                else
                {
                    LookupItem expType = new LookupItem();
                    expType.Text = "Nearby";
                    expType.Description = "";
                    expType.Value = i.ToString();
                    if (i.ToString().Trim() == esdc.ExplorationTypeParID.Trim())
                    {
                        expType.Selected = true;
                        expStatusTypeName = "Nearby";
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
                if (item.xBlockID.Trim() == esdc.xBlockID.Trim())
                {
                    expBlock.Selected = true;
                    expBlockName = item.xBlockName;
                }
                expBlockList.Add(expBlock);
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
                        OrderBy = "BasinName asc",
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
                if (item.BasinID.Trim() == esdc.BasinID.Trim())
                {
                    expBasin.Selected = true;
                    expBasinName = item.BasinName;
                }
                expBasinList.Add(expBasin);
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
                        OrderBy = "xAssetName asc",
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
                if (item.xAssetID.Trim() == esdc.xAssetID.Trim())
                {
                    expAsset.Selected = true;
                    expAssetName = item.xAssetName;
                }
                expAssetList.Add(expAsset);
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
                if (item.xAreaID.Trim() == esdc.ExplorationAreaParID.Trim())
                {
                    expArea.Selected = true;
                    expAreaName = item.xAreaName;
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
                if (item.ParamListID.Trim() == esdc.SingleOrMultiParID.Trim())
                {
                    singleMulti.Selected = true;
                    expSingleMultiName = item.ParamValue1Text;
                }
                singleMultiList.Add(singleMulti);
            }

            return new FieldSection
            {
                SectionName = "General Info",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xStructureID),
                        FieldType = FieldType.Hidden,
                        Value = paramID
                    }, //kiri hidden
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        Value = esdc.xStructureName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.ExplorationAreaParID),
                        Label = "Exp.Area",
                        FieldType = FieldType.Text,
                        Value = expAreaName,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xStructureStatusParID),
                        Label = "Exploration Status",
                        FieldType = FieldType.Text,
                        Value = expStatusName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.ExplorationTypeParID),
                        Label = "Exploration Type",
                        FieldType = FieldType.Text,
                        Value = expStatusTypeName,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text,
                        Value = explorationStructure.Play,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.SubholdingID),
                        Label = "Subholding ID",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.SubholdingID,
                        LookUpList = new LookupList
                        {
                            Items = dataEntitySHUList
                        },
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.UDSubTypeParID),
                        Label = "UD Type",
                        FieldType = FieldType.Text,
                        Value = expUDTypeName,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Text,
                        Value = fullNameCountry,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.UDClassificationParID),
                        Label = "UD Classification",
                        FieldType = FieldType.Text,
                        Value = expClassification,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Dropdown,
                        Value = esdc.RegionalID,
                        LookUpList = new LookupList
                        {
                            Items = dataEntityREGList
                        },
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.UDSubClassificationParID),
                        Label = "UD Sub Classification",
                        FieldType = FieldType.Text,
                        Value = expSubClassification,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.ZonaID),
                        Label = "Zona",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.ZonaID,
                        LookUpList = new LookupList
                        {
                            Items = dataZonaList
                        },
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDEntityDto.EffectiveYear),
                        Label = "RKAP Plan",
                        FieldType = FieldType.Text,
                        Value = valueEffective,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Text,
                        Value = expBlockName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        FieldType = FieldType.Text,
                        Value = expSingleMultiName,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.APHID),
                        Label = "APH",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.APHID,
                        LookUpList = new LookupList
                        {
                            Items = dataAPHList
                        },
                        IsDisabled = true
                    }, //kiri
                    //new Field {
                    //    Id = nameof(MDExplorationStructureESDCDto.OnePagerMontage),
                    //    Label = "One Pager Montage",
                    //    FieldType = FieldType.FileUpload,
                    //}, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Text,
                        Value = expAssetName,
                    }, //kiri
                    //new Field {
                    //    Id = nameof(MDExplorationStructureESDCDto.StructureOutline),
                    //    Label = "Structure Outline",
                    //    FieldType = FieldType.FileUpload,
                    //}, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Text,
                        Value = expBasinName,
                    }, //kiri
                }
            };
        }
        private FieldSection FieldESDCProduction()
        {
            return new FieldSection
            {
                SectionName = "Production",
            };
        }
        private FieldSection FieldESDCVolumetric()
        {
            return new FieldSection
            {
                SectionName = "Volumetric",
            };
        }
        private FieldSection FieldESDCForecast()
        {
            return new FieldSection
            {
                SectionName = "Forecast",
            };
        }
        private FieldSection FieldESDCDiscrepancy()
        {
            return new FieldSection
            {
                SectionName = "Discrepancy",
            };
        }
        private FieldSection FieldESDCInPlace()
        {
            return new FieldSection
            {
                SectionName = "InPlace",
            };
        }

        // Header grid
        protected override List<ColumnDefinition> DefineGrid()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("StructureID", nameof(MDExplorationStructureWithAdditionalFields.xStructureID), ColumnType.String,"esdc.xStructureID"),
                new ColumnDefinition("Exploration Structure", nameof(MDExplorationStructureWithAdditionalFields.xStructureName), ColumnType.String),
                new ColumnDefinition("Exploration Status", nameof(MDExplorationStructureWithAdditionalFields.ParamValue1Text), ColumnType.String),
                new ColumnDefinition("Zona", nameof(MDExplorationStructureWithAdditionalFields.ZonaName), ColumnType.String, "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.ZonaID)"),
                new ColumnDefinition("Basin", nameof(MDExplorationStructureWithAdditionalFields.BasinName), ColumnType.String,"ba.BasinName"),
                new ColumnDefinition("Region", nameof(MDExplorationStructureWithAdditionalFields.RegionalName), ColumnType.String,"(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.RegionalID)"),
                new ColumnDefinition("Block", nameof(MDExplorationStructureWithAdditionalFields.xBlockName), ColumnType.String, "bl.xBlockName"),
                new ColumnDefinition("RKAP Year", nameof(MDExplorationStructureWithAdditionalFields.RKAPFiscalYear), ColumnType.String,"COALESCE((select DISTINCT dr.RKAPFiscalYear from xplore.TX_Drilling dr where dr.xStructureID = esdc.xStructureID),0)"),
                new ColumnDefinition("ESDC Status", nameof(MDExplorationStructureWithAdditionalFields.StatusData), ColumnType.String,"esdc.StatusData"),
                new ColumnDefinition("Created Date", nameof(MDExplorationStructureWithAdditionalFields.CreatedDate), ColumnType.DateTime,"esdc.CreatedDate"),
                new ColumnDefinition("Created By", nameof(MDExplorationStructureWithAdditionalFields.CreatedBy), ColumnType.String,"COALESCE(esdc.CreatedBy,'')"),
                new ColumnDefinition("Updated Date", nameof(MDExplorationStructureWithAdditionalFields.UpdatedDate), ColumnType.DateTime,"esdc.UpdatedDate"),
                new ColumnDefinition("Updated By", nameof(MDExplorationStructureWithAdditionalFields.UpdatedBy), ColumnType.String,"COALESCE(esdc.UpdatedBy,'')"),
            };
        }

        protected override List<ColumnDefinition> DefineGridProsResource()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("StructureID", nameof(MDExplorationStructureWithAdditionalFields.xStructureID), ColumnType.String),
                new ColumnDefinition("TRR/Prospect/Lead", nameof(MDExplorationStructureDto.xStructureStatusParID), ColumnType.String),
                new ColumnDefinition("Name", nameof(MDExplorationStructureDto.xStructureName), ColumnType.String),
                new ColumnDefinition("Objectives", nameof(MDExplorationStructureDto.Play), ColumnType.String),
                new ColumnDefinition("Region", nameof(MDExplorationStructureWithAdditionalFields.RegionalID), ColumnType.String),
                new ColumnDefinition("WK", nameof(MDExplorationStructureWithAdditionalFields.xBlockName), ColumnType.String),
                new ColumnDefinition("Zona", nameof(MDExplorationStructureWithAdditionalFields.ZonaID), ColumnType.String),
                new ColumnDefinition("PI Pertamina", nameof(MDExplorationBlockPartnerDto.PartnerName), ColumnType.String),

                new ColumnDefinition("Source Rock", nameof(TXProsResourcesTargetDto.GCFSR), ColumnType.Number),
                new ColumnDefinition("Migration & Timing", nameof(TXProsResourcesTargetDto.GCFTM), ColumnType.Number),
                new ColumnDefinition("Reservoir", nameof(TXProsResourcesTargetDto.GCFReservoir), ColumnType.Number),
                new ColumnDefinition("Closure", nameof(TXProsResourcesTargetDto.GCFClosure), ColumnType.Number),
                new ColumnDefinition("Containment", nameof(TXProsResourcesTargetDto.GCFContainment), ColumnType.Number),
                new ColumnDefinition("Pg", nameof(TXProsResourcesTargetDto.GCFPGTotal), ColumnType.Number),

                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Low)", nameof(TXProsResourcesTargetDto.P90InPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Best)", nameof(TXProsResourcesTargetDto.P50InPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Mean)", nameof(TXProsResourcesTargetDto.PMeanInPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (High)", nameof(TXProsResourcesTargetDto.P10InPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (1U)", nameof(TXProsResourcesTargetDto.P90RecoverableOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (2U)", nameof(TXProsResourcesTargetDto.P50RecoverableOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (Mean)", nameof(TXProsResourcesTargetDto.PMeanRecoverableOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (3U)", nameof(TXProsResourcesTargetDto.P10RecoverableOil), ColumnType.Number),

                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Low)", nameof(TXProsResourcesTargetDto.P90InPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Best)", nameof(TXProsResourcesTargetDto.P50InPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Mean)", nameof(TXProsResourcesTargetDto.PMeanInPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (High)", nameof(TXProsResourcesTargetDto.P10InPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (1U)", nameof(TXProsResourcesTargetDto.P90RecoverableGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (2U)", nameof(TXProsResourcesTargetDto.P50RecoverableGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (Mean)", nameof(TXProsResourcesTargetDto.PMeanRecoverableGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (3U)", nameof(TXProsResourcesTargetDto.P10RecoverableGas), ColumnType.Number)
            };
        }

        protected override List<ColumnDefinition> DefineGridRJPP()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("StructureID", nameof(MDExplorationStructureWithAdditionalFields.xStructureID), ColumnType.String),
                new ColumnDefinition("TRR/Prospect/Lead", nameof(ProsResourceExcelDto.ParamValue1Text), ColumnType.String),
                new ColumnDefinition("Name", nameof(MDExplorationStructureDto.xStructureName), ColumnType.String),
                new ColumnDefinition("Objectives", nameof(MDExplorationStructureDto.Play), ColumnType.String),
                new ColumnDefinition("Region", nameof(MDExplorationStructureWithAdditionalFields.RegionalID), ColumnType.String),
                new ColumnDefinition("WK", nameof(MDExplorationStructureWithAdditionalFields.xBlockName), ColumnType.String),
                new ColumnDefinition("Zona", nameof(MDExplorationStructureWithAdditionalFields.ZonaID), ColumnType.String),
                new ColumnDefinition("PI Pertamina", nameof(MDExplorationBlockPartnerDto.PartnerName), ColumnType.String),

                new ColumnDefinition("Source Rock", nameof(TXProsResourcesTargetDto.GCFSR), ColumnType.Number),
                new ColumnDefinition("Migration & Timing", nameof(TXProsResourcesTargetDto.GCFTM), ColumnType.Number),
                new ColumnDefinition("Reservoir", nameof(TXProsResourcesTargetDto.GCFReservoir), ColumnType.Number),
                new ColumnDefinition("Closure", nameof(TXProsResourcesTargetDto.GCFClosure), ColumnType.Number),
                new ColumnDefinition("Containment", nameof(TXProsResourcesTargetDto.GCFContainment), ColumnType.Number),
                new ColumnDefinition("Pg", nameof(TXProsResourcesTargetDto.GCFPGTotal), ColumnType.Number),

                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Low)", nameof(TXProsResourcesTargetDto.P90InPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Best)", nameof(TXProsResourcesTargetDto.P50InPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Mean)", nameof(TXProsResourcesTargetDto.PMeanInPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (High)", nameof(TXProsResourcesTargetDto.P10InPlaceOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (1U)", nameof(TXProsResourcesTargetDto.P90RecoverableOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (2U)", nameof(TXProsResourcesTargetDto.P50RecoverableOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (Mean)", nameof(TXProsResourcesTargetDto.PMeanRecoverableOil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (3U)", nameof(TXProsResourcesTargetDto.P10RecoverableOil), ColumnType.Number),

                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Low)", nameof(TXProsResourcesTargetDto.P90InPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Best)", nameof(TXProsResourcesTargetDto.P50InPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Mean)", nameof(TXProsResourcesTargetDto.PMeanInPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (High)", nameof(TXProsResourcesTargetDto.P10InPlaceGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (1U)", nameof(TXProsResourcesTargetDto.P90RecoverableGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (2U)", nameof(TXProsResourcesTargetDto.P50RecoverableGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (Mean)", nameof(TXProsResourcesTargetDto.PMeanRecoverableGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (3U)", nameof(TXProsResourcesTargetDto.P10RecoverableGas), ColumnType.Number)
            };
        }

        protected override async Task<ExportExcelExploration> GetDataExportExcel(string xStructureID)
        {
            ExportExcelExploration exportExploreObj = new ExportExcelExploration();
            exportExploreObj.ExplorationStructure = await _serviceES.GetExportExplorationStructureByStructureID(xStructureID);
            exportExploreObj.ProsResourcesTarget = await _servicePT.GetExportProsResourceTargetByStructureID(xStructureID);
            exportExploreObj.ProsResources = await _servicePR.GetExportProsResourceByStructureID(xStructureID);
            exportExploreObj.ContResources = await _serviceCR.GetExportContResourceTargetByStructureID(xStructureID);
            exportExploreObj.Drilling = await _serviceDR.GetExportDrillingByStructureID(xStructureID);
            exportExploreObj.Economic = await _serviceEC.GetExportEconomicByStructureID(xStructureID);

            return exportExploreObj;
        }
        protected List<ColumnDefinition> DefineExplorationAreaGrid()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("Area ID", nameof(MDExplorationAreaDto.xAreaID), ColumnType.String),
                new ColumnDefinition("Area Name", nameof(MDExplorationAreaDto.xAreaName), ColumnType.String),
            };
        }

        public async Task<PartialViewResult> GetAdaptiveFilterESDC(string columnId, string usernameSession)
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
            var selectList = await _esdcService.GetAdaptiveFilterListESDC(columnId, hrisObj);
            return PartialView("Component/Grid/AdaptiveFilter/CheckboxList", selectList);
        }

    }
}