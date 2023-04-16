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
    public class DrillingController : Controller
    {
        private readonly ITXDrillingService _serviceDR;
        private readonly IMDParameterListService _servicePL;
        public DrillingController(ITXDrillingService serviceDR, IMDParameterListService servicePL)
        {
            _serviceDR = serviceDR;
            _servicePL = servicePL;
        }

        public ActionResult EditingCustomDR()
        {
            PopulateRigCategories();
            PopulateWellCategories();
            PopulateLocationCategories();
            PopulateDrillingBitCategories();
            PopulateOperationalContextCategories();
            //ViewData["dateCategory"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var define = DefineDrilling(FormState.Create, "");
            //FormState ts = FormState.Create;
            //var define = DefineGeneralInfo(ts);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomEditDR(string structureID)
        {
            PopulateRigCategories();
            PopulateWellCategories();
            PopulateLocationCategories();
            PopulateDrillingBitCategories();
            PopulateOperationalContextCategories();
            //ViewData["dateCategory"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var define = DefineDrilling(FormState.Edit, structureID);
            //FormState ts = FormState.Create;
            //var define = DefineGeneralInfo(ts);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }

        private void PopulateWellCategories()
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("WellType")).Result;
            List<MDEntityWithView> wellTypeList = new List<MDEntityWithView>();
            foreach (var item in datas)
            {
                MDEntityWithView wellType = new MDEntityWithView();
                wellType.EntityName = item.ParamValue1Text;
                wellTypeList.Add(wellType);
            }

            ViewData["categoriesWell"] = wellTypeList.ToList();
            ViewData["wellCategory"] = wellTypeList.First();
        }

        private void PopulateRigCategories()
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("RigType")).Result;
            List<MDEntityWithView> rigTypeList = new List<MDEntityWithView>();
            foreach (var item in datas)
            {
                MDEntityWithView rigType = new MDEntityWithView();
                rigType.EntityName = item.ParamValue1Text;
                rigTypeList.Add(rigType);
            }

            ViewData["categoriesRig"] = rigTypeList.ToList();
            ViewData["rigCategory"] = rigTypeList.First();
        }
        private void PopulateLocationCategories()
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("DrillingLocation")).Result;
            List<MDEntityWithView> locationList = new List<MDEntityWithView>();
            foreach (var item in datas)
            {
                MDEntityWithView locObj = new MDEntityWithView();
                locObj.EntityName = item.ParamValue1Text;
                locationList.Add(locObj);
            }

            ViewData["categoriesLocation"] = locationList.ToList();
            ViewData["locationCategory"] = locationList.First();
        }
        private void PopulateDrillingBitCategories()
        {
            List<MDEntityWithView> bitList = new List<MDEntityWithView>();

            MDEntityWithView bitObjYes = new MDEntityWithView();
            bitObjYes.EntityName = "Yes";
            bitList.Add(bitObjYes);

            MDEntityWithView bitObjNo = new MDEntityWithView();
            bitObjNo.EntityName = "No";
            bitList.Add(bitObjNo);

            ViewData["categoriesBit"] = bitList.ToList();
            ViewData["bitCategory"] = bitList.First();
        }
        private void PopulateOperationalContextCategories()
        {
            var datas = Task.Run(() => _servicePL.GetLookupListText("OperationalContext")).Result;
            List<MDEntityWithView> ocList = new List<MDEntityWithView>();
            foreach (var item in datas)
            {
                MDEntityWithView ocObj = new MDEntityWithView();
                ocObj.EntityName = item.ParamValue1Text;
                ocList.Add(ocObj);
            }

            ViewData["categoriesOC"] = ocList.ToList();
            ViewData["ocCategory"] = ocList.First();
        }

        protected FormDefinition DefineDrilling(FormState formState, string structureID)
        {
            if(formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "Drilling",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldDrilling()
                    }
                };
                return formDef;
            }
            else
            {
                var formDef = new FormDefinition
                {
                    Title = "Drilling",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldDrilling()
                    }
                };
                return formDef;
            }
        }
        private FieldSection FieldDrilling()
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
                        Label = "UD STatus",
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

        public ActionResult EditingCustomDR_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXDrillingDto> txDrillingObj = new List<TXDrillingDto>();
            if (clientData.State == "create")
            {
                var model = new CrudPage
                {
                    Id = "Drilling",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "dr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                //var datas = await _servicePR.GetPaged(model.GridParam);

                //var datas = Task.Run(() => _serviceDR.GetPaged(model.GridParam)).Result;
                //return Json(datas.Items.ToDataSourceResult(request));


                return Json(txDrillingObj.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "Drilling",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "dr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                var datas = Task.Run(() => _serviceDR.GetDrillingByStructureID(clientData.Data)).Result;
                foreach (var item in datas)
                {
                    if(item.PlayOpener == true)
                    {
                        item.PlayOpenerBit = "Yes";
                    }
                    else if(item.PlayOpener == false)
                    {
                        item.PlayOpenerBit = "No";
                    }
                    else
                    {
                        item.PlayOpenerBit = "";
                    }
                    if (item.CommitmentWell == true)
                    {
                        item.CommitmentWellBit = "Yes";
                    }
                    else if (item.CommitmentWell == false)
                    {
                        item.CommitmentWellBit = "No";
                    }
                    else
                    {
                        item.CommitmentWellBit = "";
                    }
                    if (item.PotentialDelay == true)
                    {
                        item.PotentialDelayBit = "Yes";
                    }
                    else if (item.PotentialDelay == false)
                    {
                        item.PotentialDelayBit = "No";
                    }
                    else
                    {
                        item.PotentialDelayBit = "";
                    }
                    if(string.IsNullOrEmpty(item.OperationalContextParId))
                    {
                        item.OperationalContextParId = "";
                    }
                    else
                    {
                        item.OperationalContextParId = item.OperationalContextParId;
                    }
                    item.ExpectedDrillingDate = formatDate(item.ExpectedDrillingDate);
                    item.ExpectedPG = item.ExpectedPG * 100;
                    item.CurrentPG = item.CurrentPG * 100;
                    item.NetRevenueInterest = item.NetRevenueInterest * 100;
                    item.ChanceComponentClosure = item.ChanceComponentClosure * 100;
                    item.ChanceComponentContainment = item.ChanceComponentContainment * 100;
                    item.ChanceComponentReservoir = item.ChanceComponentReservoir * 100;
                    item.ChanceComponentSource = item.ChanceComponentSource * 100;
                    item.ChanceComponentTiming = item.ChanceComponentTiming * 100;
                }
                return Json(datas.ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> EditingCustomDRView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXDrillingDto> txDrillingObj = new List<TXDrillingDto>();
            if (clientData.State == "create")
            {
                var model = new CrudPage
                {
                    Id = "Drilling",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "dr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                //var datas = await _servicePR.GetPaged(model.GridParam);

                //var datas = Task.Run(() => _serviceDR.GetPaged(model.GridParam)).Result;
                //return Json(datas.Items.ToDataSourceResult(request));


                return Json(txDrillingObj.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "Drilling",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "dr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                var datas = await _serviceDR.GetDrillingByStructureID(clientData.Data);
                if(datas == null || datas.Count() ==0)
                {
                    return Json(txDrillingObj.ToDataSourceResult(request));
                }
                else
                {
                    foreach (var item in datas)
                    {
                        item.ExpectedDrillingDate = formatDate(item.ExpectedDrillingDate);
                        item.ExpectedPG = item.ExpectedPG * 100;
                        item.CurrentPG = item.CurrentPG * 100;
                        item.NetRevenueInterest = item.NetRevenueInterest * 100;
                        item.ChanceComponentClosure = item.ChanceComponentClosure * 100;
                        item.ChanceComponentContainment = item.ChanceComponentContainment * 100;
                        item.ChanceComponentReservoir = item.ChanceComponentReservoir * 100;
                        item.ChanceComponentSource = item.ChanceComponentSource * 100;
                        item.ChanceComponentTiming = item.ChanceComponentTiming * 100;
                    }
                    return Json(datas.ToDataSourceResult(request));
                }
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomDR_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXDrillingDto> drilling)
        {
            if (drilling != null && ModelState.IsValid)
            {
                foreach (var product in drilling)
                {
                    _serviceDR.Update(product);
                }
            }

            return Json(drilling.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomDR_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXDrillingDto> drilling)
        {
            var results = new List<TXDrillingDto>();

            if (drilling != null && ModelState.IsValid)
            {
                foreach (var item in drilling)
                {
                    _serviceDR.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomDR_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXDrillingDto> drilling)
        {
            foreach (var item in drilling)
            {
                _serviceDR.Destroy(item.xStructureID, item.xWellID);
            }

            return Json(drilling.ToDataSourceResult(request, ModelState));
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
        public string formatDate(string dt)
        {
            var date = DateTime.Parse(dt);
            var day = date.Day;
            var month = date.Month;
            var year = date.Year;
            string[] months = { "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agu", "Sep", "Okt", "Nov", "Dec" };

            return day + "-" + months[month - 1] + "-" + year;
        }
    }
}