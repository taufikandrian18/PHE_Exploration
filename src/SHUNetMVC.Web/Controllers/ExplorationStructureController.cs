using ASPNetMVC.Abstraction.Model.Entities;
using Newtonsoft.Json;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.Constant;
using SHUNetMVC.Infrastructure.Validators;
using SHUNetMVC.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class ExplorationStructureController : BaseCrudController<MDExplorationStructureDto, MDExplorationStructureWithAdditionalFields>
    {
        private readonly IMDExplorationStructureService _explorationStructureService;
        private readonly ILGExplorationStructureService _explorationStructureServiceLG;
        private readonly IMDParameterListService _servicePL;
        private readonly IMDExplorationBlockService _serviceBL;
        private readonly IMDExplorationBasinService _serviceBS;
        private readonly IMDExplorationAssetService _serviceAS;
        private readonly IMDExplorationAreaService _serviceAR;
        private readonly ITXDrillingService _serviceDR;
        private readonly ILGDrillingService _serviceDRLG;
        private readonly ITXProsResourcesService _servicePR;
        private readonly ILGProsResourcesService _servicePRLG;
        private readonly ITXProsResourcesTargetService _servicePT;
        private readonly ILGProsResourcesTargetService _servicePTLG;
        private readonly ITXContingenResourcesService _serviceCR;
        private readonly ILGContingenResourcesService _serviceCRLG;
        private readonly ILGActivityService _serviceAC;
        private readonly IMDExplorationBlockPartnerService _serviceBP;
        private readonly IMDEntityService _service;
        private readonly ITXEconomicService _serviceEC;
        private readonly ILGEconomicService _serviceECLG;
        private readonly HttpContextBase _httpContextBase;
        private readonly IUserService _userServiceObj;
        private readonly IVWDIMEntityService _serviceVW;
        private readonly IVWCountryService _serviceVC;
        private readonly IHRISDevOrgUnitHierarchyService _serviceHrObj;
        public ExplorationStructureController(IMDExplorationStructureService crudSvc,
            ILGExplorationStructureService crudSvcLG,
            ILookupService lookupSvc,
            IMDEntityService service,
            IMDParameterListService servicePL,
            IMDExplorationBlockService serviceBL,
            IMDExplorationBasinService serviceBS,
            IMDExplorationAssetService serviceAS,
            IMDExplorationAreaService serviceAR,
            ITXDrillingService serviceDR,
            ILGDrillingService serviceDRLG,
            ITXProsResourcesService servicePR,
            ILGProsResourcesService servicePRLG,
            ITXEconomicService serviceEC,
            ILGEconomicService serviceECLG,
            HttpContextBase httpContextBase,
            ITXProsResourcesTargetService servicePT,
            ILGProsResourcesTargetService servicePTLG,
            ITXContingenResourcesService serviceCR,
            ILGContingenResourcesService serviceCRLG,
            ILGActivityService serviceAC,
            IMDExplorationBlockPartnerService serviceBP,
            IVWDIMEntityService serviceVW,
            IVWCountryService serviceVC,
            IHRISDevOrgUnitHierarchyService serviceHrObj,
            IUserService userServiceObj) : base(crudSvc, lookupSvc, userServiceObj, serviceHrObj, httpContextBase)
        {
            _explorationStructureService = crudSvc;
            _explorationStructureServiceLG = crudSvcLG;
            _service = service;
            _servicePL = servicePL;
            _serviceBL = serviceBL;
            _serviceBS = serviceBS;
            _serviceAS = serviceAS;
            _serviceAR = serviceAR;
            _serviceDR = serviceDR;
            _serviceDRLG = serviceDRLG;
            _servicePR = servicePR;
            _servicePRLG = servicePRLG;
            _serviceEC = serviceEC;
            _serviceECLG = serviceECLG;
            _httpContextBase = httpContextBase;
            _userServiceObj = userServiceObj;
            _servicePT = servicePT;
            _servicePTLG = servicePTLG;
            _serviceCR = serviceCR;
            _serviceCRLG = serviceCRLG;
            _serviceAC = serviceAC;
            _serviceBP = serviceBP;
            _serviceVW = serviceVW;
            _serviceVC = serviceVC;
            _serviceHrObj = serviceHrObj;
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
                SectionName = "Exploration Structure",
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
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID),
                        Label = "Exploration Status",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ZonaID),
                        Label = "Zona",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Text
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StatusData),
                        Label = "Data Status",
                        FieldType = FieldType.Text
                    },
                }
            };
        }

        protected override List<ColumnDefinition> DefineGrid()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("StructureID", nameof(MDExplorationStructureWithAdditionalFields.xStructureID), ColumnType.String),
                new ColumnDefinition("Exploration Structure", nameof(MDExplorationStructureWithAdditionalFields.xStructureName), ColumnType.String),
                new ColumnDefinition("Exploration Status", nameof(MDExplorationStructureWithAdditionalFields.ParamValue1Text), ColumnType.String),
                new ColumnDefinition("Zona", nameof(MDExplorationStructureWithAdditionalFields.ZonaName), ColumnType.String, "(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.ZonaID)"),
                new ColumnDefinition("Basin", nameof(MDExplorationStructureWithAdditionalFields.BasinName), ColumnType.String,"ba.BasinName"),
                new ColumnDefinition("Region", nameof(MDExplorationStructureWithAdditionalFields.RegionalName), ColumnType.String,"(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.RegionalID)"),
                new ColumnDefinition("Block", nameof(MDExplorationStructureWithAdditionalFields.xBlockName), ColumnType.String, "bl.xBlockName"),
                new ColumnDefinition("Country", nameof(MDExplorationStructureWithAdditionalFields.CountriesID), ColumnType.String, "s.CountriesID"),
                new ColumnDefinition("TX Status", nameof(MDExplorationStructureWithAdditionalFields.StatusData), ColumnType.String),
                new ColumnDefinition("Created Date", nameof(MDExplorationStructureWithAdditionalFields.CreatedDate), ColumnType.DateTime,"s.CreatedDate"),
                new ColumnDefinition("Created By", nameof(MDExplorationStructureWithAdditionalFields.CreatedBy), ColumnType.String,"COALESCE(s.CreatedBy,'')"),
                new ColumnDefinition("Updated Date", nameof(MDExplorationStructureWithAdditionalFields.UpdatedDate), ColumnType.DateTime,"s.UpdatedDate"),
                new ColumnDefinition("Updated By", nameof(MDExplorationStructureWithAdditionalFields.UpdatedBy), ColumnType.String,"COALESCE(s.UpdatedBy,'')")
            };
        }

        protected override List<ColumnDefinition> DefineGridProsResource()
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
                new ColumnDefinition("PI Pertamina", nameof(MDExplorationBlockPartnerDto.PI), ColumnType.Number),

                new ColumnDefinition("Source Rock", nameof(TXProsResourceDto.GCFSRPR), ColumnType.Number),
                new ColumnDefinition("Migration & Timing", nameof(TXProsResourceDto.GCFTMPR), ColumnType.Number),
                new ColumnDefinition("Reservoir", nameof(TXProsResourceDto.GCFReservoirPR), ColumnType.Number),
                new ColumnDefinition("Closure", nameof(TXProsResourceDto.GCFClosurePR), ColumnType.Number),
                new ColumnDefinition("Containment", nameof(TXProsResourceDto.GCFContainmentPR), ColumnType.Number),
                new ColumnDefinition("Pg", nameof(TXProsResourceDto.GCFPGTotalPR), ColumnType.Number),

                new ColumnDefinition("PI 100% - Initial in Place MSTB (Low)", nameof(TXPropectiveResourceTargetWthFields.PIP90InPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Initial in Place MSTB (Best)", nameof(TXPropectiveResourceTargetWthFields.PIP50InPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Initial in Place MSTB (Mean)", nameof(TXPropectiveResourceTargetWthFields.PIPMeanInPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Initial in Place MSTB (High)", nameof(TXPropectiveResourceTargetWthFields.PIP10InPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - RF MSTB", nameof(TXPropectiveResourceTargetWthFields.PIRFOilPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MSTB (1U)", nameof(TXPropectiveResourceTargetWthFields.PIP90RROil), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MSTB (2U)", nameof(TXPropectiveResourceTargetWthFields.PIP50RROil), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MSTB (Mean)", nameof(TXPropectiveResourceTargetWthFields.PIPMeanRROil), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MSTB (3U)", nameof(TXPropectiveResourceTargetWthFields.PIP10RROil), ColumnType.Number),

                new ColumnDefinition("PI 100% - Initial in Place MMSCF (Low)", nameof(TXPropectiveResourceTargetWthFields.PIP90InPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Initial in Place MMSCF (Best)", nameof(TXPropectiveResourceTargetWthFields.PIP50InPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Initial in Place MMSCF (Mean)", nameof(TXPropectiveResourceTargetWthFields.PIPMeanInPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Initial in Place MMSCF (High)", nameof(TXPropectiveResourceTargetWthFields.PIP10InPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - RF MMSCF", nameof(TXPropectiveResourceTargetWthFields.PIRFGasPR), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MMSCF (1U)", nameof(TXPropectiveResourceTargetWthFields.PIP90RRGas), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MMSCF (2U)", nameof(TXPropectiveResourceTargetWthFields.PIP50RRGas), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MMSCF (Mean)", nameof(TXPropectiveResourceTargetWthFields.PIPMeanRRGas), ColumnType.Number),
                new ColumnDefinition("PI 100% - Prospective Resources MMSCF (3U)", nameof(TXPropectiveResourceTargetWthFields.PIP10RRGas), ColumnType.Number),

                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Low)", nameof(TXProsResourceDto.P90InPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Best)", nameof(TXProsResourceDto.P50InPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (Mean)", nameof(TXProsResourceDto.PMeanInPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MSTB (High)", nameof(TXProsResourceDto.P10InPlaceOilPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - RF MSTB", nameof(TXProsResourceDto.RFOilPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (1U)", nameof(TXProsResourceDto.P90RROil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (2U)", nameof(TXProsResourceDto.P50RROil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (Mean)", nameof(TXProsResourceDto.PMeanRROil), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MSTB (3U)", nameof(TXProsResourceDto.P10RROil), ColumnType.Number),

                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Low)", nameof(TXProsResourceDto.P90InPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Best)", nameof(TXProsResourceDto.P50InPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (Mean)", nameof(TXProsResourceDto.PMeanInPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Initial in Place MMSCF (High)", nameof(TXProsResourceDto.P10InPlaceGasPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - RF MMSCF", nameof(TXProsResourceDto.RFGasPR), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (1U)", nameof(TXProsResourceDto.P90RRGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (2U)", nameof(TXProsResourceDto.P50RRGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (Mean)", nameof(TXProsResourceDto.PMeanRRGas), ColumnType.Number),
                new ColumnDefinition("Net Pertamina Interest - Prospective Resources MMSCF (3U)", nameof(TXProsResourceDto.P10RRGas), ColumnType.Number)
            };
        }

        protected override List<ColumnDefinition> DefineGridRJPP()
        {
            return new List<ColumnDefinition>
            {
                new ColumnDefinition("StructureID", nameof(ExploreRJPPExcelDto.xStructureID), ColumnType.String),
                new ColumnDefinition("Well Name", nameof(ExploreRJPPExcelDto.xWellName), ColumnType.String),
                new ColumnDefinition("Play Opener", nameof(ExploreRJPPExcelDto.PlayOpener), ColumnType.String),
                new ColumnDefinition("Group", nameof(ExploreRJPPExcelDto.GroupDrill), ColumnType.String),
                new ColumnDefinition("SpudDate", nameof(ExploreRJPPExcelDto.ExpectedDrillingDate), ColumnType.DateTime),
                new ColumnDefinition("Drilling + Completion Period", nameof(ExploreRJPPExcelDto.DrillingCompletionPeriod), ColumnType.Number),
                new ColumnDefinition("Location", nameof(ExploreRJPPExcelDto.Location), ColumnType.String),
                new ColumnDefinition("APH", nameof(ExploreRJPPExcelDto.APHName), ColumnType.String),
                new ColumnDefinition("AP", nameof(ExploreRJPPExcelDto.xAssetName), ColumnType.String),
                new ColumnDefinition("Play", nameof(ExploreRJPPExcelDto.Play), ColumnType.String),
                new ColumnDefinition("Prospect Type", nameof(ExploreRJPPExcelDto.ParamValue1Text), ColumnType.String),
                new ColumnDefinition("UD Cassification K7", nameof(ExploreRJPPExcelDto.ParamValue1TextUDClass), ColumnType.String),
                new ColumnDefinition("Single or Multizone", nameof(ExploreRJPPExcelDto.ParamValue1TextSingleMulti), ColumnType.String),
                new ColumnDefinition("Well Type", nameof(ExploreRJPPExcelDto.WellTypeParID), ColumnType.String),
                new ColumnDefinition("Operating Status", nameof(ExploreRJPPExcelDto.ParamValue1TextOperatorStatus), ColumnType.String),
                new ColumnDefinition("Commitment Well", nameof(ExploreRJPPExcelDto.CommitmentWell), ColumnType.String),
                new ColumnDefinition("Operating Environment", nameof(ExploreRJPPExcelDto.DrillingLocation), ColumnType.String),
                new ColumnDefinition("Operational Context", nameof(ExploreRJPPExcelDto.OperationalContext), ColumnType.String),
                new ColumnDefinition("Expected TD Depth", nameof(ExploreRJPPExcelDto.TotalDepthMeter), ColumnType.Number),
                new ColumnDefinition("Potential Delay", nameof(ExploreRJPPExcelDto.PotentialDelay), ColumnType.String),
                new ColumnDefinition("Participating Interest", nameof(ExploreRJPPExcelDto.PartnerName), ColumnType.Number),
                new ColumnDefinition("Net Revenue Interest", nameof(ExploreRJPPExcelDto.NetRevenueInterest), ColumnType.Number),
                new ColumnDefinition("Resources (Oil - P90)", nameof(ExploreRJPPExcelDto.P90ResourceOil), ColumnType.Number),
                new ColumnDefinition("Resources (Oil - P50)", nameof(ExploreRJPPExcelDto.P50ResourceOil), ColumnType.Number),
                new ColumnDefinition("Resources (Oil - P10)", nameof(ExploreRJPPExcelDto.P10ResourceOil), ColumnType.Number),
                new ColumnDefinition("Resources (Gas - P90)", nameof(ExploreRJPPExcelDto.P90ResourceGas), ColumnType.Number),
                new ColumnDefinition("Resources (Gas - P50)", nameof(ExploreRJPPExcelDto.P50ResourceGas), ColumnType.Number),
                new ColumnDefinition("Resources (Gas - P10)", nameof(ExploreRJPPExcelDto.P10ResourceGas), ColumnType.Number),
                new ColumnDefinition("Current Pg", nameof(ExploreRJPPExcelDto.CurrentPG), ColumnType.Number),
                new ColumnDefinition("Expected Pg", nameof(ExploreRJPPExcelDto.ExpectedPG), ColumnType.Number),
                new ColumnDefinition("Chance Components (Source)", nameof(ExploreRJPPExcelDto.ChanceComponentSource), ColumnType.Number),
                new ColumnDefinition("Chance Components (Timing/Migration)", nameof(ExploreRJPPExcelDto.ChanceComponentTiming), ColumnType.Number),
                new ColumnDefinition("Chance Components (Reservoir)", nameof(ExploreRJPPExcelDto.ChanceComponentReservoir), ColumnType.Number),
                new ColumnDefinition("Chance Components (Closure)", nameof(ExploreRJPPExcelDto.ChanceComponentClosure), ColumnType.Number),
                new ColumnDefinition("Chance Components (Containment)", nameof(ExploreRJPPExcelDto.ChanceComponentContainment), ColumnType.Number),
                new ColumnDefinition("Well Cost (DHB)", nameof(ExploreRJPPExcelDto.DrillingCostDHB), ColumnType.Number),
                new ColumnDefinition("Well Cost (CHB)", nameof(ExploreRJPPExcelDto.DrillingCost), ColumnType.Number),
                new ColumnDefinition("NPV Profitability (Oil - P90)", nameof(ExploreRJPPExcelDto.P90NPVProfitabilityOil), ColumnType.Number),
                new ColumnDefinition("NPV Profitability (Oil - P50)", nameof(ExploreRJPPExcelDto.P50NPVProfitabilityOil), ColumnType.Number),
                new ColumnDefinition("NPV Profitability (Oil - P10)", nameof(ExploreRJPPExcelDto.P10NPVProfitabilityOil), ColumnType.Number),
                new ColumnDefinition("NPV Profitability (Gas - P90)", nameof(ExploreRJPPExcelDto.P90NPVProfitabilityGas), ColumnType.Number),
                new ColumnDefinition("NPV Profitability (Gas - P50)", nameof(ExploreRJPPExcelDto.P50NPVProfitabilityGas), ColumnType.Number),
                new ColumnDefinition("NPV Profitability (Gas - P10)", nameof(ExploreRJPPExcelDto.P10NPVProfitabilityGas), ColumnType.Number)
            };
        }



        protected override async Task<ExportExcelExploration> GetDataExportExcel(string xStructureID)
        {
            ExportExcelExploration exportExploreObj = new ExportExcelExploration();
            exportExploreObj.ExplorationStructure = await _explorationStructureService.GetExportExplorationStructureByStructureID(xStructureID);
            exportExploreObj.ProsResourcesTarget = await _servicePT.GetExportProsResourceTargetByStructureID(xStructureID);
            exportExploreObj.ProsResources = await _servicePR.GetExportProsResourceByStructureID(xStructureID);
            exportExploreObj.ContResources = await _serviceCR.GetExportContResourceTargetByStructureID(xStructureID);
            exportExploreObj.Drilling = await _serviceDR.GetExportDrillingByStructureID(xStructureID);
            exportExploreObj.Economic = await _serviceEC.GetExportEconomicByStructureID(xStructureID);

            return exportExploreObj;
        }


        public override async Task<ActionResult> Create([Bind(Exclude = "")] MDExplorationStructureDto model)
        {
            return await BaseCreate(model, new MDExplorationStructureValidator(FormState.Create, _explorationStructureService));
        }

        public override async Task<ActionResult> Edit([Bind(Exclude = "")] MDExplorationStructureDto model)
        {
            return await BaseUpdate(model, new MDExplorationStructureValidator(FormState.Edit, _explorationStructureService));
        }

        public ActionResult Review()
        {
            var username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var model = new CrudPage
            {
                Id = "ExplorationStructureReview",
                Title = "Pencatatan Sumber Daya",
                SubTitle = "This is the list of Exploration Structure",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "Review" + "_grid",
                    ColumnDefinitions = DefineGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "[xStructureID] desc",
                        Page = 1,
                        Size = 20
                    }
                },
                UsernameSession = username
            };
            return View(model);
        }

        public ActionResult Report()
        {
            var username = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
            var model = new CrudPage
            {
                Id = "ExplorationStructureReview",
                Title = "Pencatatan Sumber Daya",
                SubTitle = "This is the list of Exploration Structure",
                GridParam = new GridParam
                {
                    GridId = this.GetType().Name + "Report" + "_grid",
                    ColumnDefinitions = DefineGrid(),
                    FilterList = new FilterList
                    {
                        OrderBy = "[xStructureID] desc",
                        Page = 1,
                        Size = 20
                    }
                },
                UsernameSession = username
            };
            return View(model);
        }

        [HttpPost]
        [Obsolete]
        public async Task<ActionResult> ExplorationUpdateStatus(string structureID, string statusData)
        {
            try
            {
                //string user = HttpContext.User.Identity.Name;
                var user = (_httpContext.User.Identity as ClaimsIdentity)?.Claims
                    .FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                var explorationStructure = await _explorationStructureService.GetOne(structureID);
                if (explorationStructure.StatusData.Trim() == "Submitted" && statusData.Trim() == "Reject Submitted")
                {
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
                                var actionStr = "Reject";
                                var userData = await _userService.GetUserInfo(user);
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER ADMIN")
                                    {
                                        actionStr = "Reject2";
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
                                                         "Web Exploration", "Reject Data", "-");

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
                                        return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                            }
                        }

                        if (!string.IsNullOrEmpty(transNumber))
                        {
                            explorationStructure.MadamTransID = transNumber.Trim();
                            explorationStructure.StatusData = statusData.Trim();
                            explorationStructure.UpdatedDate = DateTime.Now;
                            explorationStructure.UpdatedBy = user;
                        }
                    }
                    //insert log activity
                    LGActivityDto activityObj = new LGActivityDto();
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    activityObj.IP = myIP;
                    activityObj.Menu = "Review";
                    activityObj.Action = "Update";
                    activityObj.TransactionID = structureID;
                    activityObj.Status = explorationStructure.StatusData;
                    activityObj.CreatedDate = DateTime.Now;
                    activityObj.CreatedBy = user;
                    await _serviceAC.Create(activityObj);
                }
                else if (explorationStructure.StatusData.Trim() == "Submitted" && statusData.Trim() == "Approved")
                {
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
                                var actionStr = "Approve";
                                var userData = await _userService.GetUserInfo(user);
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER ADMIN")
                                    {
                                        actionStr = "Approve2";
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
                                                         "Web Exploration", "Approve Data", "-");

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
                                        return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                            }
                        }

                        if (!string.IsNullOrEmpty(transNumber))
                        {
                            explorationStructure.MadamTransID = transNumber.Trim();
                            explorationStructure.StatusData = statusData.Trim();
                            explorationStructure.UpdatedDate = DateTime.Now;
                            explorationStructure.UpdatedBy = user;
                        }
                    }
                    //insert log activity
                    LGActivityDto activityObj = new LGActivityDto();
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    activityObj.IP = myIP;
                    activityObj.Menu = "Review";
                    activityObj.Action = "Update";
                    activityObj.TransactionID = structureID;
                    activityObj.Status = explorationStructure.StatusData;
                    activityObj.CreatedDate = DateTime.Now;
                    activityObj.CreatedBy = user;
                    await _serviceAC.Create(activityObj);
                }
                else if (explorationStructure.StatusData.Trim() == "Approved" && statusData.Trim() == "Released")
                {
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
                                var actionStr = "Release";
                                var userData = await _userService.GetUserInfo(user);
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER ADMIN")
                                    {
                                        actionStr = "Release2";
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
                                                         "Web Exploration", "Release Data", "-");

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
                                        return Json(new { success = false, Message = "Workflow Failed" }, JsonRequestBehavior.AllowGet);
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed" }, JsonRequestBehavior.AllowGet);
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (!string.IsNullOrEmpty(transNumber))
                        {
                            explorationStructure.MadamTransID = transNumber.Trim();
                            explorationStructure.StatusData = statusData.Trim();
                            explorationStructure.UpdatedDate = DateTime.Now;
                            explorationStructure.UpdatedBy = user;
                        }
                    }
                    //Get data Log History
                    ExportExcelExploration exportExploreObj = new ExportExcelExploration();
                    MDExplorationStructureDto exploreStructureObj = new MDExplorationStructureDto();
                    exploreStructureObj = await _explorationStructureService.GetOne(structureID);
                    exportExploreObj.ProsResourcesTarget = await _servicePT.GetExportProsResourceTargetByStructureID(structureID);
                    exportExploreObj.ProsResources = await _servicePR.GetExportProsResourceByStructureID(structureID);
                    var contResources = await _serviceCR.GetContResourceTargetByStructureID(structureID);
                    exportExploreObj.Drilling = await _serviceDR.GetExportDrillingByStructureID(structureID);
                    exportExploreObj.Economic = await _serviceEC.GetExportEconomicByStructureID(structureID);
                    //Create StructureHistoryId
                    List<LG_ExplorationStructure> getKey = new List<LG_ExplorationStructure>();
                    getKey = _explorationStructureServiceLG.GetByStructureID(structureID);
                    if (getKey.Count == 0)
                    {
                        var structureHistoryId = structureID + "_Log";
                        if (exploreStructureObj.xStructureStatusParID != "4")
                        {
                            #region ExplorationStructure
                            //Insert Log_ExplorationStructure
                            LGExplorationStructureDto esObj = new LGExplorationStructureDto();
                            esObj.StructureHistoryID = structureHistoryId;
                            esObj.xStructureID = structureID;
                            esObj.xStructureName = exploreStructureObj.xStructureName;
                            esObj.xStructureStatusParID = exploreStructureObj.xStructureStatusParID;
                            esObj.SingleOrMultiParID = exploreStructureObj.SingleOrMultiParID;
                            esObj.ExplorationTypeParID = exploreStructureObj.ExplorationTypeParID;
                            esObj.SubholdingID = exploreStructureObj.SubholdingID;
                            esObj.BasinID = exploreStructureObj.BasinID;
                            esObj.RegionalID = exploreStructureObj.RegionalID;
                            esObj.ZonaID = exploreStructureObj.ZonaID;
                            esObj.APHID = exploreStructureObj.APHID;
                            esObj.xAssetID = exploreStructureObj.xAssetID;
                            esObj.xBlockID = exploreStructureObj.xBlockID;
                            esObj.xAreaID = exploreStructureObj.xAreaID;
                            esObj.UDClassificationParID = exploreStructureObj.UDClassificationParID;
                            esObj.UDSubClassificationParID = exploreStructureObj.UDSubClassificationParID;
                            esObj.ExplorationAreaParID = exploreStructureObj.ExplorationAreaParID;
                            esObj.CountriesID = exploreStructureObj.CountriesID;
                            esObj.Play = exploreStructureObj.Play;
                            esObj.StatusData = "Release";
                            esObj.IsDeleted = false;
                            esObj.MadamTransID = "";
                            esObj.CreatedDate = DateTime.Now;
                            esObj.CreatedBy = user;
                            await _explorationStructureServiceLG.Create(esObj);
                            #endregion
                            #region ProsResources
                            //Insert Log_ProsResources
                            LGProsResourceDto prObj = new LGProsResourceDto();
                            if (exportExploreObj.ProsResources.Count() > 0)
                            {
                                prObj.StructureHistoryID = structureHistoryId;
                                prObj.xStructureID = exportExploreObj.ProsResources.FirstOrDefault().xStructureID;
                                prObj.P90InPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceOilPR;
                                prObj.P90InPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceOilPRUoM;
                                prObj.P50InPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceOilPR;
                                prObj.P50InPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceOilPRUoM;
                                prObj.PMeanInPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceOilPR;
                                prObj.PMeanInPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceOilPRUoM;
                                prObj.P10InPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceOilPR;
                                prObj.P10InPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceOilPRUoM;
                                prObj.P90InPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceGasPR;
                                prObj.P90InPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceGasPRUoM;
                                prObj.P50InPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceGasPR;
                                prObj.P50InPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceGasPRUoM;
                                prObj.PMeanInPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceGasPR;
                                prObj.PMeanInPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceGasPRUoM;
                                prObj.P10InPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceGasPR;
                                prObj.P10InPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceGasPRUoM;
                                prObj.P90InPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceTotalPR;
                                prObj.P90InPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceTotalPRUoM;
                                prObj.P50InPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceTotalPR;
                                prObj.P50InPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceTotalPRUoM;
                                prObj.PMeanInPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceTotalPR;
                                prObj.PMeanInPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceTotalPRUoM;
                                prObj.P10InPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceTotalPR;
                                prObj.P10InPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceTotalPRUoM;
                                prObj.RFOilPR = exportExploreObj.ProsResources.FirstOrDefault().RFOilPR;
                                prObj.RFGasPR = exportExploreObj.ProsResources.FirstOrDefault().RFGasPR;
                                prObj.P90RROil = exportExploreObj.ProsResources.FirstOrDefault().P90RROil;
                                prObj.P90RROilUoM = exportExploreObj.ProsResources.FirstOrDefault().P90RROilUoM;
                                prObj.P50RROil = exportExploreObj.ProsResources.FirstOrDefault().P50RROil;
                                prObj.P50RROilUoM = exportExploreObj.ProsResources.FirstOrDefault().P50RROilUoM;
                                prObj.PMeanRROil = exportExploreObj.ProsResources.FirstOrDefault().PMeanRROil;
                                prObj.PMeanRROilUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanRROilUoM;
                                prObj.P10RROil = exportExploreObj.ProsResources.FirstOrDefault().P10RROil;
                                prObj.P10RROilUoM = exportExploreObj.ProsResources.FirstOrDefault().P10RROilUoM;
                                prObj.P90RRGas = exportExploreObj.ProsResources.FirstOrDefault().P90RRGas;
                                prObj.P90RRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().P90RRGasUoM;
                                prObj.P50RRGas = exportExploreObj.ProsResources.FirstOrDefault().P50RRGas;
                                prObj.P50RRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().P50RRGasUoM;
                                prObj.PMeanRRGas = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRGas;
                                prObj.PMeanRRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRGasUoM;
                                prObj.P10RRGas = exportExploreObj.ProsResources.FirstOrDefault().P10RRGas;
                                prObj.P10RRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().P10RRGasUoM;
                                prObj.P90RRTotal = exportExploreObj.ProsResources.FirstOrDefault().P90RRTotal;
                                prObj.P90RRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().P90RRTotalUoM;
                                prObj.P50RRTotal = exportExploreObj.ProsResources.FirstOrDefault().P50RRTotal;
                                prObj.P50RRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().P50RRTotalUoM;
                                prObj.PMeanRRTotal = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRTotal;
                                prObj.PMeanRRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRTotalUoM;
                                prObj.P10RRTotal = exportExploreObj.ProsResources.FirstOrDefault().P10RRTotal;
                                prObj.P10RRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().P10RRTotalUoM;
                                prObj.HydrocarbonTypePRParID = exportExploreObj.ProsResources.FirstOrDefault().HydrocarbonTypePRParID;
                                prObj.GCFSRPR = exportExploreObj.ProsResources.FirstOrDefault().GCFSRPR;
                                prObj.GCFSRPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFSRPRUoM;
                                prObj.GCFTMPR = exportExploreObj.ProsResources.FirstOrDefault().GCFTMPR;
                                prObj.GCFTMPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFTMPRUoM;
                                prObj.GCFReservoirPR = exportExploreObj.ProsResources.FirstOrDefault().GCFReservoirPR;
                                prObj.GCFReservoirPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFReservoirPRUoM;
                                prObj.GCFClosurePR = exportExploreObj.ProsResources.FirstOrDefault().GCFClosurePR;
                                prObj.GCFClosurePRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFClosurePRUoM;
                                prObj.GCFContainmentPR = exportExploreObj.ProsResources.FirstOrDefault().GCFContainmentPR;
                                prObj.GCFContainmentPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFContainmentPRUoM;
                                prObj.GCFPGTotalPR = exportExploreObj.ProsResources.FirstOrDefault().GCFPGTotalPR;
                                prObj.GCFPGTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFPGTotalPRUoM;
                                prObj.ExpectedPG = exportExploreObj.ProsResources.FirstOrDefault().ExpectedPG;
                                prObj.CurrentPG = exportExploreObj.ProsResources.FirstOrDefault().CurrentPG;
                                prObj.MethodParID = exportExploreObj.ProsResources.FirstOrDefault().MethodParID;
                                prObj.CreatedDate = DateTime.Now;
                                prObj.CreatedBy = user;
                                await _servicePRLG.Create(prObj);
                            }
                            #endregion
                            #region ProResourcesTarget
                            //Insert Log_ProsResourcesTarget
                            LGProsResourcesTargetDto ptObj = new LGProsResourcesTargetDto();
                            if (exportExploreObj.ProsResourcesTarget.Count() > 0)
                            {
                                ptObj.StructureHistoryID = structureHistoryId;
                                ptObj.TargetID = exportExploreObj.ProsResourcesTarget.FirstOrDefault().TargetID;
                                ptObj.TargetName = "";
                                ptObj.xStructureID = exportExploreObj.ProsResourcesTarget.FirstOrDefault().xStructureID;
                                ptObj.P90InPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceOil;
                                ptObj.P90InPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceOilUoM;
                                ptObj.P50InPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceOil;
                                ptObj.P50InPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceOilUoM;
                                ptObj.PMeanInPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceOil;
                                ptObj.PMeanInPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceOilUoM;
                                ptObj.P10InPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceOil;
                                ptObj.P10InPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceOilUoM;
                                ptObj.P90InPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceGas;
                                ptObj.P90InPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceGasUoM;
                                ptObj.P50InPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceGas;
                                ptObj.P50InPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceGasUoM;
                                ptObj.PMeanInPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceGas;
                                ptObj.PMeanInPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceGasUoM;
                                ptObj.P10InPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceGas;
                                ptObj.P10InPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceGasUoM;
                                ptObj.P90InPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceTotal;
                                ptObj.P90InPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceTotalUoM;
                                ptObj.P50InPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceTotal;
                                ptObj.P50InPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceTotalUoM;
                                ptObj.PMeanInPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceTotal;
                                ptObj.PMeanInPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceTotalUoM;
                                ptObj.P10InPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceTotal;
                                ptObj.P10InPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceTotalUoM;
                                ptObj.RFOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().RFOil;
                                ptObj.RFGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().RFGas;
                                ptObj.P90RecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableOil;
                                ptObj.P90RecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableOilUoM;
                                ptObj.P50RecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableOil;
                                ptObj.P50RecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableOilUoM;
                                ptObj.PMeanRecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableOil;
                                ptObj.PMeanRecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableOilUoM;
                                ptObj.P10RecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableOil;
                                ptObj.P10RecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableOilUoM;
                                ptObj.P90RecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableGas;
                                ptObj.P90RecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableGasUoM;
                                ptObj.P50RecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableGas;
                                ptObj.P50RecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableGasUoM;
                                ptObj.PMeanRecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableGas;
                                ptObj.PMeanRecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableGasUoM;
                                ptObj.P10RecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableGas;
                                ptObj.P10RecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableGasUoM;
                                ptObj.P90RecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableTotal;
                                ptObj.P90RecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableTotalUoM;
                                ptObj.P50RecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableTotal;
                                ptObj.P50RecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableTotalUoM;
                                ptObj.PMeanRecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableTotal;
                                ptObj.PMeanRecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableTotalUoM;
                                ptObj.P10RecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableTotal;
                                ptObj.P10RecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableTotalUoM;
                                ptObj.HydrocarbonTypeParID = exportExploreObj.ProsResourcesTarget.FirstOrDefault().HydrocarbonTypeParID;
                                ptObj.GCFSR = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFSR;
                                ptObj.GCFSRUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFSRUoM;
                                ptObj.GCFTM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFTM;
                                ptObj.GCFTMUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFTMUoM;
                                ptObj.GCFReservoir = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFReservoir;
                                ptObj.GCFReservoirUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFReservoirUoM;
                                ptObj.GCFClosure = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFClosure;
                                ptObj.GCFClosureUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFClosureUoM;
                                ptObj.GCFContainment = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFContainment;
                                ptObj.GCFContainmentUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFContainmentUoM;
                                ptObj.GCFPGTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFPGTotal;
                                ptObj.GCFPGTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFPGTotalUoM;
                                ptObj.CreatedDate = DateTime.Now;
                                ptObj.CreatedBy = user;
                                await _servicePTLG.Create(ptObj);
                            }
                            #endregion                     
                            #region Drilling
                            //Insert Log_Driliing
                            LGDrillingDto drObj = new LGDrillingDto();
                            if (exportExploreObj.Drilling.Count() > 0)
                            {
                                drObj.StructureHistoryID = structureHistoryId;
                                drObj.xStructureID = exportExploreObj.Drilling.FirstOrDefault().xStructureID;
                                drObj.xWellID = exportExploreObj.Drilling.FirstOrDefault().xWellID;
                                drObj.RKAPFiscalYear = exportExploreObj.Drilling.FirstOrDefault().RKAPFiscalYear;
                                drObj.PlayOpener = exportExploreObj.Drilling.FirstOrDefault().PlayOpener;
                                drObj.DrillingCompletionPeriod = exportExploreObj.Drilling.FirstOrDefault().DrillingCompletionPeriod;
                                drObj.Location = exportExploreObj.Drilling.FirstOrDefault().Location;
                                drObj.WaterDepthFeet = exportExploreObj.Drilling.FirstOrDefault().WaterDepthFeet;
                                drObj.WaterDepthMeter = exportExploreObj.Drilling.FirstOrDefault().WaterDepthMeter;
                                drObj.TotalDepthFeet = exportExploreObj.Drilling.FirstOrDefault().TotalDepthFeet;
                                drObj.TotalDepthMeter = exportExploreObj.Drilling.FirstOrDefault().TotalDepthMeter;
                                drObj.SurfaceLocationLatitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLatitude;
                                drObj.SurfaceLocationLongitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLongitude;
                                drObj.CommitmentWell = exportExploreObj.Drilling.FirstOrDefault().CommitmentWell;
                                drObj.OperationalContextParId = exportExploreObj.Drilling.FirstOrDefault().OperationalContextParId;
                                drObj.PotentialDelay = exportExploreObj.Drilling.FirstOrDefault().PotentialDelay;
                                drObj.NetRevenueInterest = exportExploreObj.Drilling.FirstOrDefault().NetRevenueInterest;
                                drObj.DrillingCostDHB = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHB;
                                drObj.DrillingCostDHBCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHBCurr;
                                drObj.DrillingCost = exportExploreObj.Drilling.FirstOrDefault().DrillingCost;
                                drObj.DrillingCostCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostCurr;
                                drObj.ExpectedDrillingDate = exportExploreObj.Drilling.FirstOrDefault().ExpectedDrillingDate;
                                drObj.P90ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOil;
                                drObj.P90ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOilUoM;
                                drObj.P50ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOil;
                                drObj.P50ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOilUoM;
                                drObj.P10ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOil;
                                drObj.P10ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOilUoM;
                                drObj.P90ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGas;
                                drObj.P90ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGasUoM;
                                drObj.P50ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGas;
                                drObj.P50ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGasUoM;
                                drObj.P10ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGas;
                                drObj.P10ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGasUoM;
                                drObj.CurrentPG = exportExploreObj.Drilling.FirstOrDefault().CurrentPG;
                                drObj.ExpectedPG = exportExploreObj.Drilling.FirstOrDefault().ExpectedPG;
                                drObj.ChanceComponentSource = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentSource;
                                drObj.ChanceComponentTiming = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentTiming;
                                drObj.ChanceComponentReservoir = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentReservoir;
                                drObj.ChanceComponentClosure = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentClosure;
                                drObj.ChanceComponentContainment = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentContainment;
                                drObj.P90NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOil;
                                drObj.P90NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOilCurr;
                                drObj.P50NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOil;
                                drObj.P50NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOilCurr;
                                drObj.P10NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOil;
                                drObj.P10NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOilCurr;
                                drObj.P90NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGas;
                                drObj.P90NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGasCurr;
                                drObj.P50NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGas;
                                drObj.P50NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGasCurr;
                                drObj.P10NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGas;
                                drObj.P10NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGasCurr;
                                drObj.CreatedDate = DateTime.Now;
                                drObj.CreatedBy = user;
                                await _serviceDRLG.Create(drObj);
                            }
                            #endregion
                            #region Economic
                            //Insert Log_Economic
                            LGEconomicDto ecObj = new LGEconomicDto();
                            if (exportExploreObj.Economic.Count() > 0)
                            {
                                ecObj.StructureHistoryID = structureHistoryId;
                                ecObj.xStructureID = structureID;
                                ecObj.DevConcept = exportExploreObj.Economic.FirstOrDefault().DevConcept;
                                ecObj.EconomicAssumption = exportExploreObj.Economic.FirstOrDefault().EconomicAssumption;
                                ecObj.CAPEX = exportExploreObj.Economic.FirstOrDefault().CAPEX;
                                ecObj.CAPEXCurr = exportExploreObj.Economic.FirstOrDefault().CAPEXCurr;
                                ecObj.OPEXProduction = exportExploreObj.Economic.FirstOrDefault().OPEXProduction;
                                ecObj.OPEXProductionCurr = exportExploreObj.Economic.FirstOrDefault().OPEXProductionCurr;
                                ecObj.OPEXFacility = exportExploreObj.Economic.FirstOrDefault().OPEXFacility;
                                ecObj.OPEXFacilityCurr = exportExploreObj.Economic.FirstOrDefault().OPEXFacilityCurr;
                                ecObj.ASR = exportExploreObj.Economic.FirstOrDefault().ASR;
                                ecObj.ASRCurr = exportExploreObj.Economic.FirstOrDefault().ASRCurr;
                                ecObj.EconomicResult = exportExploreObj.Economic.FirstOrDefault().EconomicResult;
                                ecObj.ContractorNPV = exportExploreObj.Economic.FirstOrDefault().ContractorNPV;
                                ecObj.ContractorNPVCurr = exportExploreObj.Economic.FirstOrDefault().ContractorNPVCurr;
                                ecObj.IRR = exportExploreObj.Economic.FirstOrDefault().IRR;
                                ecObj.ContractorPOT = exportExploreObj.Economic.FirstOrDefault().ContractorPOT;
                                ecObj.ContractorPOTUoM = exportExploreObj.Economic.FirstOrDefault().ContractorPOTUoM;
                                ecObj.PIncome = exportExploreObj.Economic.FirstOrDefault().PIncome;
                                ecObj.PIncomeCurr = exportExploreObj.Economic.FirstOrDefault().PIncomeCurr;
                                ecObj.EMV = exportExploreObj.Economic.FirstOrDefault().EMV;
                                ecObj.EMVCurr = exportExploreObj.Economic.FirstOrDefault().EMVCurr;
                                ecObj.NPV = exportExploreObj.Economic.FirstOrDefault().NPV;
                                ecObj.NPVCurr = exportExploreObj.Economic.FirstOrDefault().NPVCurr;
                                ecObj.CreatedDate = DateTime.Now;
                                ecObj.CreatedBy = user;
                                await _serviceECLG.Create(ecObj);
                            }
                            #endregion
                        }
                        else
                        {
                            #region ExplorationStructure
                            //Insert Log_ExplorationStructure
                            LGExplorationStructureDto esObj = new LGExplorationStructureDto();
                            esObj.StructureHistoryID = structureHistoryId;
                            esObj.xStructureID = structureID;
                            esObj.xStructureName = exploreStructureObj.xStructureName;
                            esObj.xStructureStatusParID = exploreStructureObj.xStructureStatusParID;
                            esObj.SingleOrMultiParID = exploreStructureObj.SingleOrMultiParID;
                            esObj.ExplorationTypeParID = exploreStructureObj.ExplorationTypeParID;
                            esObj.SubholdingID = exploreStructureObj.SubholdingID;
                            esObj.BasinID = exploreStructureObj.BasinID;
                            esObj.RegionalID = exploreStructureObj.RegionalID;
                            esObj.ZonaID = exploreStructureObj.ZonaID;
                            esObj.APHID = exploreStructureObj.APHID;
                            esObj.xAssetID = exploreStructureObj.xAssetID;
                            esObj.xBlockID = exploreStructureObj.xBlockID;
                            esObj.xAreaID = exploreStructureObj.xAreaID;
                            esObj.UDClassificationParID = exploreStructureObj.UDClassificationParID;
                            esObj.UDSubClassificationParID = exploreStructureObj.UDSubClassificationParID;
                            esObj.ExplorationAreaParID = exploreStructureObj.ExplorationAreaParID;
                            esObj.CountriesID = exploreStructureObj.CountriesID;
                            esObj.Play = exploreStructureObj.Play;
                            esObj.StatusData = "Release";
                            esObj.IsDeleted = false;
                            esObj.MadamTransID = "";
                            esObj.CreatedDate = DateTime.Now;
                            esObj.CreatedBy = user;
                            await _explorationStructureServiceLG.Create(esObj);
                            #endregion
                            #region Drilling
                            //Insert Log_Driliing
                            LGDrillingDto drObj = new LGDrillingDto();
                            if (exportExploreObj.Drilling.Count() > 0)
                            {
                                drObj.StructureHistoryID = structureHistoryId;
                                drObj.xStructureID = exportExploreObj.Drilling.FirstOrDefault().xStructureID;
                                drObj.xWellID = exportExploreObj.Drilling.FirstOrDefault().xWellID;
                                drObj.RKAPFiscalYear = exportExploreObj.Drilling.FirstOrDefault().RKAPFiscalYear;
                                drObj.PlayOpener = exportExploreObj.Drilling.FirstOrDefault().PlayOpener;
                                drObj.DrillingCompletionPeriod = exportExploreObj.Drilling.FirstOrDefault().DrillingCompletionPeriod;
                                drObj.Location = exportExploreObj.Drilling.FirstOrDefault().Location;
                                drObj.WaterDepthFeet = exportExploreObj.Drilling.FirstOrDefault().WaterDepthFeet;
                                drObj.WaterDepthMeter = exportExploreObj.Drilling.FirstOrDefault().WaterDepthMeter;
                                drObj.TotalDepthFeet = exportExploreObj.Drilling.FirstOrDefault().TotalDepthFeet;
                                drObj.TotalDepthMeter = exportExploreObj.Drilling.FirstOrDefault().TotalDepthMeter;
                                drObj.SurfaceLocationLatitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLatitude;
                                drObj.SurfaceLocationLongitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLongitude;
                                drObj.CommitmentWell = exportExploreObj.Drilling.FirstOrDefault().CommitmentWell;
                                drObj.OperationalContextParId = exportExploreObj.Drilling.FirstOrDefault().OperationalContextParId;
                                drObj.PotentialDelay = exportExploreObj.Drilling.FirstOrDefault().PotentialDelay;
                                drObj.NetRevenueInterest = exportExploreObj.Drilling.FirstOrDefault().NetRevenueInterest;
                                drObj.DrillingCostDHB = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHB;
                                drObj.DrillingCostDHBCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHBCurr;
                                drObj.DrillingCost = exportExploreObj.Drilling.FirstOrDefault().DrillingCost;
                                drObj.DrillingCostCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostCurr;
                                drObj.ExpectedDrillingDate = exportExploreObj.Drilling.FirstOrDefault().ExpectedDrillingDate;
                                drObj.P90ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOil;
                                drObj.P90ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOilUoM;
                                drObj.P50ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOil;
                                drObj.P50ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOilUoM;
                                drObj.P10ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOil;
                                drObj.P10ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOilUoM;
                                drObj.P90ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGas;
                                drObj.P90ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGasUoM;
                                drObj.P50ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGas;
                                drObj.P50ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGasUoM;
                                drObj.P10ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGas;
                                drObj.P10ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGasUoM;
                                drObj.CurrentPG = exportExploreObj.Drilling.FirstOrDefault().CurrentPG;
                                drObj.ExpectedPG = exportExploreObj.Drilling.FirstOrDefault().ExpectedPG;
                                drObj.ChanceComponentSource = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentSource;
                                drObj.ChanceComponentTiming = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentTiming;
                                drObj.ChanceComponentReservoir = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentReservoir;
                                drObj.ChanceComponentClosure = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentClosure;
                                drObj.ChanceComponentContainment = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentContainment;
                                drObj.P90NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOil;
                                drObj.P90NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOilCurr;
                                drObj.P50NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOil;
                                drObj.P50NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOilCurr;
                                drObj.P10NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOil;
                                drObj.P10NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOilCurr;
                                drObj.P90NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGas;
                                drObj.P90NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGasCurr;
                                drObj.P50NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGas;
                                drObj.P50NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGasCurr;
                                drObj.P10NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGas;
                                drObj.P10NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGasCurr;
                                drObj.CreatedDate = DateTime.Now;
                                drObj.CreatedBy = user;
                                await _serviceDRLG.Create(drObj);
                            }
                            #endregion
                            #region Economic
                            //Insert Log_Economic
                            LGEconomicDto ecObj = new LGEconomicDto();
                            if (exportExploreObj.Economic.Count() > 0)
                            {
                                ecObj.StructureHistoryID = structureHistoryId;
                                ecObj.xStructureID = structureID;
                                ecObj.DevConcept = exportExploreObj.Economic.FirstOrDefault().DevConcept;
                                ecObj.EconomicAssumption = exportExploreObj.Economic.FirstOrDefault().EconomicAssumption;
                                ecObj.CAPEX = exportExploreObj.Economic.FirstOrDefault().CAPEX;
                                ecObj.CAPEXCurr = exportExploreObj.Economic.FirstOrDefault().CAPEXCurr;
                                ecObj.OPEXProduction = exportExploreObj.Economic.FirstOrDefault().OPEXProduction;
                                ecObj.OPEXProductionCurr = exportExploreObj.Economic.FirstOrDefault().OPEXProductionCurr;
                                ecObj.OPEXFacility = exportExploreObj.Economic.FirstOrDefault().OPEXFacility;
                                ecObj.OPEXFacilityCurr = exportExploreObj.Economic.FirstOrDefault().OPEXFacilityCurr;
                                ecObj.ASR = exportExploreObj.Economic.FirstOrDefault().ASR;
                                ecObj.ASRCurr = exportExploreObj.Economic.FirstOrDefault().ASRCurr;
                                ecObj.EconomicResult = exportExploreObj.Economic.FirstOrDefault().EconomicResult;
                                ecObj.ContractorNPV = exportExploreObj.Economic.FirstOrDefault().ContractorNPV;
                                ecObj.ContractorNPVCurr = exportExploreObj.Economic.FirstOrDefault().ContractorNPVCurr;
                                ecObj.IRR = exportExploreObj.Economic.FirstOrDefault().IRR;
                                ecObj.ContractorPOT = exportExploreObj.Economic.FirstOrDefault().ContractorPOT;
                                ecObj.ContractorPOTUoM = exportExploreObj.Economic.FirstOrDefault().ContractorPOTUoM;
                                ecObj.PIncome = exportExploreObj.Economic.FirstOrDefault().PIncome;
                                ecObj.PIncomeCurr = exportExploreObj.Economic.FirstOrDefault().PIncomeCurr;
                                ecObj.EMV = exportExploreObj.Economic.FirstOrDefault().EMV;
                                ecObj.EMVCurr = exportExploreObj.Economic.FirstOrDefault().EMVCurr;
                                ecObj.NPV = exportExploreObj.Economic.FirstOrDefault().NPV;
                                ecObj.NPVCurr = exportExploreObj.Economic.FirstOrDefault().NPVCurr;
                                ecObj.CreatedDate = DateTime.Now;
                                ecObj.CreatedBy = user;
                                await _serviceECLG.Create(ecObj);
                            }
                            #endregion
                            #region ContingenResources
                            //Insert Log_ContResources
                            LGContingenResourcesDto crObj = new LGContingenResourcesDto();
                            crObj.StructureHistoryID = structureHistoryId;
                            crObj.xStructureID = contResources.xStructureID;
                            crObj.C1COil = contResources.C1COil;
                            crObj.C1COilUoM = contResources.C1COilUoM;
                            crObj.C2COil = contResources.C2COil;
                            crObj.C2COilUoM = contResources.C2COilUoM;
                            crObj.C3COil = contResources.C3COil;
                            crObj.C3COilUoM = contResources.C3COilUoM;
                            crObj.C1CGas = contResources.C1CGas;
                            crObj.C1CGasUoM = contResources.C1CGasUoM;
                            crObj.C2CGas = contResources.C2CGas;
                            crObj.C2CGasUoM = contResources.C2CGasUoM;
                            crObj.C3CGas = contResources.C3CGas;
                            crObj.C3CGasUoM = contResources.C3CGasUoM;
                            crObj.C1CTotal = contResources.C1CTotal;
                            crObj.C1CTotalUoM = contResources.C1CTotalUoM;
                            crObj.C2CTotal = contResources.C2CTotal;
                            crObj.C2CTotalUoM = contResources.C2CTotalUoM;
                            crObj.C3CTotal = contResources.C3CTotal;
                            crObj.C3CTotalUoM = contResources.C3CTotalUoM;
                            crObj.CreatedDate = DateTime.Now;
                            crObj.CreatedBy = user;
                            await _serviceCRLG.Create(crObj);
                            #endregion
                        }
                    }
                    else
                    {
                        int increment = getKey.Count();
                        var structureHistoryId = structureID + "_Log" + increment;
                        if (exploreStructureObj.xStructureStatusParID != "4")
                        {
                            #region ExplorationStructure
                            //Insert Log_ExplorationStructure
                            LGExplorationStructureDto esObj = new LGExplorationStructureDto();
                            esObj.StructureHistoryID = structureHistoryId;
                            esObj.xStructureID = structureID;
                            esObj.xStructureName = exploreStructureObj.xStructureName;
                            esObj.xStructureStatusParID = exploreStructureObj.xStructureStatusParID;
                            esObj.SingleOrMultiParID = exploreStructureObj.SingleOrMultiParID;
                            esObj.ExplorationTypeParID = exploreStructureObj.ExplorationTypeParID;
                            esObj.SubholdingID = exploreStructureObj.SubholdingID;
                            esObj.BasinID = exploreStructureObj.BasinID;
                            esObj.RegionalID = exploreStructureObj.RegionalID;
                            esObj.ZonaID = exploreStructureObj.ZonaID;
                            esObj.APHID = exploreStructureObj.APHID;
                            esObj.xAssetID = exploreStructureObj.xAssetID;
                            esObj.xBlockID = exploreStructureObj.xBlockID;
                            esObj.xAreaID = exploreStructureObj.xAreaID;
                            esObj.UDClassificationParID = exploreStructureObj.UDClassificationParID;
                            esObj.UDSubClassificationParID = exploreStructureObj.UDSubClassificationParID;
                            esObj.ExplorationAreaParID = exploreStructureObj.ExplorationAreaParID;
                            esObj.CountriesID = exploreStructureObj.CountriesID;
                            esObj.Play = exploreStructureObj.Play;
                            esObj.StatusData = "Release";
                            esObj.IsDeleted = false;
                            esObj.MadamTransID = "";
                            esObj.CreatedDate = DateTime.Now;
                            esObj.CreatedBy = user;
                            await _explorationStructureServiceLG.Create(esObj);
                            #endregion
                            #region ProsResources
                            //Insert Log_ProsResources
                            LGProsResourceDto prObj = new LGProsResourceDto();
                            if (exportExploreObj.ProsResources.Count() > 0)
                            {
                                prObj.StructureHistoryID = structureHistoryId;
                                prObj.xStructureID = exportExploreObj.ProsResources.FirstOrDefault().xStructureID;
                                prObj.P90InPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceOilPR;
                                prObj.P90InPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceOilPRUoM;
                                prObj.P50InPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceOilPR;
                                prObj.P50InPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceOilPRUoM;
                                prObj.PMeanInPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceOilPR;
                                prObj.PMeanInPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceOilPRUoM;
                                prObj.P10InPlaceOilPR = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceOilPR;
                                prObj.P10InPlaceOilPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceOilPRUoM;
                                prObj.P90InPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceGasPR;
                                prObj.P90InPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceGasPRUoM;
                                prObj.P50InPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceGasPR;
                                prObj.P50InPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceGasPRUoM;
                                prObj.PMeanInPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceGasPR;
                                prObj.PMeanInPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceGasPRUoM;
                                prObj.P10InPlaceGasPR = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceGasPR;
                                prObj.P10InPlaceGasPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceGasPRUoM;
                                prObj.P90InPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceTotalPR;
                                prObj.P90InPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P90InPlaceTotalPRUoM;
                                prObj.P50InPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceTotalPR;
                                prObj.P50InPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P50InPlaceTotalPRUoM;
                                prObj.PMeanInPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceTotalPR;
                                prObj.PMeanInPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanInPlaceTotalPRUoM;
                                prObj.P10InPlaceTotalPR = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceTotalPR;
                                prObj.P10InPlaceTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().P10InPlaceTotalPRUoM;
                                prObj.RFOilPR = exportExploreObj.ProsResources.FirstOrDefault().RFOilPR;
                                prObj.RFGasPR = exportExploreObj.ProsResources.FirstOrDefault().RFGasPR;
                                prObj.P90RROil = exportExploreObj.ProsResources.FirstOrDefault().P90RROil;
                                prObj.P90RROilUoM = exportExploreObj.ProsResources.FirstOrDefault().P90RROilUoM;
                                prObj.P50RROil = exportExploreObj.ProsResources.FirstOrDefault().P50RROil;
                                prObj.P50RROilUoM = exportExploreObj.ProsResources.FirstOrDefault().P50RROilUoM;
                                prObj.PMeanRROil = exportExploreObj.ProsResources.FirstOrDefault().PMeanRROil;
                                prObj.PMeanRROilUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanRROilUoM;
                                prObj.P10RROil = exportExploreObj.ProsResources.FirstOrDefault().P10RROil;
                                prObj.P10RROilUoM = exportExploreObj.ProsResources.FirstOrDefault().P10RROilUoM;
                                prObj.P90RRGas = exportExploreObj.ProsResources.FirstOrDefault().P90RRGas;
                                prObj.P90RRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().P90RRGasUoM;
                                prObj.P50RRGas = exportExploreObj.ProsResources.FirstOrDefault().P50RRGas;
                                prObj.P50RRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().P50RRGasUoM;
                                prObj.PMeanRRGas = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRGas;
                                prObj.PMeanRRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRGasUoM;
                                prObj.P10RRGas = exportExploreObj.ProsResources.FirstOrDefault().P10RRGas;
                                prObj.P10RRGasUoM = exportExploreObj.ProsResources.FirstOrDefault().P10RRGasUoM;
                                prObj.P90RRTotal = exportExploreObj.ProsResources.FirstOrDefault().P90RRTotal;
                                prObj.P90RRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().P90RRTotalUoM;
                                prObj.P50RRTotal = exportExploreObj.ProsResources.FirstOrDefault().P50RRTotal;
                                prObj.P50RRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().P50RRTotalUoM;
                                prObj.PMeanRRTotal = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRTotal;
                                prObj.PMeanRRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().PMeanRRTotalUoM;
                                prObj.P10RRTotal = exportExploreObj.ProsResources.FirstOrDefault().P10RRTotal;
                                prObj.P10RRTotalUoM = exportExploreObj.ProsResources.FirstOrDefault().P10RRTotalUoM;
                                prObj.HydrocarbonTypePRParID = exportExploreObj.ProsResources.FirstOrDefault().HydrocarbonTypePRParID;
                                prObj.GCFSRPR = exportExploreObj.ProsResources.FirstOrDefault().GCFSRPR;
                                prObj.GCFSRPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFSRPRUoM;
                                prObj.GCFTMPR = exportExploreObj.ProsResources.FirstOrDefault().GCFTMPR;
                                prObj.GCFTMPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFTMPRUoM;
                                prObj.GCFReservoirPR = exportExploreObj.ProsResources.FirstOrDefault().GCFReservoirPR;
                                prObj.GCFReservoirPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFReservoirPRUoM;
                                prObj.GCFClosurePR = exportExploreObj.ProsResources.FirstOrDefault().GCFClosurePR;
                                prObj.GCFClosurePRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFClosurePRUoM;
                                prObj.GCFContainmentPR = exportExploreObj.ProsResources.FirstOrDefault().GCFContainmentPR;
                                prObj.GCFContainmentPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFContainmentPRUoM;
                                prObj.GCFPGTotalPR = exportExploreObj.ProsResources.FirstOrDefault().GCFPGTotalPR;
                                prObj.GCFPGTotalPRUoM = exportExploreObj.ProsResources.FirstOrDefault().GCFPGTotalPRUoM;
                                prObj.ExpectedPG = exportExploreObj.ProsResources.FirstOrDefault().ExpectedPG;
                                prObj.CurrentPG = exportExploreObj.ProsResources.FirstOrDefault().CurrentPG;
                                prObj.MethodParID = exportExploreObj.ProsResources.FirstOrDefault().MethodParID;
                                prObj.CreatedDate = DateTime.Now;
                                prObj.CreatedBy = user;
                                await _servicePRLG.Create(prObj);
                            }
                            #endregion
                            #region ProResourcesTarget
                            //Insert Log_ProsResourcesTarget
                            LGProsResourcesTargetDto ptObj = new LGProsResourcesTargetDto();
                            if (exportExploreObj.ProsResourcesTarget.Count() > 0)
                            {
                                ptObj.StructureHistoryID = structureHistoryId;
                                ptObj.TargetID = exportExploreObj.ProsResourcesTarget.FirstOrDefault().TargetID;
                                ptObj.TargetName = "";
                                ptObj.xStructureID = exportExploreObj.ProsResourcesTarget.FirstOrDefault().xStructureID;
                                ptObj.P90InPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceOil;
                                ptObj.P90InPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceOilUoM;
                                ptObj.P50InPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceOil;
                                ptObj.P50InPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceOilUoM;
                                ptObj.PMeanInPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceOil;
                                ptObj.PMeanInPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceOilUoM;
                                ptObj.P10InPlaceOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceOil;
                                ptObj.P10InPlaceOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceOilUoM;
                                ptObj.P90InPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceGas;
                                ptObj.P90InPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceGasUoM;
                                ptObj.P50InPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceGas;
                                ptObj.P50InPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceGasUoM;
                                ptObj.PMeanInPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceGas;
                                ptObj.PMeanInPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceGasUoM;
                                ptObj.P10InPlaceGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceGas;
                                ptObj.P10InPlaceGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceGasUoM;
                                ptObj.P90InPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceTotal;
                                ptObj.P90InPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90InPlaceTotalUoM;
                                ptObj.P50InPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceTotal;
                                ptObj.P50InPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50InPlaceTotalUoM;
                                ptObj.PMeanInPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceTotal;
                                ptObj.PMeanInPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanInPlaceTotalUoM;
                                ptObj.P10InPlaceTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceTotal;
                                ptObj.P10InPlaceTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10InPlaceTotalUoM;
                                ptObj.RFOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().RFOil;
                                ptObj.RFGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().RFGas;
                                ptObj.P90RecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableOil;
                                ptObj.P90RecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableOilUoM;
                                ptObj.P50RecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableOil;
                                ptObj.P50RecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableOilUoM;
                                ptObj.PMeanRecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableOil;
                                ptObj.PMeanRecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableOilUoM;
                                ptObj.P10RecoverableOil = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableOil;
                                ptObj.P10RecoverableOilUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableOilUoM;
                                ptObj.P90RecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableGas;
                                ptObj.P90RecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableGasUoM;
                                ptObj.P50RecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableGas;
                                ptObj.P50RecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableGasUoM;
                                ptObj.PMeanRecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableGas;
                                ptObj.PMeanRecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableGasUoM;
                                ptObj.P10RecoverableGas = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableGas;
                                ptObj.P10RecoverableGasUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableGasUoM;
                                ptObj.P90RecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableTotal;
                                ptObj.P90RecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P90RecoverableTotalUoM;
                                ptObj.P50RecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableTotal;
                                ptObj.P50RecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P50RecoverableTotalUoM;
                                ptObj.PMeanRecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableTotal;
                                ptObj.PMeanRecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().PMeanRecoverableTotalUoM;
                                ptObj.P10RecoverableTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableTotal;
                                ptObj.P10RecoverableTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().P10RecoverableTotalUoM;
                                ptObj.HydrocarbonTypeParID = exportExploreObj.ProsResourcesTarget.FirstOrDefault().HydrocarbonTypeParID;
                                ptObj.GCFSR = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFSR;
                                ptObj.GCFSRUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFSRUoM;
                                ptObj.GCFTM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFTM;
                                ptObj.GCFTMUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFTMUoM;
                                ptObj.GCFReservoir = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFReservoir;
                                ptObj.GCFReservoirUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFReservoirUoM;
                                ptObj.GCFClosure = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFClosure;
                                ptObj.GCFClosureUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFClosureUoM;
                                ptObj.GCFContainment = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFContainment;
                                ptObj.GCFContainmentUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFContainmentUoM;
                                ptObj.GCFPGTotal = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFPGTotal;
                                ptObj.GCFPGTotalUoM = exportExploreObj.ProsResourcesTarget.FirstOrDefault().GCFPGTotalUoM;
                                ptObj.CreatedDate = DateTime.Now;
                                ptObj.CreatedBy = user;
                                await _servicePTLG.Create(ptObj);
                            }
                            #endregion                     
                            #region Drilling
                            //Insert Log_Driliing
                            LGDrillingDto drObj = new LGDrillingDto();
                            if (exportExploreObj.Drilling.Count() > 0)
                            {
                                drObj.StructureHistoryID = structureHistoryId;
                                drObj.xStructureID = exportExploreObj.Drilling.FirstOrDefault().xStructureID;
                                drObj.xWellID = exportExploreObj.Drilling.FirstOrDefault().xWellID;
                                drObj.RKAPFiscalYear = exportExploreObj.Drilling.FirstOrDefault().RKAPFiscalYear;
                                drObj.PlayOpener = exportExploreObj.Drilling.FirstOrDefault().PlayOpener;
                                drObj.DrillingCompletionPeriod = exportExploreObj.Drilling.FirstOrDefault().DrillingCompletionPeriod;
                                drObj.Location = exportExploreObj.Drilling.FirstOrDefault().Location;
                                drObj.WaterDepthFeet = exportExploreObj.Drilling.FirstOrDefault().WaterDepthFeet;
                                drObj.WaterDepthMeter = exportExploreObj.Drilling.FirstOrDefault().WaterDepthMeter;
                                drObj.TotalDepthFeet = exportExploreObj.Drilling.FirstOrDefault().TotalDepthFeet;
                                drObj.TotalDepthMeter = exportExploreObj.Drilling.FirstOrDefault().TotalDepthMeter;
                                drObj.SurfaceLocationLatitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLatitude;
                                drObj.SurfaceLocationLongitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLongitude;
                                drObj.CommitmentWell = exportExploreObj.Drilling.FirstOrDefault().CommitmentWell;
                                drObj.OperationalContextParId = exportExploreObj.Drilling.FirstOrDefault().OperationalContextParId;
                                drObj.PotentialDelay = exportExploreObj.Drilling.FirstOrDefault().PotentialDelay;
                                drObj.NetRevenueInterest = exportExploreObj.Drilling.FirstOrDefault().NetRevenueInterest;
                                drObj.DrillingCostDHB = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHB;
                                drObj.DrillingCostDHBCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHBCurr;
                                drObj.DrillingCost = exportExploreObj.Drilling.FirstOrDefault().DrillingCost;
                                drObj.DrillingCostCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostCurr;
                                drObj.ExpectedDrillingDate = exportExploreObj.Drilling.FirstOrDefault().ExpectedDrillingDate;
                                drObj.P90ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOil;
                                drObj.P90ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOilUoM;
                                drObj.P50ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOil;
                                drObj.P50ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOilUoM;
                                drObj.P10ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOil;
                                drObj.P10ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOilUoM;
                                drObj.P90ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGas;
                                drObj.P90ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGasUoM;
                                drObj.P50ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGas;
                                drObj.P50ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGasUoM;
                                drObj.P10ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGas;
                                drObj.P10ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGasUoM;
                                drObj.CurrentPG = exportExploreObj.Drilling.FirstOrDefault().CurrentPG;
                                drObj.ExpectedPG = exportExploreObj.Drilling.FirstOrDefault().ExpectedPG;
                                drObj.ChanceComponentSource = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentSource;
                                drObj.ChanceComponentTiming = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentTiming;
                                drObj.ChanceComponentReservoir = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentReservoir;
                                drObj.ChanceComponentClosure = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentClosure;
                                drObj.ChanceComponentContainment = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentContainment;
                                drObj.P90NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOil;
                                drObj.P90NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOilCurr;
                                drObj.P50NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOil;
                                drObj.P50NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOilCurr;
                                drObj.P10NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOil;
                                drObj.P10NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOilCurr;
                                drObj.P90NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGas;
                                drObj.P90NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGasCurr;
                                drObj.P50NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGas;
                                drObj.P50NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGasCurr;
                                drObj.P10NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGas;
                                drObj.P10NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGasCurr;
                                drObj.CreatedDate = DateTime.Now;
                                drObj.CreatedBy = user;
                                await _serviceDRLG.Create(drObj);
                            }
                            #endregion
                            #region Economic
                            //Insert Log_Economic
                            LGEconomicDto ecObj = new LGEconomicDto();
                            if (exportExploreObj.Economic.Count() > 0)
                            {
                                ecObj.StructureHistoryID = structureHistoryId;
                                ecObj.xStructureID = structureID;
                                ecObj.DevConcept = exportExploreObj.Economic.FirstOrDefault().DevConcept;
                                ecObj.EconomicAssumption = exportExploreObj.Economic.FirstOrDefault().EconomicAssumption;
                                ecObj.CAPEX = exportExploreObj.Economic.FirstOrDefault().CAPEX;
                                ecObj.CAPEXCurr = exportExploreObj.Economic.FirstOrDefault().CAPEXCurr;
                                ecObj.OPEXProduction = exportExploreObj.Economic.FirstOrDefault().OPEXProduction;
                                ecObj.OPEXProductionCurr = exportExploreObj.Economic.FirstOrDefault().OPEXProductionCurr;
                                ecObj.OPEXFacility = exportExploreObj.Economic.FirstOrDefault().OPEXFacility;
                                ecObj.OPEXFacilityCurr = exportExploreObj.Economic.FirstOrDefault().OPEXFacilityCurr;
                                ecObj.ASR = exportExploreObj.Economic.FirstOrDefault().ASR;
                                ecObj.ASRCurr = exportExploreObj.Economic.FirstOrDefault().ASRCurr;
                                ecObj.EconomicResult = exportExploreObj.Economic.FirstOrDefault().EconomicResult;
                                ecObj.ContractorNPV = exportExploreObj.Economic.FirstOrDefault().ContractorNPV;
                                ecObj.ContractorNPVCurr = exportExploreObj.Economic.FirstOrDefault().ContractorNPVCurr;
                                ecObj.IRR = exportExploreObj.Economic.FirstOrDefault().IRR;
                                ecObj.ContractorPOT = exportExploreObj.Economic.FirstOrDefault().ContractorPOT;
                                ecObj.ContractorPOTUoM = exportExploreObj.Economic.FirstOrDefault().ContractorPOTUoM;
                                ecObj.PIncome = exportExploreObj.Economic.FirstOrDefault().PIncome;
                                ecObj.PIncomeCurr = exportExploreObj.Economic.FirstOrDefault().PIncomeCurr;
                                ecObj.EMV = exportExploreObj.Economic.FirstOrDefault().EMV;
                                ecObj.EMVCurr = exportExploreObj.Economic.FirstOrDefault().EMVCurr;
                                ecObj.NPV = exportExploreObj.Economic.FirstOrDefault().NPV;
                                ecObj.NPVCurr = exportExploreObj.Economic.FirstOrDefault().NPVCurr;
                                ecObj.CreatedDate = DateTime.Now;
                                ecObj.CreatedBy = user;
                                await _serviceECLG.Create(ecObj);
                            }
                            #endregion
                        }
                        else
                        {
                            #region ExplorationStructure
                            //Insert Log_ExplorationStructure
                            LGExplorationStructureDto esObj = new LGExplorationStructureDto();
                            esObj.StructureHistoryID = structureHistoryId;
                            esObj.xStructureID = structureID;
                            esObj.xStructureName = exploreStructureObj.xStructureName;
                            esObj.xStructureStatusParID = exploreStructureObj.xStructureStatusParID;
                            esObj.SingleOrMultiParID = exploreStructureObj.SingleOrMultiParID;
                            esObj.ExplorationTypeParID = exploreStructureObj.ExplorationTypeParID;
                            esObj.SubholdingID = exploreStructureObj.SubholdingID;
                            esObj.BasinID = exploreStructureObj.BasinID;
                            esObj.RegionalID = exploreStructureObj.RegionalID;
                            esObj.ZonaID = exploreStructureObj.ZonaID;
                            esObj.APHID = exploreStructureObj.APHID;
                            esObj.xAssetID = exploreStructureObj.xAssetID;
                            esObj.xBlockID = exploreStructureObj.xBlockID;
                            esObj.xAreaID = exploreStructureObj.xAreaID;
                            esObj.UDClassificationParID = exploreStructureObj.UDClassificationParID;
                            esObj.UDSubClassificationParID = exploreStructureObj.UDSubClassificationParID;
                            esObj.ExplorationAreaParID = exploreStructureObj.ExplorationAreaParID;
                            esObj.CountriesID = exploreStructureObj.CountriesID;
                            esObj.Play = exploreStructureObj.Play;
                            esObj.StatusData = "Release";
                            esObj.IsDeleted = false;
                            esObj.MadamTransID = "";
                            esObj.CreatedDate = DateTime.Now;
                            esObj.CreatedBy = user;
                            await _explorationStructureServiceLG.Create(esObj);
                            #endregion
                            #region Drilling
                            //Insert Log_Driliing
                            LGDrillingDto drObj = new LGDrillingDto();
                            if (exportExploreObj.Drilling.Count() > 0)
                            {
                                drObj.StructureHistoryID = structureHistoryId;
                                drObj.xStructureID = exportExploreObj.Drilling.FirstOrDefault().xStructureID;
                                drObj.xWellID = exportExploreObj.Drilling.FirstOrDefault().xWellID;
                                drObj.RKAPFiscalYear = exportExploreObj.Drilling.FirstOrDefault().RKAPFiscalYear;
                                drObj.PlayOpener = exportExploreObj.Drilling.FirstOrDefault().PlayOpener;
                                drObj.DrillingCompletionPeriod = exportExploreObj.Drilling.FirstOrDefault().DrillingCompletionPeriod;
                                drObj.Location = exportExploreObj.Drilling.FirstOrDefault().Location;
                                drObj.WaterDepthFeet = exportExploreObj.Drilling.FirstOrDefault().WaterDepthFeet;
                                drObj.WaterDepthMeter = exportExploreObj.Drilling.FirstOrDefault().WaterDepthMeter;
                                drObj.TotalDepthFeet = exportExploreObj.Drilling.FirstOrDefault().TotalDepthFeet;
                                drObj.TotalDepthMeter = exportExploreObj.Drilling.FirstOrDefault().TotalDepthMeter;
                                drObj.SurfaceLocationLatitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLatitude;
                                drObj.SurfaceLocationLongitude = exportExploreObj.Drilling.FirstOrDefault().SurfaceLocationLongitude;
                                drObj.CommitmentWell = exportExploreObj.Drilling.FirstOrDefault().CommitmentWell;
                                drObj.OperationalContextParId = exportExploreObj.Drilling.FirstOrDefault().OperationalContextParId;
                                drObj.PotentialDelay = exportExploreObj.Drilling.FirstOrDefault().PotentialDelay;
                                drObj.NetRevenueInterest = exportExploreObj.Drilling.FirstOrDefault().NetRevenueInterest;
                                drObj.DrillingCostDHB = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHB;
                                drObj.DrillingCostDHBCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostDHBCurr;
                                drObj.DrillingCost = exportExploreObj.Drilling.FirstOrDefault().DrillingCost;
                                drObj.DrillingCostCurr = exportExploreObj.Drilling.FirstOrDefault().DrillingCostCurr;
                                drObj.ExpectedDrillingDate = exportExploreObj.Drilling.FirstOrDefault().ExpectedDrillingDate;
                                drObj.P90ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOil;
                                drObj.P90ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceOilUoM;
                                drObj.P50ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOil;
                                drObj.P50ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceOilUoM;
                                drObj.P10ResourceOil = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOil;
                                drObj.P10ResourceOilUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceOilUoM;
                                drObj.P90ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGas;
                                drObj.P90ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P90ResourceGasUoM;
                                drObj.P50ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGas;
                                drObj.P50ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P50ResourceGasUoM;
                                drObj.P10ResourceGas = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGas;
                                drObj.P10ResourceGasUoM = exportExploreObj.Drilling.FirstOrDefault().P10ResourceGasUoM;
                                drObj.CurrentPG = exportExploreObj.Drilling.FirstOrDefault().CurrentPG;
                                drObj.ExpectedPG = exportExploreObj.Drilling.FirstOrDefault().ExpectedPG;
                                drObj.ChanceComponentSource = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentSource;
                                drObj.ChanceComponentTiming = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentTiming;
                                drObj.ChanceComponentReservoir = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentReservoir;
                                drObj.ChanceComponentClosure = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentClosure;
                                drObj.ChanceComponentContainment = exportExploreObj.Drilling.FirstOrDefault().ChanceComponentContainment;
                                drObj.P90NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOil;
                                drObj.P90NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityOilCurr;
                                drObj.P50NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOil;
                                drObj.P50NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityOilCurr;
                                drObj.P10NPVProfitabilityOil = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOil;
                                drObj.P10NPVProfitabilityOilCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityOilCurr;
                                drObj.P90NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGas;
                                drObj.P90NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P90NPVProfitabilityGasCurr;
                                drObj.P50NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGas;
                                drObj.P50NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P50NPVProfitabilityGasCurr;
                                drObj.P10NPVProfitabilityGas = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGas;
                                drObj.P10NPVProfitabilityGasCurr = exportExploreObj.Drilling.FirstOrDefault().P10NPVProfitabilityGasCurr;
                                drObj.CreatedDate = DateTime.Now;
                                drObj.CreatedBy = user;
                                await _serviceDRLG.Create(drObj);
                            }
                            #endregion
                            #region Economic
                            //Insert Log_Economic
                            LGEconomicDto ecObj = new LGEconomicDto();
                            if (exportExploreObj.Economic.Count() > 0)
                            {
                                ecObj.StructureHistoryID = structureHistoryId;
                                ecObj.xStructureID = structureID;
                                ecObj.DevConcept = exportExploreObj.Economic.FirstOrDefault().DevConcept;
                                ecObj.EconomicAssumption = exportExploreObj.Economic.FirstOrDefault().EconomicAssumption;
                                ecObj.CAPEX = exportExploreObj.Economic.FirstOrDefault().CAPEX;
                                ecObj.CAPEXCurr = exportExploreObj.Economic.FirstOrDefault().CAPEXCurr;
                                ecObj.OPEXProduction = exportExploreObj.Economic.FirstOrDefault().OPEXProduction;
                                ecObj.OPEXProductionCurr = exportExploreObj.Economic.FirstOrDefault().OPEXProductionCurr;
                                ecObj.OPEXFacility = exportExploreObj.Economic.FirstOrDefault().OPEXFacility;
                                ecObj.OPEXFacilityCurr = exportExploreObj.Economic.FirstOrDefault().OPEXFacilityCurr;
                                ecObj.ASR = exportExploreObj.Economic.FirstOrDefault().ASR;
                                ecObj.ASRCurr = exportExploreObj.Economic.FirstOrDefault().ASRCurr;
                                ecObj.EconomicResult = exportExploreObj.Economic.FirstOrDefault().EconomicResult;
                                ecObj.ContractorNPV = exportExploreObj.Economic.FirstOrDefault().ContractorNPV;
                                ecObj.ContractorNPVCurr = exportExploreObj.Economic.FirstOrDefault().ContractorNPVCurr;
                                ecObj.IRR = exportExploreObj.Economic.FirstOrDefault().IRR;
                                ecObj.ContractorPOT = exportExploreObj.Economic.FirstOrDefault().ContractorPOT;
                                ecObj.ContractorPOTUoM = exportExploreObj.Economic.FirstOrDefault().ContractorPOTUoM;
                                ecObj.PIncome = exportExploreObj.Economic.FirstOrDefault().PIncome;
                                ecObj.PIncomeCurr = exportExploreObj.Economic.FirstOrDefault().PIncomeCurr;
                                ecObj.EMV = exportExploreObj.Economic.FirstOrDefault().EMV;
                                ecObj.EMVCurr = exportExploreObj.Economic.FirstOrDefault().EMVCurr;
                                ecObj.NPV = exportExploreObj.Economic.FirstOrDefault().NPV;
                                ecObj.NPVCurr = exportExploreObj.Economic.FirstOrDefault().NPVCurr;
                                ecObj.CreatedDate = DateTime.Now;
                                ecObj.CreatedBy = user;
                                await _serviceECLG.Create(ecObj);
                            }
                            #endregion
                            #region ContingenResources
                            //Insert Log_ContResources
                            LGContingenResourcesDto crObj = new LGContingenResourcesDto();
                            crObj.StructureHistoryID = structureHistoryId;
                            crObj.xStructureID = contResources.xStructureID;
                            crObj.C1COil = contResources.C1COil;
                            crObj.C1COilUoM = contResources.C1COilUoM;
                            crObj.C2COil = contResources.C2COil;
                            crObj.C2COilUoM = contResources.C2COilUoM;
                            crObj.C3COil = contResources.C3COil;
                            crObj.C3COilUoM = contResources.C3COilUoM;
                            crObj.C1CGas = contResources.C1CGas;
                            crObj.C1CGasUoM = contResources.C1CGasUoM;
                            crObj.C2CGas = contResources.C2CGas;
                            crObj.C2CGasUoM = contResources.C2CGasUoM;
                            crObj.C3CGas = contResources.C3CGas;
                            crObj.C3CGasUoM = contResources.C3CGasUoM;
                            crObj.C1CTotal = contResources.C1CTotal;
                            crObj.C1CTotalUoM = contResources.C1CTotalUoM;
                            crObj.C2CTotal = contResources.C2CTotal;
                            crObj.C2CTotalUoM = contResources.C2CTotalUoM;
                            crObj.C3CTotal = contResources.C3CTotal;
                            crObj.C3CTotalUoM = contResources.C3CTotalUoM;
                            crObj.CreatedDate = DateTime.Now;
                            crObj.CreatedBy = user;
                            await _serviceCRLG.Create(crObj);
                            #endregion
                        }
                    }
                    //insert log activity
                    LGActivityDto activityObj = new LGActivityDto();
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    activityObj.IP = myIP;
                    activityObj.Menu = "Review";
                    activityObj.Action = "Update";
                    activityObj.TransactionID = structureID;
                    activityObj.Status = explorationStructure.StatusData;
                    activityObj.CreatedDate = DateTime.Now;
                    activityObj.CreatedBy = user;
                    await _serviceAC.Create(activityObj);
                }
                else if (explorationStructure.StatusData.Trim() == "Revision" && statusData.Trim() == "Approved")
                {
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
                                var actionStr = "Approve";
                                var userData = await _userService.GetUserInfo(user);
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER ADMIN")
                                    {
                                        actionStr = "Approve2";
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
                                                         "Web Exploration", "Approve Data", "-");

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
                                        return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                            }
                        }

                        if (!string.IsNullOrEmpty(transNumber))
                        {
                            explorationStructure.MadamTransID = transNumber.Trim();
                            explorationStructure.StatusData = statusData.Trim();
                            explorationStructure.UpdatedDate = DateTime.Now;
                            explorationStructure.UpdatedBy = user;
                        }
                    }
                    //insert log activity
                    LGActivityDto activityObj = new LGActivityDto();
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    activityObj.IP = myIP;
                    activityObj.Menu = "Review";
                    activityObj.Action = "Update";
                    activityObj.TransactionID = structureID;
                    activityObj.Status = explorationStructure.StatusData;
                    activityObj.CreatedDate = DateTime.Now;
                    activityObj.CreatedBy = user;
                    await _serviceAC.Create(activityObj);
                }
                else
                {
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
                                var actionStr = "Reject";
                                var userData = await _userService.GetUserInfo(user);
                                foreach (var item in userData.Roles)
                                {
                                    if (item.Value.Trim() == "SUPER ADMIN")
                                    {
                                        actionStr = "Reject2";
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
                                                         "Web Exploration", "Reject Data", "-");

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
                                        return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                    }
                                }
                                else
                                {
                                    return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                                }
                            }
                            else
                            {
                                return Json(new { success = false, Message = "Workflow Failed", JsonRequestBehavior.AllowGet });
                            }
                        }

                        if (!string.IsNullOrEmpty(transNumber))
                        {
                            explorationStructure.MadamTransID = transNumber.Trim();
                            explorationStructure.StatusData = "Reject Revision";
                            explorationStructure.UpdatedDate = DateTime.Now;
                            explorationStructure.UpdatedBy = user;
                        }
                    }
                    //insert log activity
                    LGActivityDto activityObj = new LGActivityDto();
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    activityObj.IP = myIP;
                    activityObj.Menu = "Review";
                    activityObj.Action = "Update";
                    activityObj.TransactionID = structureID;
                    activityObj.Status = explorationStructure.StatusData;
                    activityObj.CreatedDate = DateTime.Now;
                    activityObj.CreatedBy = user;
                    await _serviceAC.Create(activityObj);
                }
                await _explorationStructureService.Update(explorationStructure);

                return Json(new { success = true, Message = "Workflow Success", JsonRequestBehavior.AllowGet });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override FormDefinition DefineFormView(FormState formState, string paramID)
        {
            var model = Task.Run(() => _explorationStructureService.GetOne(paramID)).Result;
            var formDef = new FormDefinition
            {
                Title = "Pencatatan Sumber Daya",
                State = formState,
                paramID = paramID,
                DataProsResourceTarget = Task.Run(() => _servicePT.GetProsResourceTargetByStructureID(paramID)).Result,
                Data = Task.Run(() => _servicePR.GetOne(paramID)).Result,
                DataCont = Task.Run(() => _serviceCR.GetOne(paramID)).Result,
                DataDrilling = Task.Run(() => _serviceDR.GetDrillingByStructureID(paramID)).Result,
                DataPartner = Task.Run(async () => await _serviceBP.GetLookupListText(model.xBlockID)).Result,
                StatusData = model.StatusData,
                FieldSections = new List<FieldSection>()
                {
                    FieldGeneralInfo(paramID),
                    FieldProsResources(paramID),
                    FieldContResources(),
                    FieldDrilling(),
                    FieldExplorationBlock(paramID),
                    FieldEconomic(paramID),
                    FieldBlockPartners(model.xBlockID),
                }
            };

            foreach(var item in formDef.DataDrilling)
            {
                item.NetRevenueInterest = item.NetRevenueInterest * 100;
                item.ExpectedPG = item.ExpectedPG * 100;
                item.CurrentPG = item.CurrentPG * 100;
                if(item.PlayOpener == true)
                {
                    item.PlayOpenerBit = "Yes";
                }
                else
                {
                    item.PlayOpenerBit = "No";
                }
                if (item.CommitmentWell == true)
                {
                    item.CommitmentWellBit = "Yes";
                }
                else
                {
                    item.CommitmentWellBit = "No";
                }
                if (item.PotentialDelay == true)
                {
                    item.PotentialDelayBit = "Yes";
                }
                else
                {
                    item.PotentialDelayBit = "No";
                }
            }

            return formDef;
        }
        private FieldSection FieldGeneralInfo(string paramID)
        {
            // get data exploration structure
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
                            expSubClassification = "K7A";
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
                            expUDTypeName = item.ParamValue1Text;
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
                if (item.ParamListID.Trim() == explorationStructure.xStructureStatusParID.Trim())
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
                    if (i.ToString().Trim() == explorationStructure.ExplorationTypeParID.Trim())
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
                    if (i.ToString().Trim() == explorationStructure.ExplorationTypeParID.Trim())
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
                if (item.xBlockID.Trim() == explorationStructure.xBlockID.Trim())
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
                if (item.BasinID.Trim() == explorationStructure.BasinID.Trim())
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
                if (item.xAssetID.Trim() == explorationStructure.xAssetID.Trim())
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
                if (item.xAreaID.Trim() == explorationStructure.ExplorationAreaParID.Trim())
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
                if (item.ParamListID.Trim() == explorationStructure.SingleOrMultiParID.Trim())
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
                        Id = nameof(MDExplorationStructureDto.xStructureID),
                        FieldType = FieldType.Hidden,
                        Value = paramID
                    }, //kiri hidden
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Exploration Structure",
                        FieldType = FieldType.Text,
                        Value = explorationStructure.xStructureName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationAreaParID),
                        Label = "Exp.Area",
                        FieldType = FieldType.Text,
                        Value = expAreaName,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID),
                        Label = "Exploration Status",
                        FieldType = FieldType.Text,
                        Value = expStatusName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.ExplorationTypeParID),
                        Label = "Exploration Type",
                        FieldType = FieldType.Text,
                        Value = expStatusTypeName,
                    }, //kanan
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
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubTypeParID),
                        Label = "UD Type",
                        FieldType = FieldType.Text,
                        Value = expUDTypeName,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.CountriesID),
                        Label = "Country",
                        FieldType = FieldType.Text,
                        Value = fullNameCountry,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDClassificationParID),
                        Label = "UD Classification",
                        FieldType = FieldType.Text,
                        Value = expClassification,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.RegionalID),
                        Label = "Region",
                        FieldType = FieldType.Dropdown,
                        Value = explorationStructure.RegionalID,
                        LookUpList = new LookupList
                        {
                            Items = dataEntityREGList
                        },
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.UDSubClassificationParID),
                        Label = "UD Sub Classification",
                        FieldType = FieldType.Text,
                        Value = expSubClassification,
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
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDEntityDto.EffectiveYear),
                        Label = "RKAP Plan",
                        FieldType = FieldType.Text,
                        Value = valueEffective,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xBlockID),
                        Label = "Block",
                        FieldType = FieldType.Text,
                        Value = expBlockName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.SingleOrMultiParID),
                        Label = "Single/Multi",
                        FieldType = FieldType.Text,
                        Value = expSingleMultiName,
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
                        IsDisabled = true
                    }, //kiri
                    //new Field {
                    //    Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                    //    Label = "One Pager Montage",
                    //    FieldType = FieldType.FileUpload,
                    //}, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Text,
                        Value = expAssetName,
                    }, //kiri
                    //new Field {
                    //    Id = nameof(MDExplorationStructureDto.StructureOutline),
                    //    Label = "Structure Outline",
                    //    FieldType = FieldType.FileUpload,
                    //}, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.BasinID),
                        Label = "Basin",
                        FieldType = FieldType.Text,
                        Value = expBasinName,
                    }, //kiri
                }
            };
        }
        private FieldSection FieldProsResources(string paramID)
        {
            var methodName = "";
            var prosRes = Task.Run(() => _servicePR.GetOne(paramID)).Result;
            if (!string.IsNullOrEmpty(prosRes.MethodParID))
            {
                for (int i = 1; i <= 2; i++)
                {
                    LookupItem methodObj = new LookupItem();
                    if (i == 1)
                    {
                        methodObj.Text = "Arithmethic";
                        methodObj.Description = "";
                        methodObj.Value = "arithmethic";
                        if (methodObj.Value == prosRes.MethodParID.Trim())
                        {
                            methodObj.Selected = true;
                            methodName = "Arithmethic";
                        }
                    }
                    else
                    {
                        methodObj.Text = "MZM";
                        methodObj.Description = "";
                        methodObj.Value = "mzm";
                        if (methodObj.Value == prosRes.MethodParID.Trim())
                        {
                            methodObj.Selected = true;
                            methodName = "MZM";
                        }
                    }
                }
            }

            return new FieldSection
            {
                SectionName = "Prospective Resource",
                Fields = {
                     new Field {
                        Id = nameof(TXProsResourceDto.MethodParID),
                        Label = "Prospective Resource Method",
                        FieldType = FieldType.Text,
                        Value = methodName,
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Current PG",
                        FieldType = FieldType.Text,
                        Value =  decimal.Parse(string.Format("{0:0.##}", prosRes.CurrentPG * 100)),
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xStructureName),
                        Label = "Expected PG",
                        FieldType = FieldType.Text,
                        Value = decimal.Parse(string.Format("{0:0.##}", prosRes.ExpectedPG * 100)),
                    }, //kiri
                }
            };
        }
        private FieldSection FieldContResources()
        {
            return new FieldSection
            {
                SectionName = "Contingen Resource",
            };
        }
        private FieldSection FieldDrilling()
        {
            return new FieldSection
            {
                SectionName = "Drilling",
            };
        }
        private FieldSection FieldExplorationBlock(string paramID)
        {
            var blockStatusName = "";
            var operatorshipStatusName = "";
            var explorationStructure = Task.Run(() => _explorationStructureService.GetOne(paramID)).Result;
            var explorationBlock = Task.Run(() => _serviceBL.GetOne(explorationStructure.xBlockID)).Result;
            var dateAward = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            var dateExpired = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            if (explorationBlock.AwardDate != null)
            {
                dateAward = explorationBlock.AwardDate.Value.Date.ToString("dd-MMM-yyyy");
            }
            if (explorationBlock.ExpiredDate != null)
            {
                dateExpired = explorationBlock.ExpiredDate.Value.Date.ToString("dd-MMM-yyyy");
            }

            var dataBS = Task.Run(() => _servicePL.GetLookupListText("BlockStatus")).Result;
            List<LookupItem> blockStatusList = new List<LookupItem>();
            foreach (var item in dataBS)
            {
                LookupItem blockStatus = new LookupItem();
                blockStatus.Text = item.ParamValue1Text;
                blockStatus.Description = "";
                blockStatus.Value = item.ParamListID;
                if (item.ParamListID.Trim() == explorationBlock.xBlockStatusParID.Trim())
                {
                    blockStatus.Selected = true;
                    blockStatusName = item.ParamValue1Text;
                }
                blockStatusList.Add(blockStatus);
            }

            var dataOS = Task.Run(() => _servicePL.GetLookupListText("Operators")).Result;
            List<LookupItem> operatorStatusList = new List<LookupItem>();
            foreach (var item in dataOS)
            {
                LookupItem operatorStatus = new LookupItem();
                operatorStatus.Text = item.ParamValue1Text;
                operatorStatus.Description = "";
                operatorStatus.Value = item.ParamListID;
                if (item.ParamListID.Trim() == explorationBlock.OperatorshipStatusParID.Trim())
                {
                    operatorStatus.Selected = true;
                    operatorshipStatusName = item.ParamValue1Text;
                }
                operatorStatusList.Add(operatorStatus);
            }

            return new FieldSection
            {
                SectionName = "Exploration Block",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationBlockDto.xBlockID),
                        Label = "Block ID",
                        FieldType = FieldType.Text,
                        Value = explorationBlock.xBlockID,
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.xBlockID),
                        Label = "Block Name",
                        FieldType = FieldType.Text,
                        Value = explorationBlock.xBlockName,
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.AwardDate),
                        Label = "Working Area Award Date",
                        FieldType = FieldType.Date,
                        Value = dateAward,
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.ExpiredDate),
                        Label = "Working Area Exp. Date",
                        FieldType = FieldType.Date,
                        Value = dateExpired,
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.xBlockStatusParID),
                        Label = "Block Status",
                        FieldType = FieldType.Text,
                        Value = blockStatusName,
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorshipStatusParID),
                        Label = "Operatorship Status",
                        FieldType = FieldType.Text,
                        Value = operatorshipStatusName,
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorName),
                        Label = "Operator Name",
                        FieldType = FieldType.Text,
                        Value = explorationBlock.OperatorName,
                    },
                }
            };
        }
        private FieldSection FieldEconomic(string paramID)
        {
            var explorationEco = Task.Run(() => _serviceEC.GetOne(paramID)).Result;
            explorationEco.IRR = decimal.Parse(string.Format("{0:0.##}", explorationEco.IRR * 100));
            explorationEco.PIncome = decimal.Parse(string.Format("{0:0.##}", explorationEco.PIncome * 100));
            explorationEco.ContractorPOT = decimal.Parse(string.Format("{0:0.##}", explorationEco.ContractorPOT * 100));
            explorationEco.ContractorNPV = decimal.Parse(string.Format("{0:0.##}", explorationEco.ContractorNPV));
            explorationEco.ASR = decimal.Parse(string.Format("{0:0.##}", explorationEco.ASR));
            explorationEco.EMV = decimal.Parse(string.Format("{0:0.##}", explorationEco.EMV));
            explorationEco.NPV = decimal.Parse(string.Format("{0:0.##}", explorationEco.NPV));
            return new FieldSection
            {
                SectionName = "Economic",
                Fields = {
                    new Field {
                        Id = nameof(TXEconomicDto.xStructureID),
                        Value = paramID,
                        FieldType = FieldType.Hidden
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.DevConcept),
                        Label = "Dev Concept",
                        FieldType = FieldType.Text,
                        Value = explorationEco.DevConcept,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EconomicAssumption),
                        Label = "Economic Assumption",
                        FieldType = FieldType.Text,
                        Value = explorationEco.EconomicAssumption,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.CAPEX),
                        Label = "CAPEX (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.CAPEX,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXProduction),
                        Label = "OPEX Production (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.OPEXProduction,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXFacility),
                        Label = "OPEX Surface Facility (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.OPEXFacility,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ASR),
                        Label = "ASR (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.ASR,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EconomicResult),
                        Label = "Economic Result",
                        FieldType = FieldType.Text,
                        Value = explorationEco.EconomicResult,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorNPV),
                        Label = "Contractor NPV (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.ContractorNPV,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.IRR),
                        Label = "IRR (%)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.IRR,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorPOT),
                        Label = "Disc. Contractor POT (%)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.ContractorPOT,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.PIncome),
                        Label = "PI",
                        FieldType = FieldType.Text,
                        Value = explorationEco.PIncome,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EMV),
                        Label = "EMV (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.EMV,
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.NPV),
                        Label = "NPV Profitability (MMUSD)",
                        FieldType = FieldType.Text,
                        Value = explorationEco.NPV,
                    },
                }
            };
        }
        private FieldSection FieldBlockPartners(string blockID)
        {
            return new FieldSection
            {
                SectionName = "Block Partner",
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

        public async Task<PartialViewResult> GetAdaptiveFilterView(string columnId, string usernameSession)
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
            var selectList = await _explorationStructureService.GetAdaptiveFilterListView(columnId, hrisObj);
            return PartialView("Component/Grid/AdaptiveFilter/CheckboxList", selectList);
        }

        public async Task<PartialViewResult> GetAdaptiveFilterReport(string columnId, string usernameSession)
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
            var selectList = await _explorationStructureService.GetAdaptiveFilterListReport(columnId, hrisObj);
            return PartialView("Component/Grid/AdaptiveFilter/CheckboxList", selectList);
        }
    }
}