using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class EconomicController : Controller
    {
        private readonly IMDParameterListService _servicePL;
        private readonly IMDExplorationBlockPartnerService _serviceBP;
        private readonly ITXEconomicService _serviceEC;
        private readonly IMDExplorationStructureService _serviceES;
        private readonly IMDExplorationBlockService _serviceBL;
        private readonly IMDExplorationBlockPartnerService _serviceBLP;
        public EconomicController(IMDParameterListService servicePL, 
            IMDExplorationBlockPartnerService serviceBP, 
            ITXEconomicService serviceEC, 
            IMDExplorationStructureService serviceES,
            IMDExplorationBlockService serviceBL,
            IMDExplorationBlockPartnerService serviceBLP)
        {
            _servicePL = servicePL;
            _serviceBP = serviceBP;
            _serviceEC = serviceEC;
            _serviceES = serviceES;
            _serviceBL = serviceBL;
            _serviceBLP = serviceBLP;
        }
        // GET: Economic
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Create()
        {
            FormState ts = FormState.Create;
            var define = DefineEconomic(ts, "");
            return PartialView("Component/Form/FormEconomic", define);
        }
        public PartialViewResult Edit(string structureID)
        {
            FormState ts = FormState.Edit;
            var define = DefineEconomic(ts, structureID);
            return PartialView("Component/Form/FormEconomic", define);
        }
        public ActionResult ParticipantList_Read([DataSourceRequest] DataSourceRequest request, ClientDataModel clientData)
        {
            List<MDExplorationBlockPartnerDto> listObj = new List<MDExplorationBlockPartnerDto>();
            if (string.IsNullOrEmpty(clientData.Data))
            {
                for (int i = 1; i <= 5; i++)
                {
                    MDExplorationBlockPartnerDto partnerObj = new MDExplorationBlockPartnerDto();
                    if (i == 1)
                    {
                        partnerObj.PartnerName = "Partner " + i;
                        partnerObj.PI = 0;
                    }
                    if (i == 2)
                    {
                        partnerObj.PartnerName = "Partner " + i;
                        partnerObj.PI = 0;
                    }
                    if (i == 3)
                    {
                        partnerObj.PartnerName = "Partner " + i;
                        partnerObj.PI = 0;
                    }
                    if (i == 4)
                    {
                        partnerObj.PartnerName = "Partner " + i;
                        partnerObj.PI = 0;
                    }
                    if (i == 5)
                    {
                        partnerObj.PartnerName = "Partner " + i;
                        partnerObj.PI = 0;
                    }
                    listObj.Add(partnerObj);
                }
            }
            else
            {
                var task = Task.Run(async () => await _serviceBP.GetLookupListText(clientData.Data));
                var partnerlist = task.Result;
                foreach(var item in partnerlist)
                {
                    item.PI = item.PI * 100;
                }
                listObj.AddRange(partnerlist);
            }
            return Json(listObj.ToDataSourceResult(request));
        }
        protected FormDefinition DefineEconomic(FormState formState, string structureID)
        {
            if(formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "Economic",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                {
                    FieldExplorationBlock(),
                    FieldEconomic()
                }
                };
                return formDef;
            }
            else
            {
                var formDef = new FormDefinition
                {
                    Title = "Economic",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                {
                    FieldExplorationBlock(structureID),
                    FieldEconomic(structureID)
                }
                };
                return formDef;
            }

        }
        
        private FieldSection FieldExplorationBlock()
        {
            var dataBS = Task.Run(() => _servicePL.GetLookupListText("BlockStatus")).Result;
            List<LookupItem> blockStatusList = new List<LookupItem>();
            foreach (var item in dataBS)
            {
                LookupItem blockStatus = new LookupItem();
                blockStatus.Text = item.ParamValue1Text;
                blockStatus.Description = "";
                blockStatus.Value = item.ParamListID;
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
                operatorStatusList.Add(operatorStatus);
            }

            return new FieldSection
            {
                SectionName = "Exploration Block",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationBlockDto.AwardDate),
                        Label = "Working Area Award Date",
                        FieldType = FieldType.Text,
                        Value = DateTime.Now.Date.ToString("dd-MMM-yyyy"),
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.ExpiredDate),
                        Label = "Working Area Exp. Date",
                        FieldType = FieldType.Text,
                        Value = DateTime.Now.Date.ToString("dd-MMM-yyyy"),
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.xBlockStatusParID),
                        Label = "Block Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = blockStatusList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorshipStatusParID),
                        Label = "Operatorship Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = operatorStatusList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorName),
                        Label = "Operator Name",
                        FieldType = FieldType.Text,
                        IsRequired = true,
                        IsDisabled = true
                    },
                }
            };
        }
        private FieldSection FieldEconomic()
        {
            return new FieldSection
            {
                SectionName = "Economic",
                Fields = {
                    new Field {
                        Id = nameof(TXEconomicDto.xStructureID),
                        FieldType = FieldType.Hidden
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.DevConcept),
                        Label = "Dev Concept",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EconomicAssumption),
                        Label = "Economic Assumption",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.CAPEX),
                        Label = "CAPEX (MMUSD)",
                        FieldType = FieldType.Number,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXProduction),
                        Label = "OPEX Production (MMUSD)",
                        FieldType = FieldType.Number,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXFacility),
                        Label = "OPEX Surface Facility (MMUSD)",
                        FieldType = FieldType.Number,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ASR),
                        Label = "ASR (MMUSD)",
                        FieldType = FieldType.Decimal,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EconomicResult),
                        Label = "Economic Result",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorNPV),
                        Label = "Contractor NPV (MMUSD)",
                        FieldType = FieldType.Decimal,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.IRR),
                        Label = "IRR (%)",
                        FieldType = FieldType.Percentage,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorPOT),
                        Label = "Disc. Contractor POT (Years)",
                        FieldType = FieldType.Decimal,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.PIncome),
                        Label = "PI",
                        FieldType = FieldType.Decimal,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EMV),
                        Label = "EMV (MMUSD)",
                        FieldType = FieldType.Decimal,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.NPV),
                        Label = "NPV Profitability (MMUSD)",
                        FieldType = FieldType.Decimal,
                        IsRequired = true
                    },
                }
            };
        }

        [HttpGet]
        private FieldSection FieldExplorationBlock(string paramID)
        {
            var explorationStructure = Task.Run(() => _serviceES.GetOne(paramID)).Result;
            var explorationBlock = Task.Run(() => _serviceBL.GetOne(explorationStructure.xBlockID)).Result;
            //var explorationBlockPartner = Task.Run(() => _serviceBLP.GetOne(explorationStructure.xBlockID)).Result;
            var dateAward = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            var dateExpired = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            if (explorationBlock.AwardDate != null)
            {
                dateAward = explorationBlock.AwardDate.Value.Date.ToString("dd-MMM-yyyy");
            }
            if(explorationBlock.ExpiredDate != null)
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
                if(item.ParamListID.Trim() == explorationBlock.xBlockStatusParID.Trim())
                {
                    blockStatus.Selected = true;
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
                if(item.ParamListID.Trim() == explorationBlock.OperatorshipStatusParID.Trim())
                {
                    operatorStatus.Selected = true;
                }
                if (item.ParamListID.Trim() == explorationBlock.OperatorName.Trim())
                {
                    operatorStatus.Selected = true;
                }
                /*if (explorationBlock.OperatorshipStatusParID == "1")
                {
                    explorationBlockPartner.PartnerName.FirstOrDefault();
                }*/
                operatorStatusList.Add(operatorStatus);
            }

            return new FieldSection
            {
                SectionName = "Exploration Block",
                Fields = {
                    new Field {
                        Id = nameof(MDExplorationBlockDto.AwardDate),
                        Label = "Working Area Award Date",
                        FieldType = FieldType.Text,
                        Value = dateAward,
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.ExpiredDate),
                        Label = "Working Area Exp. Date",
                        FieldType = FieldType.Text,
                        Value = dateExpired,
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.xBlockStatusParID),
                        Label = "Block Status",
                        FieldType = FieldType.Dropdown,
                        Value = explorationBlock.xBlockStatusParID,
                        LookUpList = new LookupList
                        {
                            Items = blockStatusList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorshipStatusParID),
                        Label = "Operatorship Status",
                        FieldType = FieldType.Dropdown,
                        Value = explorationBlock.OperatorshipStatusParID,
                        LookUpList = new LookupList
                        {
                            Items = operatorStatusList
                        },
                        IsRequired = true,
                        IsDisabled = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorName),
                        Label = "Operator Name",
                        FieldType = FieldType.Text,
                        Value = explorationBlock.OperatorName,
                        IsRequired = true,
                        IsDisabled = true
                    },
                }
            };
        }
        [HttpGet]
        private FieldSection FieldEconomic(string paramID)
        {
            var explorationEco = Task.Run(() => _serviceEC.GetOne(paramID)).Result;
            explorationEco.IRR = decimal.Parse(string.Format("{0:0.##}", explorationEco.IRR * 100));
            explorationEco.PIncome = decimal.Parse(string.Format("{0:0.##}", explorationEco.PIncome));
            explorationEco.ContractorPOT = decimal.Parse(string.Format("{0:0.##}", explorationEco.ContractorPOT));
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
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EconomicAssumption),
                        Label = "Economic Assumption",
                        FieldType = FieldType.Text,
                        Value = explorationEco.EconomicAssumption,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.CAPEX),
                        Label = "CAPEX (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.CAPEX,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXProduction),
                        Label = "OPEX Production (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.OPEXProduction,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXFacility),
                        Label = "OPEX Surface Facility (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.OPEXFacility,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ASR),
                        Label = "ASR (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.ASR,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EconomicResult),
                        Label = "Economic Result",
                        FieldType = FieldType.Text,
                        Value = explorationEco.EconomicResult,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorNPV),
                        Label = "Contractor NPV (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.ContractorNPV,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.IRR),
                        Label = "IRR (%)",
                        FieldType = FieldType.Percentage,
                        Value = explorationEco.IRR,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorPOT),
                        Label = "Disc. Contractor POT (Years)",
                        FieldType = FieldType.Percentage,
                        Value = explorationEco.ContractorPOT,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.PIncome),
                        Label = "PI",
                        FieldType = FieldType.Percentage,
                        Value = explorationEco.PIncome,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EMV),
                        Label = "EMV (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.EMV,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.NPV),
                        Label = "NPV Profitability (MMUSD)",
                        FieldType = FieldType.Decimal,
                        Value = explorationEco.NPV,
                        IsRequired = true
                    },
                }
            };
        }
        public sealed class ClientDataModel
        {
            public string Data { get; set; }
        }
    }
}