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
        public EconomicController(IMDParameterListService servicePL, IMDExplorationBlockPartnerService serviceBP)
        {
            _servicePL = servicePL;
            _serviceBP = serviceBP;
        }
        // GET: Economic
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult Create()
        {
            FormState ts = FormState.Create;
            var define = DefineEconomic(ts);
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
                listObj.AddRange(partnerlist);
            }
            return Json(listObj.ToDataSourceResult(request));
        }
        protected FormDefinition DefineEconomic(FormState formState)
        {
            var formDef = new FormDefinition
            {
                Title = "Economic",
                State = formState,
                FieldSections = new List<FieldSection>()
                {
                    FieldExplorationBlock(),
                    FieldEconomic()
                }
            };
            return formDef;
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
                        FieldType = FieldType.Date,
                        Value = DateTime.Now.Date.ToString("dd-MMM-yyyy"),
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.ExpiredDate),
                        Label = "Working Area Exp. Date",
                        FieldType = FieldType.Date,
                        Value = DateTime.Now.Date.ToString("dd-MMM-yyyy"),
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.xBlockStatusParID),
                        Label = "Block Status",
                        FieldType = FieldType.Dropdown,
                        LookUpList = new LookupList
                        {
                            Items = blockStatusList
                        },
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorshipStatusParID),
                        Label = "Operatorship Status",
                        FieldType = FieldType.Radio,
                        Value = "0",
                        LookUpList = new LookupList
                        {
                            Items = operatorStatusList
                        },
                    },
                    new Field {
                        Id = nameof(MDExplorationBlockDto.OperatorName),
                        Label = "Operator Name",
                        FieldType = FieldType.Text,
                        IsRequired = true
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
                        Label = "CAPEX",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXProduction),
                        Label = "OPEX Production",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.OPEXFacility),
                        Label = "OPEX Surface Facility",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ASR),
                        Label = "ASR",
                        FieldType = FieldType.Text,
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
                        Label = "Contractor NPV",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.IRR),
                        Label = "IRR",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.ContractorPOT),
                        Label = "Disc. Contractor POT",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.PIncome),
                        Label = "PI",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.EMV),
                        Label = "EMV",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(TXEconomicDto.NPV),
                        Label = "NPV Profitability",
                        FieldType = FieldType.Text,
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