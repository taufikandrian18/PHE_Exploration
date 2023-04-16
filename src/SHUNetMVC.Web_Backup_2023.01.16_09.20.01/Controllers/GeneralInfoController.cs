using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private readonly ITXEconomicService _serviceEC;
        private readonly IMDExplorationWellService _serviceWL;
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
            IMDExplorationWellService serviceWL,
            ITXEconomicService serviceEC)
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
            _serviceWL = serviceWL;
            _serviceEC = serviceEC;
        }
        // GET: GeneralInfo
        public ActionResult Index()
        {
            var formDef = DefineForm(FormState.Create);
            return View(formDef);
        }

        public FormDefinition DefineForm(FormState formState)
        {
            var formDef = new FormDefinition
            {
                Title = "Create",
                State = formState,
            };
            return formDef;
        }

        public PartialViewResult Create()
        {
            FormState ts = FormState.Create;
            var define = DefineGeneralInfo(ts);
            return PartialView("Component/Form/Form", define);
        }

        public ActionResult Selection_Orders_Read([DataSourceRequest] DataSourceRequest request, ClientDataModel clientData)
        {
            var task = Task.Run(async () => await _userService.GetCurrentUserInfo());
            var userTmp = task.Result;
            var hrisObj = "";

            //var taskHRisTest = Task.Run(async () => await _serviceHris.GetLookupText("10116103"));
            //var hrisObj = taskHRisTest.Result;

            //bypass
            //var taskHRisTest = Task.Run(async () => await _serviceHris.GetLevelEntities("10116103"));
            //hrisObj = taskHRisTest.Result;

            //real deal
            if (!string.IsNullOrEmpty(userTmp.OrgUnitID))
            {
                var taskHRis = Task.Run(async () => await _serviceHris.GetLookupText(userTmp.OrgUnitID));
                hrisObj = taskHRis.Result;
            }

            var datas = Task.Run(() => _service.GetLookupListText(clientData.Data, hrisObj.Trim())).Result;
            return Json(datas.ToDataSourceResult(request));
            //return Json(datas.Items.ToDataSourceResult(request));
        }

        private List<MultiTabMenu> GetMemberList()
        {
            List<MultiTabMenu> members = new List<MultiTabMenu>
            {
                new MultiTabMenu { Slug = "Create", Name = "General Info", Controller = "GeneralInfo" },
                new MultiTabMenu { Slug = "Create", Name = "Prospective Resources", Controller = "ProsResources" },
                new MultiTabMenu { Slug = "Create", Name = "Contingen Resources", Controller = "ContingenResources" },
                new MultiTabMenu { Slug = "Create", Name = "Drilling", Controller = "Drilling" },
                new MultiTabMenu { Slug = "Create", Name = "Economic", Controller = "Economic" }
            };
            return members;
        }

        protected FormDefinition DefineGeneralInfo(FormState formState)
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

        private FieldSection FieldGeneralInfo()
        {
            //List<Field> lstField = new List<Field>();
            //FieldSection fs = new FieldSection();
            //fs.SectionName = "Add New Draft";
            //Field fieldId = new Field();
            //fieldId.Id = nameof(MDExplorationStructureDto.StructureID);
            //fieldId.FieldType = FieldType.Hidden;
            //lstField.Add(fieldId);
            //Field fieldsn = new Field();
            //fieldsn.Id = nameof(MDExplorationStructureDto.StructureName);
            //fieldsn.Label = "Exploration Structure";
            //fieldsn.FieldType = FieldType.Text;
            //fieldsn.IsRequired = true;
            //lstField.Add(fieldsn);
            //Field fieldsut = new Field();
            //fieldsut.Id = nameof(MDExplorationStructureDto.SubUDStatusParID);
            //fieldsut.Label = "Sub UD Type";
            //fieldsut.FieldType = FieldType.Dropdown;
            //lstField.Add(fieldsn);
            List<LookupItem> rkapPlanList = new List<LookupItem>();
            for(int i = DateTime.Now.Year; i <= 2123; i++ )
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
            for(int i = 1; i <= 2; i++)
            {
                if(i == 1)
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

            var dataEA = Task.Run(() => _servicePL.GetLookupListText("ExplorationArea")).Result;
            List<LookupItem> expAreaList = new List<LookupItem>();
            foreach (var item in dataEA)
            {
                LookupItem expArea = new LookupItem();
                expArea.Text = item.ParamValue1Text;
                expArea.Description = "";
                expArea.Value = item.ParamListID;
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
                        Id = nameof(MDExplorationStructureDto.xStructureStatusParID                  ),
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
                        FieldType = FieldType.Text,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.OnePagerMontage),
                        Label = "One Pager Montage",
                        FieldType = FieldType.FileUpload,
                    }, //kanan
                    new Field {
                        Id = nameof(MDExplorationStructureDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Text,
                        IsRequired = true,
                        IsDisabled = true
                    }, //kiri
                    new Field {
                        Id = nameof(MDExplorationStructureDto.StructureOutline),
                        Label = "Structure Outline",
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
            result.Region = regId;
            result.Zona = zonId;
            var blockDatas = Task.Run(() => _serviceBL.GetOne(blcId.Trim())).Result;
            if(blockDatas != null)
            {
                resultChild.BlockID = blockDatas.xBlockID;
                resultChild.BlockName = blockDatas.xBlockName;
                resultChild.AwardDate = blockDatas.AwardDate.HasValue ? blockDatas.AwardDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd");
                resultChild.ExpiredDate = blockDatas.ExpiredDate.HasValue ? blockDatas.ExpiredDate.Value.ToString("yyyy-MM-dd") : DateTime.Now.Date.ToString("yyyy-MM-dd");
                resultChild.BlockStatusParID = blockDatas.xBlockStatusParID;
                resultChild.OperatorshipStatusParID = blockDatas.OperatorshipStatusParID;
                resultChild.OperatorName = blockDatas.OperatorName;
                resultChild.Country = blockDatas.CountriesID;
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
        public ActionResult UploadFiles()
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

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
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
                return Json("No files selected.");
            }
        }

        [HttpPost]
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
                var userTmp = await _userService.GetCurrentUserInfo();
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
                explorStructureObj.StatusData = "Submitted";
                explorStructureObj.CreatedDate = DateTime.Now.Date;
                explorStructureObj.CreatedBy = userTmp.Name;
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
                            if (pt.HydrocarbonTypeParID.Trim() == "Oil")
                            {
                                pt.GCFClosureUoM = "MBO";
                                pt.GCFContainmentUoM = "MBO";
                                pt.GCFPGTotalUoM = "MBO";
                                pt.GCFReservoirUoM = "MBO";
                                pt.GCFSRUoM = "MBO";
                                pt.GCFTMUoM = "MBO";
                            }
                            else if (pt.HydrocarbonTypeParID.Trim() == "Gas")
                            {
                                pt.GCFClosureUoM = "MMSCF";
                                pt.GCFContainmentUoM = "MMSCF";
                                pt.GCFPGTotalUoM = "MMSCF";
                                pt.GCFReservoirUoM = "MMSCF";
                                pt.GCFSRUoM = "MMSCF";
                                pt.GCFTMUoM = "MMSCF";
                            }
                            else
                            {
                                pt.GCFClosureUoM = "MBOE";
                                pt.GCFContainmentUoM = "MBOE";
                                pt.GCFPGTotalUoM = "MBOE";
                                pt.GCFReservoirUoM = "MBOE";
                                pt.GCFSRUoM = "MBOE";
                                pt.GCFTMUoM = "MBOE";
                            }
                            pt.CreatedDate = DateTime.Now.Date;
                            pt.CreatedBy = userTmp.Name;
                            await _servicePT.Create(pt);

                        }
                    }
                    RootProsResources prosResource = JsonConvert.DeserializeObject<RootProsResources>(prosResources);
                    if (prosResource.rows.Count() > 0)
                    {
                        foreach (var pr in prosResource.rows)
                        {
                            pr.xStructureID = structureID;
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
                            pr.ExpectedPG = decimal.Parse(expectedPG, CultureInfo.InvariantCulture);
                            pr.CurrentPG = decimal.Parse(currentPG, CultureInfo.InvariantCulture);
                            pr.MethodParID = recoverableOption;
                            pr.CreatedDate = DateTime.Now.Date;
                            pr.CreatedBy = userTmp.Name;
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
                            wellObj.xWellName = dr.WellName;
                            wellObj.DrillingLocation = dr.DrillingLocation;
                            wellObj.RigTypeParID = dr.RigTypeParID;
                            wellObj.WellTypeParID = dr.WellTypeParID;
                            wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                            wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                            await _serviceWL.Create(wellObj);
                            TXDrillingDto drlObj = new TXDrillingDto();
                            drlObj.xStructureID = structureID;
                            drlObj.xWellID = wellObj.xWellID;
                            drlObj.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                            if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                            {
                                string[] drilDate = dr.ExpectedDrillingDate.Split('-');
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
                                var drillingDateStr = drilDate[0].Trim() + "/" + month + "/" + drilDate[2].Trim();
                                var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                drlObj.ExpectedDrillingDate = dateDrl.Date.ToString();
                            }
                            else
                            {
                                drlObj.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                            }
                            drlObj.DrillingCostCurr = "MMUSD";
                            drlObj.P90ResourceOilUoM = "MBO";
                            drlObj.P50ResourceOilUoM = "MBO";
                            drlObj.P10ResourceOilUoM = "MBO";
                            drlObj.P90ResourceGasUoM = "MMSCF";
                            drlObj.P50ResourceGasUoM = "MMSCF";
                            drlObj.P10ResourceGasUoM = "MMSCF";
                            drlObj.P90NPVProfitabiltyOilUoM = "MBO";
                            drlObj.P50NPVProfitabiltyOilUoM = "MBO";
                            drlObj.P10NPVProfitabiltyOilUoM = "MBO";
                            drlObj.P90NPVProfitabiltyGasUoM = "MMSCF";
                            drlObj.P50NPVProfitabiltyGasUoM = "MMSCF";
                            drlObj.P10NPVProfitabiltyGasUoM = "MMSCF";
                            drlObj.CreatedDate = DateTime.Now.Date;
                            drlObj.CreatedBy = userTmp.Name;
                            await _serviceDR.Create(drlObj);
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
                            cr.C1OilUoM = "MBO";
                            cr.C2OilUoM = "MBO";
                            cr.C3OilUoM = "MBO";
                            cr.C1GasUoM = "MMSCF";
                            cr.C2GasUoM = "MMSCF";
                            cr.C3GasUoM = "MMSCF";
                            cr.C1TotalUoM = "MBOE";
                            cr.C2TotalUoM = "MBOE";
                            cr.C3TotalUoM = "MBOE";
                            cr.CreatedDate = DateTime.Now.Date;
                            cr.CreatedBy = userTmp.Name;
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
                            wellObj.xWellName = dr.WellName;
                            wellObj.DrillingLocation = dr.DrillingLocation;
                            wellObj.RigTypeParID = dr.RigTypeParID;
                            wellObj.WellTypeParID = dr.WellTypeParID;
                            wellObj.BHLocationLatitude = dr.BHLocationLatitude;
                            wellObj.BHLocationLongitude = dr.BHLocationLongitude;
                            await _serviceWL.Create(wellObj);
                            TXDrillingDto drlObj = new TXDrillingDto();
                            drlObj.xStructureID = structureID;
                            drlObj.xWellID = wellObj.xWellID;
                            drlObj.RKAPFiscalYear = Convert.ToInt32(EffectiveYear);
                            if (!string.IsNullOrEmpty(dr.ExpectedDrillingDate))
                            {
                                string[] drilDate = dr.ExpectedDrillingDate.Split('-');
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
                                var drillingDateStr = drilDate[0].Trim() + "/" + month + "/" + drilDate[2].Trim();
                                var dateDrl = DateTime.ParseExact(drillingDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                drlObj.ExpectedDrillingDate = dateDrl.Date.ToString();
                            }
                            else
                            {
                                drlObj.ExpectedDrillingDate = DateTime.Now.Date.ToString();
                            }
                            drlObj.DrillingCostCurr = "MMUSD";
                            drlObj.P90ResourceOilUoM = "MBO";
                            drlObj.P50ResourceOilUoM = "MBO";
                            drlObj.P10ResourceOilUoM = "MBO";
                            drlObj.P90ResourceGasUoM = "MMSCF";
                            drlObj.P50ResourceGasUoM = "MMSCF";
                            drlObj.P10ResourceGasUoM = "MMSCF";
                            drlObj.P90NPVProfitabiltyOilUoM = "MBO";
                            drlObj.P50NPVProfitabiltyOilUoM = "MBO";
                            drlObj.P10NPVProfitabiltyOilUoM = "MBO";
                            drlObj.P90NPVProfitabiltyGasUoM = "MMSCF";
                            drlObj.P50NPVProfitabiltyGasUoM = "MMSCF";
                            drlObj.P10NPVProfitabiltyGasUoM = "MMSCF";
                            drlObj.CreatedDate = DateTime.Now.Date;
                            drlObj.CreatedBy = userTmp.Name;
                            await _serviceDR.Create(drlObj);
                        }
                    }
                }
                TXEconomicDto ecoObj = new TXEconomicDto();
                ecoObj.xStructureID = structureID;
                ecoObj.DevConcept = economicDevConcept;
                ecoObj.EconomicAssumption = economicEconomicAssumption;
                ecoObj.CAPEX = Convert.ToInt32(economicCAPEX);
                ecoObj.CAPEXCurr = "MMUSD";
                ecoObj.OPEXProduction = Convert.ToInt32(economicOPEXProduction);
                ecoObj.OPEXProductionCurr = "MMUSD";
                ecoObj.OPEXFacility = Convert.ToInt32(economicOPEXFacility);
                ecoObj.OPEXFacilityCurr = "MMUSD";
                ecoObj.ASR = decimal.Parse(economicASR);
                ecoObj.ASRCurr = "MMUSD";
                ecoObj.EconomicResult = economicEconomicResult;
                ecoObj.ContractorNPV = decimal.Parse(economicContractorNPV);
                ecoObj.ContractorNPVCurr = "MMUSD";
                ecoObj.IRR = decimal.Parse(economicIRR);
                ecoObj.ContractorPOT = decimal.Parse(economicContractorPOT);
                ecoObj.ContractorPOTUoM = "%";
                ecoObj.PIncome = decimal.Parse(economicPIncome);
                ecoObj.PIncomeCurr = "MMUSD";
                ecoObj.EMV = decimal.Parse(economicEMV);
                ecoObj.EMVCurr = "MMUSD";
                ecoObj.NPV = decimal.Parse(economicEMV);
                ecoObj.NPVCurr = "MMUSD";
                ecoObj.CreatedDate = DateTime.Now.Date;
                ecoObj.CreatedBy = userTmp.Name;
                await _serviceEC.Create(ecoObj);
                return Json(new { success = true });
            }
            catch(Exception ex)
            {
                return Json(new { success = false });
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
    }
}