using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class TXEconomicExcelDto
    {
        public string xBlockID { get; set; }
        public string xBlockName { get; set; }
        public Nullable<System.DateTime> AwardDate { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public string OperatorStatusName { get; set; }
        public string OperatorName { get; set; }
        public string DevConcept { get; set; }
        public string EconomicAssumption { get; set; }
        public int CAPEX { get; set; }
        public string CAPEXCurr { get; set; }
        public int OPEXProduction { get; set; }
        public string OPEXProductionCurr { get; set; }
        public int OPEXFacility { get; set; }
        public string OPEXFacilityCurr { get; set; }
        public decimal ASR { get; set; }
        public string ASRCurr { get; set; }
        public string EconomicResult { get; set; }
        public decimal ContractorNPV { get; set; }
        public string ContractorNPVCurr { get; set; }
        public decimal IRR { get; set; }
        public decimal ContractorPOT { get; set; }
        public string ContractorPOTUoM { get; set; }
        public decimal PIncome { get; set; }
        public string PIncomeCurr { get; set; }
        public decimal EMV { get; set; }
        public string EMVCurr { get; set; }
        public decimal NPV { get; set; }
        public string NPVCurr { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
