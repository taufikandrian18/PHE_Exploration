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
    public class ESDCVolumetricController : Controller
    {
        private readonly ITXESDCVolumetricService _service;
        public ESDCVolumetricController(ITXESDCVolumetricService service)
        {
            _service = service;
        }
        // GET: ESDCVolumetric
        public ActionResult EditingCustomESDCVolumetric()
        {
            //PopulateCategories();
            var define = DefineESDCVolumetric(FormState.Create, "");
            ////return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomESDCVolumetricEdit(string structureID)
        {
            //PopulateCategories();
            var define = DefineESDCVolumetric(FormState.Edit, structureID);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }

        protected FormDefinition DefineESDCVolumetric(FormState formState, string structureID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "ESDCVolumetric",
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
                var datas = Task.Run(() => _service.GetListTXESDCVolumetricByStructureID(structureID)).Result;
                var formDef = new FormDefinition
                {
                    Title = "ESDCVolumetric",
                    State = formState,
                    paramID = structureID,
                    DataESDCVol = datas,
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
        public ActionResult EditingCustomESDCVolumetric_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCVolumetricDto> PRLst = new List<TXESDCVolumetricDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ESDCVolumetric",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC Volumetric",
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
                    TXESDCVolumetricDto PRObj = new TXESDCVolumetricDto();
                    PRObj.xStructureID = "0";
                    if (i == 0)
                    {
                        PRObj.UncertaintyLevel = "Low Value";
                    }
                    else if (i == 1)
                    {
                        PRObj.UncertaintyLevel = "Middle Value";
                    }
                    else
                    {
                        PRObj.UncertaintyLevel = "High Value";
                    }
                    PRObj.GRRPrevOil = 0;
                    PRObj.GRRPrevCondensate = 0;
                    PRObj.GRRPrevAssociated = 0;
                    PRObj.GRRPrevNonAssociated = 0;
                    PRObj.ReservesPrevOil = 0;
                    PRObj.ReservesPrevCondensate = 0;
                    PRObj.ReservesPrevAssociated = 0;
                    PRObj.ReservesPrevNonAssociated = 0;
                    PRObj.GOIOil = 0;
                    PRObj.GOICondensate = 0;
                    PRObj.GOIAssociated = 0;
                    PRObj.GOINonAssociated = 0;
                    PRObj.ReservesOil = 0;
                    PRObj.ReservesCondensate = 0;
                    PRObj.ReservesAssociated = 0;
                    PRObj.ReservesNonAssociated = 0;
                    PRLst.Add(PRObj);
                }
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ESDCVolumetric",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC Volumetric",
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
                var datas = Task.Run(() => _service.GetListTXESDCVolumetricByStructureID(clientData.Data)).Result;
                PRLst.AddRange(datas);
                int i = 0;
                List<TXESDCVolumetricDto> PRTemp = new List<TXESDCVolumetricDto>();
                foreach (var item in PRLst)
                {
                    if (i == 0)
                    {
                        PRTemp.Add(PRLst.Where(x => x.UncertaintyLevel.ToLower().Contains("low")).FirstOrDefault());
                    }
                    else if (i == 1)
                    {
                        PRTemp.Add(PRLst.Where(x => x.UncertaintyLevel.ToLower().Contains("mid")).FirstOrDefault());
                    }
                    else
                    {
                        PRTemp.Add(PRLst.Where(x => x.UncertaintyLevel.ToLower().Contains("high")).FirstOrDefault());
                    }
                    i++;
                }
                return Json(PRTemp.ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> EditingCustomESDCVolumetricView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCVolumetricDto> PRLst = new List<TXESDCVolumetricDto>();
            List<TX_ESDCVolumetric> PRLstEnt = new List<TX_ESDCVolumetric>();
            //var datas = await _serviceCR.GetPaged(model.GridParam);
            var datas = await _service.GetListTXESDCVolumetricByStructureID(clientData.Data);
            if (datas.Count() == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    TXESDCVolumetricDto PRObj = new TXESDCVolumetricDto();
                    PRObj.xStructureID = "0";
                    if (i == 0)
                    {
                        PRObj.UncertaintyLevel = "Low Value";
                    }
                    else if (i == 1)
                    {
                        PRObj.UncertaintyLevel = "Middle Value";
                    }
                    else
                    {
                        PRObj.UncertaintyLevel = "High Value";
                    }
                    PRObj.GRRPrevOil = 0;
                    PRObj.GRRPrevCondensate = 0;
                    PRObj.GRRPrevAssociated = 0;
                    PRObj.GRRPrevNonAssociated = 0;
                    PRObj.ReservesPrevOil = 0;
                    PRObj.ReservesPrevCondensate = 0;
                    PRObj.ReservesPrevAssociated = 0;
                    PRObj.ReservesPrevNonAssociated = 0;
                    PRObj.GOIOil = 0;
                    PRObj.GOICondensate = 0;
                    PRObj.GOIAssociated = 0;
                    PRObj.GOINonAssociated = 0;
                    PRObj.ReservesOil = 0;
                    PRObj.ReservesCondensate = 0;
                    PRObj.ReservesAssociated = 0;
                    PRObj.ReservesNonAssociated = 0;
                    PRLst.Add(PRObj);
                }
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                PRLst.AddRange(datas);
                int i = 0;
                List<TXESDCVolumetricDto> PRTemp = new List<TXESDCVolumetricDto>();
                foreach (var item in PRLst)
                {
                    if (i == 0)
                    {
                        PRTemp.Add(PRLst.Where(x => x.UncertaintyLevel.ToLower().Contains("low")).FirstOrDefault());
                    }
                    else if (i == 1)
                    {
                        PRTemp.Add(PRLst.Where(x => x.UncertaintyLevel.ToLower().Contains("mid")).FirstOrDefault());
                    }
                    else
                    {
                        PRTemp.Add(PRLst.Where(x => x.UncertaintyLevel.ToLower().Contains("high")).FirstOrDefault());
                    }
                    i++;
                }
                return Json(PRTemp.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCVolumetric_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCVolumetricDto> esdcVolumetric)
        {
            if (esdcVolumetric != null && ModelState.IsValid)
            {
                foreach (var product in esdcVolumetric)
                {
                    _service.Update(product);
                }
            }

            return Json(esdcVolumetric.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCVolumetric_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCVolumetricDto> esdcVolumetric)
        {
            var results = new List<TXESDCVolumetricDto>();

            if (esdcVolumetric != null && ModelState.IsValid)
            {
                foreach (var item in esdcVolumetric)
                {
                    _service.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCVolumetric_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCVolumetricDto> esdcVolumetric)
        {
            foreach (var item in esdcVolumetric)
            {
                _service.Destroy(item.xStructureID, item.UncertaintyLevel.Trim());
            }

            return Json(esdcVolumetric.ToDataSourceResult(request, ModelState));
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
    }
}