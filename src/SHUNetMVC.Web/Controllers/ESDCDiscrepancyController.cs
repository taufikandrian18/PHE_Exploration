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
    public class ESDCDiscrepancyController : Controller
    {
        private readonly ITXESDCDiscrepancyService _service;
        public ESDCDiscrepancyController(ITXESDCDiscrepancyService service)
        {
            _service = service;
        }
        // GET: ESDCDiscrepancy
        public ActionResult EditingCustomESDCDiscrepancy()
        {
            //PopulateCategories();
            var define = DefineESDCDiscrepancy(FormState.Create, "");
            ////return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomESDCDiscrepancyEdit(string structureID)
        {
            //PopulateCategories();
            var define = DefineESDCDiscrepancy(FormState.Edit, structureID);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }

        protected FormDefinition DefineESDCDiscrepancy(FormState formState, string structureID)
        {
            if (formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "ESDCDiscrepancy",
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
                var datas = Task.Run(() => _service.GetListTXESDCDiscrepancyByStructureID(structureID)).Result;
                var formDef = new FormDefinition
                {
                    Title = "ESDCDiscrepancy",
                    State = formState,
                    paramID = structureID,
                    DataESDCDis = datas,
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
        public ActionResult EditingCustomESDCDiscrepancy_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCDiscrepancyDto> DPLst = new List<TXESDCDiscrepancyDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                var model = new CrudPage
                {
                    Id = "ESDCDiscrepancy",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC Discrepancy",
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
                    TXESDCDiscrepancyDto DPObj = new TXESDCDiscrepancyDto();
                    DPObj.xStructureID = "0";
                    if (i == 0)
                    {
                        DPObj.UncertaintyLevel = "Low Value";
                    }
                    else if (i == 1)
                    {
                        DPObj.UncertaintyLevel = "Middle Value";
                    }
                    else
                    {
                        DPObj.UncertaintyLevel = "High Value";
                    }
                    DPObj.CFUMOil = 0;
                    DPObj.CFUMCondensate = 0;
                    DPObj.CFUMAssociated = 0;
                    DPObj.CFUMNonAssociated = 0;
                    DPObj.CFPPAOil = 0;
                    DPObj.CFPPACondensate = 0;
                    DPObj.CFPPAAssociated = 0;
                    DPObj.CFPPANonAssociated = 0;
                    DPObj.CFWIOil = 0;
                    DPObj.CFWICondensate = 0;
                    DPObj.CFWIAssociated = 0;
                    DPObj.CFWINonAssociated = 0;
                    DPObj.CFCOil = 0;
                    DPObj.CFCCondensate = 0;
                    DPObj.CFCAssociated = 0;
                    DPObj.CFCNonAssociated = 0;
                    DPObj.UCOil = 0;
                    DPObj.UCCondensate = 0;
                    DPObj.UCAssociated = 0;
                    DPObj.UCNonAssociated = 0;
                    DPObj.CIOOil = 0;
                    DPObj.CIOCondensate = 0;
                    DPObj.CIOAssociated = 0;
                    DPObj.CIONonAssociated = 0;
                    DPLst.Add(DPObj);
                }
                return Json(DPLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ESDCDiscrepancy",
                    Title = "ESDC",
                    SubTitle = "This is the list of ESDC Discrepancy",
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
                var datas = Task.Run(() => _service.GetListTXESDCDiscrepancyByStructureID(clientData.Data)).Result;
                DPLst.AddRange(datas);
                int i = 0;
                List<TXESDCDiscrepancyDto> DPTemp = new List<TXESDCDiscrepancyDto>();
                foreach (var item in DPLst)
                {
                    if (i == 0)
                    {
                        DPTemp.Add(DPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("low")).FirstOrDefault());
                    }
                    else if (i == 1)
                    {
                        DPTemp.Add(DPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("mid")).FirstOrDefault());
                    }
                    else
                    {
                        DPTemp.Add(DPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("high")).FirstOrDefault());
                    }
                    i++;
                }
                return Json(DPTemp.ToDataSourceResult(request));
            }

        }
        public async Task<ActionResult> EditingCustomESDCDiscrepancyView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXESDCDiscrepancyDto> DPLst = new List<TXESDCDiscrepancyDto>();
            List<TX_ESDCDiscrepancy> DPLstEnt = new List<TX_ESDCDiscrepancy>();
            //var datas = await _serviceCR.GetPaged(model.GridParam);
            var datas = await _service.GetListTXESDCDiscrepancyByStructureID(clientData.Data);
            if (datas.Count() == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    TXESDCDiscrepancyDto DPObj = new TXESDCDiscrepancyDto();
                    DPObj.xStructureID = "0";
                    if (i == 0)
                    {
                        DPObj.UncertaintyLevel = "Low Value";
                    }
                    else if (i == 1)
                    {
                        DPObj.UncertaintyLevel = "Middle Value";
                    }
                    else
                    {
                        DPObj.UncertaintyLevel = "High Value";
                    }
                    DPObj.CFUMOil = 0;
                    DPObj.CFUMCondensate = 0;
                    DPObj.CFUMAssociated = 0;
                    DPObj.CFUMNonAssociated = 0;
                    DPObj.CFPPAOil = 0;
                    DPObj.CFPPACondensate = 0;
                    DPObj.CFPPAAssociated = 0;
                    DPObj.CFPPANonAssociated = 0;
                    DPObj.CFWIOil = 0;
                    DPObj.CFWICondensate = 0;
                    DPObj.CFWIAssociated = 0;
                    DPObj.CFWINonAssociated = 0;
                    DPObj.CFCOil = 0;
                    DPObj.CFCCondensate = 0;
                    DPObj.CFCAssociated = 0;
                    DPObj.CFCNonAssociated = 0;
                    DPObj.UCOil = 0;
                    DPObj.UCCondensate = 0;
                    DPObj.UCAssociated = 0;
                    DPObj.UCNonAssociated = 0;
                    DPObj.CIOOil = 0;
                    DPObj.CIOCondensate = 0;
                    DPObj.CIOAssociated = 0;
                    DPObj.CIONonAssociated = 0;
                    DPLst.Add(DPObj);
                }
                return Json(DPLst.ToDataSourceResult(request));
            }
            else
            {
                DPLst.AddRange(datas);
                int i = 0;
                List<TXESDCDiscrepancyDto> DPTemp = new List<TXESDCDiscrepancyDto>();
                foreach (var item in DPLst)
                {
                    if (i == 0)
                    {
                        DPTemp.Add(DPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("low")).FirstOrDefault());
                    }
                    else if (i == 1)
                    {
                        DPTemp.Add(DPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("mid")).FirstOrDefault());
                    }
                    else
                    {
                        DPTemp.Add(DPLst.Where(x => x.UncertaintyLevel.ToLower().Contains("high")).FirstOrDefault());
                    }
                    i++;
                }
                return Json(DPTemp.ToDataSourceResult(request));
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCDiscrepancy_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCDiscrepancyDto> esdcDiscrepancy)
        {
            if (esdcDiscrepancy != null && ModelState.IsValid)
            {
                foreach (var product in esdcDiscrepancy)
                {
                    _service.Update(product);
                }
            }

            return Json(esdcDiscrepancy.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCDiscrepancy_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCDiscrepancyDto> esdcDiscrepancy)
        {
            var results = new List<TXESDCDiscrepancyDto>();

            if (esdcDiscrepancy != null && ModelState.IsValid)
            {
                foreach (var item in esdcDiscrepancy)
                {
                    _service.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomESDCDiscrepancy_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXESDCDiscrepancyDto> esdcDiscrepancy)
        {
            foreach (var item in esdcDiscrepancy)
            {
                _service.Destroy(item.xStructureID, item.UncertaintyLevel.Trim());
            }

            return Json(esdcDiscrepancy.ToDataSourceResult(request, ModelState));
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
    }
}