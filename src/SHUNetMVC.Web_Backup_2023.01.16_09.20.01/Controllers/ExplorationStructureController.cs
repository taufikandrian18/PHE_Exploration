using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using SHUNetMVC.Infrastructure.Validators;
using SHUNetMVC.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASPNetMVC.Web.Controllers
{
    public class ExplorationStructureController : BaseCrudController<MDExplorationStructureDto, MDExplorationStructureWithAdditionalFields>
    {
        private readonly IMDExplorationStructureService _explorationStructureService;
        public ExplorationStructureController(IMDExplorationStructureService crudSvc, ILookupService lookupSvc) : base(crudSvc, lookupSvc)
        {
            _explorationStructureService = crudSvc;
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
                new ColumnDefinition("StructureID", nameof(MDExplorationStructureWithAdditionalFields.xStructureID), ColumnType.Id),
                new ColumnDefinition("Exploration Structure", nameof(MDExplorationStructureWithAdditionalFields.xStructureName), ColumnType.String),
                new ColumnDefinition("Exploration Status", nameof(MDExplorationStructureWithAdditionalFields.ParamValue1Text), ColumnType.String),
                new ColumnDefinition("Zona", nameof(MDExplorationStructureWithAdditionalFields.ZonaID), ColumnType.String),
                new ColumnDefinition("Basin", nameof(MDExplorationStructureWithAdditionalFields.BasinName), ColumnType.String,"ba.BasinName"),
                new ColumnDefinition("Region", nameof(MDExplorationStructureWithAdditionalFields.RegionalID), ColumnType.String),
                new ColumnDefinition("Block", nameof(MDExplorationStructureWithAdditionalFields.xBlockName), ColumnType.String, "bl.BlockName"),
                new ColumnDefinition("Country", nameof(MDExplorationStructureWithAdditionalFields.CountriesID), ColumnType.String),
                new ColumnDefinition("Data Status", nameof(MDExplorationStructureWithAdditionalFields.StatusData), ColumnType.String)
            };
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
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }
    }
}