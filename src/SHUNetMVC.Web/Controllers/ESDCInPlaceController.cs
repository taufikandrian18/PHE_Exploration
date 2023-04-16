using ASPNetMVC.Abstraction.Model.Entities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SHUNetMVC.Abstraction.Model.Dto;
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
    public class ESDCInPlaceController : Controller
    {
        private readonly ITXESDCInPlaceService _service;
        public ESDCInPlaceController(ITXESDCInPlaceService service)
        {
            _service = service;
        }
        // GET: ESDCInPlace
        public ActionResult EditingCustomESDCInPlace()
        {
            //PopulateCategories();
            var define = DefineESDCInPlace(FormState.Create, "");
            ////return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomESDCInPlaceEdit(string structureID)
        {
            //PopulateCategories();
            var define = DefineESDCInPlace(FormState.Edit, structureID);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }

        protected FormDefinition DefineESDCInPlace(FormState formState, string structureID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "ESDCInPlace",
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
                var datas = Task.Run(() => _service.GetListTXESDCInPlaceByStructureID(structureID)).Result;
                var formDef = new FormDefinition
                {
                    Title = "ESDCInPlace",
                    State = formState,
                    paramID = structureID,
                    DataESDCIn = datas,
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
        public ActionResult EditingCustomESDCInPlace_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCInPlaceDto> IPLst = new List<TXESDCInPlaceDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ESDCInPlace",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC InPlace",
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

                for (int i = 0; i < 3; i++)
                {
                    TXESDCInPlaceDto IPObj = new TXESDCInPlaceDto();
                    IPObj.xStructureID = "0";
                    if (i == 0)
                    {
                        IPObj.UncertaintyLevel = "Low Value";
                    }
                    else if (i == 1)
                    {
                        IPObj.UncertaintyLevel = "Middle Value";
                    }
                    else
                    {
                        IPObj.UncertaintyLevel = "High Value";
                    }
                    IPObj.P90IGIP = 0;
                    IPObj.P90IOIP = 0;
                    IPLst.Add(IPObj);
                }
                return Json(IPLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ESDCInPlace",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC InPlace",
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
                var datas = Task.Run(() => _service.GetListTXESDCInPlaceByStructureID(clientData.Data)).Result;
                IPLst.AddRange(datas);
                int i = 0;
                List<TXESDCInPlaceDto> IPTemp = new List<TXESDCInPlaceDto>();
                foreach (var item in IPLst)
                {
                    if (i == 0)
                    {
                        IPTemp.Add(IPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("low")).FirstOrDefault());
                    }
                    else if (i == 1)
                    {
                        IPTemp.Add(IPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("mid")).FirstOrDefault());
                    }
                    else
                    {
                        IPTemp.Add(IPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("high")).FirstOrDefault());
                    }
                    i++;
                }
                return Json(IPTemp.ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> EditingCustomESDCInPlaceView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCInPlaceDto> IPLst = new List<TXESDCInPlaceDto>();
            List<TX_ESDCInPlace> PRLstEnt = new List<TX_ESDCInPlace>();
            //var datas = await _serviceCR.GetPaged(model.GridParam);
            var datas = await _service.GetListTXESDCInPlaceByStructureID(clientData.Data);
            if (datas.Count() == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    TXESDCInPlaceDto IPObj = new TXESDCInPlaceDto();
                    IPObj.xStructureID = "0";
                    if (i == 0)
                    {
                        IPObj.UncertaintyLevel = "Low Value";
                    }
                    else if (i == 1)
                    {
                        IPObj.UncertaintyLevel = "Middle Value";
                    }
                    else
                    {
                        IPObj.UncertaintyLevel = "High Value";
                    }
                    IPObj.P90IGIP = 0;
                    IPObj.P90IOIP = 0;
                    IPLst.Add(IPObj);
                }
                return Json(IPLst.ToDataSourceResult(request));
            }
            else
            {
                IPLst.AddRange(datas);
                int i = 0;
                List<TXESDCInPlaceDto> IPTemp = new List<TXESDCInPlaceDto>();
                foreach (var item in IPLst)
                {
                    if (i == 0)
                    {
                        IPTemp.Add(IPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("low")).FirstOrDefault());
                    }
                    else if (i == 1)
                    {
                        IPTemp.Add(IPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("mid")).FirstOrDefault());
                    }
                    else
                    {
                        IPTemp.Add(IPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("high")).FirstOrDefault());
                    }
                    i++;
                }
                return Json(IPTemp.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCInPlace_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCInPlaceDto> esdcInPlace)
        {
            if (esdcInPlace != null && ModelState.IsValid)
            {
                foreach (var product in esdcInPlace)
                {
                    _service.Update(product);
                }
            }

            return Json(esdcInPlace.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCInPlace_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCInPlaceDto> esdcInPlace)
        {
            var results = new List<TXESDCInPlaceDto>();

            if (esdcInPlace != null && ModelState.IsValid)
            {
                foreach (var item in esdcInPlace)
                {
                    _service.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCInPlace_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCInPlaceDto> esdcInPlace)
        {
            foreach (var item in esdcInPlace)
            {
                _service.Destroy(item.xStructureID, item.UncertaintyLevel.Trim());
            }

            return Json(esdcInPlace.ToDataSourceResult(request, ModelState));
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
    }
}