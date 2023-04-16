using ASPNetMVC.Abstraction.Model.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class ESDCForecastController : Controller
    {
        private readonly ITXESDCForecastService _service;
        public ESDCForecastController(ITXESDCForecastService service)
        {
            _service = service;
        }
        public ActionResult EditingCustomESDCForecast()
        {
            //ViewData["dateCategory"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var define = DefineForecast(FormState.Create, "");
            //FormState ts = FormState.Create;
            //var define = DefineGeneralInfo(ts);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomEditESDCForecast(string structureID)
        {
            //ViewData["dateCategory"] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var define = DefineForecast(FormState.Edit, structureID);
            //FormState ts = FormState.Create;
            //var define = DefineGeneralInfo(ts);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }

        protected FormDefinition DefineForecast(FormState formState, string structureID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "Forecast",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldForecast()
                    }
                };
                return formDef;
            }
            else
            {
                var datas = Task.Run(() => _service.GetForecastByStructureID(structureID)).Result;
                var formDef = new FormDefinition
                {
                    Title = "Forecast",
                    State = formState,
                    paramID = structureID,
                    DataESDCFor = datas,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldForecast()
                    }
                };
                return formDef;
            }
        }
        private FieldSection FieldForecast()
        {
            return new FieldSection
            {
                SectionName = "Add New Draft",
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
                        Id = nameof(MDExplorationStructureESDCDto.UDSubClassificationParID),
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
                        Id = nameof(MDExplorationStructureESDCDto.ExplorationTypeParID),
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
                        Id = nameof(MDExplorationStructureESDCDto.UDClassificationParID),
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
                        Id = nameof(MDExplorationStructureESDCDto.BasinID),
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
                        Id = nameof(MDExplorationStructureESDCDto.APHID),
                        Label = "APH",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.RegionalID),
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
                        Id = nameof(MDExplorationStructureESDCDto.xAssetID),
                        Label = "AP / Assets",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.ZonaID),
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
                        Id = nameof(MDExplorationStructureESDCDto.ExplorationAreaParID),
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
                        Id = nameof(MDExplorationStructureESDCDto.xBlockID),
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
                        Id = nameof(MDExplorationStructureESDCDto.Play),
                        Label = "Play",
                        FieldType = FieldType.Text,
                        IsRequired = true
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.CountriesID),
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
                        Id = nameof(MDExplorationStructureESDCDto.SingleOrMultiParID),
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
                        Id = nameof(MDExplorationStructureESDCDto.OnePagerMontage),
                        Label = "One Pager Montage",
                        FieldType = FieldType.FileUpload,
                    },
                    new Field {
                        Id = nameof(MDExplorationStructureESDCDto.StructureOutline),
                        Label = "Structure Outline",
                        FieldType = FieldType.FileUpload,
                    },
                }
            };
        }
        public ActionResult EditingCustomESDCForecast_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCForecastDto> txESDCForecastObj = new List<TXESDCForecastDto>();
            if (clientData.State == "create")
            {
                var model = new CrudPage
                {
                    Id = "ESDCForecast",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC Forecast",
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
                //var datas = await _servicePR.GetPaged(model.GridParam);

                //var datas = Task.Run(() => _serviceDR.GetPaged(model.GridParam)).Result;
                //return Json(datas.Items.ToDataSourceResult(request));


                return Json(txESDCForecastObj.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ESDCForecast",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC Forecast",
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
                var datas = Task.Run(() => _service.GetForecastByStructureID(clientData.Data)).Result;
                foreach (var item in datas)
                {
                    /*item.ExpectedDrillingDate = formatDate(item.ExpectedDrillingDate);
                    item.ExpectedPG = item.ExpectedPG * 100;
                    item.CurrentPG = item.CurrentPG * 100;
                    item.ChanceComponentClosure = item.ChanceComponentClosure * 100;
                    item.ChanceComponentContainment = item.ChanceComponentContainment * 100;
                    item.ChanceComponentReservoir = item.ChanceComponentReservoir * 100;
                    item.ChanceComponentSource = item.ChanceComponentSource * 100;
                    item.ChanceComponentTiming = item.ChanceComponentTiming * 100;*/
                }
                return Json(datas.ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> EditingCustomESDCForecastView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCForecastDto> PRLst = new List<TXESDCForecastDto>();
            List<TX_ESDCForecast> PRLstEnt = new List<TX_ESDCForecast>();
            //var datas = await _serviceCR.GetPaged(model.GridParam);
            var datas = await _service.GetForecastByStructureID(clientData.Data);
            if (datas.Count() == 0)
            {
                TXESDCForecastDto PRObj = new TXESDCForecastDto();
                PRObj.xStructureID = "1";
                PRObj.Year = 0;
                PRObj.TPFOil = 0;
                PRObj.TPFCondensate = 0;
                PRObj.TPFAssociated = 0;
                PRObj.TPFNonAssociated = 0;
                PRObj.SFOil = 0;
                PRObj.SFCondensate = 0;
                PRObj.SFAssociated = 0;
                PRObj.SFNonAssociated = 0;
                PRObj.CIOOil = 0;
                PRObj.CIOCondensate = 0;
                PRObj.CIOAssociated = 0;
                PRObj.CIONonAssociated = 0;
                PRObj.LPOil = 0;
                PRObj.LPCondensate = 0;
                PRObj.LPAssociated = 0;
                PRObj.LPNonAssociated = 0;
                PRObj.AverageGrossHeat = 0;
                PRObj.Remarks = "";
                PRLst.Add(PRObj);
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                PRLst.AddRange(datas);
                return Json(PRLst.ToDataSourceResult(request));
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCForecast_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCForecastDto> forecast)
        {
            if (forecast != null && ModelState.IsValid)
            {
                foreach (var product in forecast)
                {
                    _service.Update(product);
                }
            }

            return Json(forecast.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCForecast_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCForecastDto> forecast)
        {
            var results = new List<TXESDCForecastDto>();

            if (forecast != null && ModelState.IsValid)
            {
                foreach (var item in forecast)
                {
                    _service.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCForecast_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCForecastDto> forecast)
        {
            foreach (var item in forecast)
            {
                _service.Destroy(item.xStructureID, item.Year);
            }

            return Json(forecast.ToDataSourceResult(request, ModelState));
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
    }
}