using SHUNetMVC.Abstraction.Model.Dto;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class FormDefinition
    {
        public string Title { get; set; }
        public string paramID { get; set; }
        public TXProsResourceDto Data { get; set; }
        public List<TXProsResourcesTargetDto> DataProsResourceTarget { get; set; }
        public List<TXDrillingDto> DataDrilling { get; set; }
        public List<TXESDCProductionDto> DataESDCProd { get; set; }
        public List<TXESDCVolumetricDto> DataESDCVol { get; set; }
        public List<TXESDCForecastDto> DataESDCFor { get; set; }
        public List<TXESDCDiscrepancyDto> DataESDCDis { get; set; }
        public List<TXESDCInPlaceDto> DataESDCIn { get; set; }
        public TXContingenResourcesDto DataCont { get; set; }
        public IEnumerable<MDExplorationBlockPartnerDto> DataPartner { get; set; }
        public string BlockID { get; set; }
        public FormState State { get; set; }
        public string StatusData { get; set; }
        public List<FieldSection> FieldSections { get; set; }

        /// <summary>
        /// Single Section
        /// </summary>
        public FieldSection FieldSection
        {
            set
            {
                FieldSections = new List<FieldSection>
                {
                    value
                };
            }
        }
        public string Value { get; set; }

        public FormDefinition()
        {
            FieldSections = new List<FieldSection>();
        }

        public FormDefinition(FieldSection fieldSection)
        {
            FieldSection = fieldSection;
        }
    }


    public class FieldSection
    {
        public string SectionName { get; set; } 
        public List<Field> Fields { get; set; }

        public FieldSection()
        {
            Fields = new List<Field>();
        }
    }

    public class Field
    {
        public string Id { get; set; }
        public string Label { get; set; }
       
        public object Value { get; set; }
        public object FileID { get; set; }
        public object Filename { get; set; }
        public object FileType { get; set; }
        public bool FileUploadESDC { get; set; }

        public object ChildPropertyName { get; set; }
        public FieldType FieldType { get; set; }

        public LookupList LookUpList { get; set; }
        public string LookUpController { get; set; }
        public string LookUpOrderBy { get; set; }
        public int? MaxLength { get; set; }
        public int? MinNumber { get; set; }
        public int? MaxNumber { get; set; }
        public bool IsRequired { get; set; }
        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public FormDefinition FormDefinition { get; set; }
        public bool IsDisabled { get; set; }

        public Field()
        {
        }

        public Field(string fieldId, FieldType fieldType)
        {
            Id = fieldId;
            Label = fieldId;
            FieldType = fieldType;
        }

        public Field(string fieldId, string label, FieldType fieldType)
        {
            Id = fieldId;
            Label = label;
            FieldType = fieldType;
        }
    }
    public enum FormState
    {
        Unknown,
        Create,
        Edit,
        View,
    }

    public enum InputState
    {
        None,
        ReadOnly,
        Disabled,
    }


    public enum FieldType
    {
        Text,
        TextArea,
        Email,
        Phone,
        Number,
        Decimal,
        Percentage,
        Dropdown,
        MultiSelect,
        Date,
        DateTime,
        Radio,
        MultiCheckbox,
        Hidden,
        Switch,
        Lookup,
        Grid,
        FileUpload
    }


    public class ListOfObjectDefinition
    {
        public FormDefinition FormDefinitions { get; set; }
        public List<ColumnDefinition> ColumnDefinitions { get; set; }

    }
}
