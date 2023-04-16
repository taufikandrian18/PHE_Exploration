using ASPNetMVC.Abstraction.Model.Entities;
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
    public class ContingenResourcesController : Controller
    {
        private readonly ITXContingenResourcesService _serviceCR;
        public ContingenResourcesController(ITXContingenResourcesService serviceCR)
        {
            _serviceCR = serviceCR;
        }
        public ActionResult EditingCustomCR()
        {
            var define = DefineContResources(FormState.Create, "");
            //FormState ts = FormState.Create;
            //var define = DefineGeneralInfo(ts);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }
        public ActionResult EditingCustomEdit(string structureID)
        {
            var define = DefineContResources(FormState.Edit, structureID);
            //FormState ts = FormState.Create;
            //var define = DefineGeneralInfo(ts);
            //return PartialView("Component/Form/Form", define);
            return PartialView("Create", define);
        }

        protected FormDefinition DefineContResources(FormState formState, string structureID)
        {
            if(formState == FormState.Create)
            {
                var formDef = new FormDefinition
                {
                    Title = "ContingenResources",
                    State = formState,
                    paramID = structureID,
                    FieldSections = new List<FieldSection>()
                    {
                        FieldContingenResources()
                    }
                };
                return formDef;
            }
            else
            {
                var datas = Task.Run(() => _serviceCR.GetOne(structureID)).Result;
                if(!string.IsNullOrEmpty(datas.xStructureID))
                {
                    var formDef = new FormDefinition
                    {
                        Title = "ContingenResources",
                        State = formState,
                        paramID = structureID,
                        DataCont = datas,
                        FieldSections = new List<FieldSection>()
                        {
                            FieldContingenResources()
                        }
                    };
                    return formDef;
                }
                else
                {
                    TXContingenResourcesDto PRObj = new TXContingenResourcesDto();
                    PRObj.xStructureID = "1";
                    PRObj.ExplorationStructureName = "Contingen Resources 1";
                    PRObj.C1COil = 0;
                    PRObj.C2COil = 0;
                    PRObj.C3COil = 0;
                    PRObj.C1CGas = 0;
                    PRObj.C2CGas = 0;
                    PRObj.C3CGas = 0;
                    PRObj.C1CTotal = 0;
                    PRObj.C2CTotal = 0;
                    PRObj.C3CTotal = 0;
                    var formDef = new FormDefinition
                    {
                        Title = "ContingenResources",
                        State = formState,
                        paramID = structureID,
                        DataCont = PRObj,
                        FieldSections = new List<FieldSection>()
                        {
                            FieldContingenResources()
                        }
                    };
                    return formDef;
                }
            }
        }
        private FieldSection FieldContingenResources()
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
        public ActionResult EditingCustomCR_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXContingenResourcesDto> PRLst = new List<TXContingenResourcesDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                TXContingenResourcesDto PRObj = new TXContingenResourcesDto();
                PRObj.xStructureID = "1";
                PRObj.ExplorationStructureName = "Contingen Resources 1";
                PRObj.C1COil = 0;
                PRObj.C2COil = 0;
                PRObj.C3COil = 0;
                PRObj.C1CGas = 0;
                PRObj.C2CGas = 0;
                PRObj.C3CGas = 0;
                PRObj.C1CTotal = 0;
                PRObj.C2CTotal = 0;
                PRObj.C3CTotal = 0;
                PRLst.Add(PRObj);
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ContingenResources",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "cr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                List<TX_ContingentResources> PRLstEnt = new List<TX_ContingentResources>();
                //var datas = await _serviceCR.GetPaged(model.GridParam);
                var datas = Task.Run(() => _serviceCR.GetContResourceTargetByStructureID(clientData.Data)).Result;
                if (datas != null)
                {
                    PRLstEnt.Add(datas);
                    return Json(PRLstEnt.ToDataSourceResult(request));
                }
                else
                {
                    List<TXContingenResourcesDto> prList = new List<TXContingenResourcesDto>();
                    TXContingenResourcesDto PRObj = new TXContingenResourcesDto();
                    PRObj.xStructureID = "1";
                    PRObj.ExplorationStructureName = "Contingen Resources 1";
                    PRObj.C1COil = 0;
                    PRObj.C2COil = 0;
                    PRObj.C3COil = 0;
                    PRObj.C1CGas = 0;
                    PRObj.C2CGas = 0;
                    PRObj.C3CGas = 0;
                    PRObj.C1CTotal = 0;
                    PRObj.C2CTotal = 0;
                    PRObj.C3CTotal = 0;
                    prList.Add(PRObj);
                    return Json(prList.ToDataSourceResult(request));
                }
            }

        }
        public async Task<ActionResult> EditingCustomCRView_Read([DataSourceRequest] DataSourceRequest request, ClientDataModelView clientData)
        {
            List<TXContingenResourcesDto> PRLst = new List<TXContingenResourcesDto>();
            if (clientData.State.ToLower().Trim() == "create")
            {
                TXContingenResourcesDto PRObj = new TXContingenResourcesDto();
                PRObj.xStructureID = "1";
                PRObj.ExplorationStructureName = "Contingen Resources 1";
                PRObj.C1COil = 0;
                PRObj.C2COil = 0;
                PRObj.C3COil = 0;
                PRObj.C1CGas = 0;
                PRObj.C2CGas = 0;
                PRObj.C3CGas = 0;
                PRObj.C1CTotal = 0;
                PRObj.C2CTotal = 0;
                PRObj.C3CTotal = 0;
                PRLst.Add(PRObj);
                return Json(PRLst.ToDataSourceResult(request));
            }
            else
            {
                var model = new CrudPage
                {
                    Id = "ContingenResources",
                    Title = "Pencatatan Sumber Daya",
                    SubTitle = "This is the list of Exploration Structure",
                    GridParam = new GridParam
                    {
                        GridId = this.GetType().Name + "_grid",
                        FilterList = new FilterList
                        {
                            OrderBy = "cr.CreatedDate desc",
                            Page = 1,
                            Size = 1000
                        }
                    }
                };
                List<TX_ContingentResources> PRLstEnt = new List<TX_ContingentResources>();
                //var datas = await _serviceCR.GetPaged(model.GridParam);
                var datas = await _serviceCR.GetOne(clientData.Data);
                if(datas == null)
                {
                    TXContingenResourcesDto PRObj = new TXContingenResourcesDto();
                    PRObj.xStructureID = "1";
                    PRObj.ExplorationStructureName = "Contingen Resources 1";
                    PRObj.C1COil = 0;
                    PRObj.C2COil = 0;
                    PRObj.C3COil = 0;
                    PRObj.C1CGas = 0;
                    PRObj.C2CGas = 0;
                    PRObj.C3CGas = 0;
                    PRObj.C1CTotal = 0;
                    PRObj.C2CTotal = 0;
                    PRObj.C3CTotal = 0;
                    PRLst.Add(PRObj);
                    return Json(PRLst.ToDataSourceResult(request));
                }
                else
                {
                    PRLst.Add(datas);
                    return Json(PRLst.ToDataSourceResult(request));
                }
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomCR_Update([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXContingenResourcesDto> contResourcesTarget)
        {
            if (contResourcesTarget != null && ModelState.IsValid)
            {
                foreach (var product in contResourcesTarget)
                {
                    _serviceCR.Update(product);
                }
            }

            return Json(contResourcesTarget.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomCR_Create([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXContingenResourcesDto> contResourcesTarget)
        {
            var results = new List<TXContingenResourcesDto>();

            if (contResourcesTarget != null && ModelState.IsValid)
            {
                foreach (var item in contResourcesTarget)
                {
                    _serviceCR.Create(item);

                    results.Add(item);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingCustomCR_Destroy([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")] IEnumerable<TXContingenResourcesDto> contResourcesTarget)
        {
            foreach (var item in contResourcesTarget)
            {
                _serviceCR.Destroy(item.xStructureID);
            }

            return Json(contResourcesTarget.ToDataSourceResult(request, ModelState));
        }

        public sealed class ClientDataModelView
        {
            public string Data { get; set; }
            public string State { get; set; }
        }
    }
}