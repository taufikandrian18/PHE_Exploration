//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASPNetMVC.Abstraction.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class LG_Economic
    {
        public string StructureHistoryID { get; set; }
        public string xStructureID { get; set; }
        public string DevConcept { get; set; }
        public string EconomicAssumption { get; set; }
        public Nullable<decimal> CAPEX { get; set; }
        public string CAPEXCurr { get; set; }
        public Nullable<decimal> OPEXProduction { get; set; }
        public string OPEXProductionCurr { get; set; }
        public Nullable<decimal> OPEXFacility { get; set; }
        public string OPEXFacilityCurr { get; set; }
        public Nullable<decimal> ASR { get; set; }
        public string ASRCurr { get; set; }
        public string EconomicResult { get; set; }
        public Nullable<decimal> ContractorNPV { get; set; }
        public string ContractorNPVCurr { get; set; }
        public Nullable<decimal> IRR { get; set; }
        public Nullable<decimal> ContractorPOT { get; set; }
        public string ContractorPOTUoM { get; set; }
        public Nullable<decimal> PIncome { get; set; }
        public string PIncomeCurr { get; set; }
        public Nullable<decimal> EMV { get; set; }
        public string EMVCurr { get; set; }
        public Nullable<decimal> NPV { get; set; }
        public string NPVCurr { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    
        public virtual LG_ExplorationStructure LG_ExplorationStructure { get; set; }
    }
}
