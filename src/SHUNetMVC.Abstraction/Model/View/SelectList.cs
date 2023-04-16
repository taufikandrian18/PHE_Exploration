using System.Collections.Generic;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class LookupList
    {
        
        public string ColumnId { get; set; }
        public List<LookupItem> Items { get; set; }
    }

    public class LookupItem
    {
        public string Text { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Untuk selection yang lebih dari 1 property untuk di view
        /// </summary>
        public object Object { get; set; }

        public string Value { get; set; }
        public bool Selected { get; set; }
    }

    public class CountExplorationStructureName
    {
        public bool IsExist { get; set; } 
    }

    public class MPEntityListITem
    {
        public string SubholdingID { get; set; }
        public string EntityName { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string Zona { get; set; }
        public string ZonaName { get; set; }
        public string APH { get; set; }
        public string APHName { get; set; }
        public string AssetName { get; set; }
        public string BasinName { get; set; }
        public string AreaName { get; set; }
        public MPEntityBlockOject BlockObject { get; set; }
    }

    public class MPESDCEntityListITem
    {
        public string RKAPYearValue { get; set; }
        public string RKAPYearText { get; set; }
        public string UDTypeValue { get; set; }
        public string UDTypeText { get; set; }
        public string UDClassValue { get; set; }
        public string UDClassText { get; set; }
        public string UDSubClassValue { get; set; }
        public string UDSubClassText { get; set; }
        public string Play { get; set; }
        public string SubholdingID { get; set; }
        public string EntityName { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string Zona { get; set; }
        public string ZonaName { get; set; }
        public string APH { get; set; }
        public string APHName { get; set; }
        public string AssetName { get; set; }
        public string BasinName { get; set; }
        public string AreaName { get; set; }
        public int fileId1 { get; set; }
        public string fileUrl1 { get; set; }
        public string fileType1 { get; set; }
        public string fileName1 { get; set; }
        public int fileId2 { get; set; }
        public string fileUrl2 { get; set; }
        public string fileType2 { get; set; }
        public string fileName2 { get; set; }
        public MPEntityBlockOject BlockObject { get; set; }
    }

    public class MPEntityBlockOject
    {
        public string BlockID { get; set; }
        public string BlockName { get; set; }
        public string AwardDate { get; set; }
        public string ExpiredDate { get; set; }
        public string BlockStatusParID { get; set; }
        public string BlockStatusParValueID { get; set; }
        public string OperatorshipStatusParID { get; set; }
        public string OperatorshipStatusParValueID { get; set; }
        public string OperatorName { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
    }
}
