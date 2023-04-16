using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class TXProsResourcesExcelDto
    {
        public string xStructureID { get; set; }
        public string ExplorationStructureName { get; set; }
        public Nullable<decimal> P90InPlaceOilPR { get; set; }
        public string P90InPlaceOilPRUoM { get; set; }
        public Nullable<decimal> P50InPlaceOilPR { get; set; }
        public string P50InPlaceOilPRUoM { get; set; }
        public Nullable<decimal> PMeanInPlaceOilPR { get; set; }
        public string PMeanInPlaceOilPRUoM { get; set; }
        public Nullable<decimal> P10InPlaceOilPR { get; set; }
        public string P10InPlaceOilPRUoM { get; set; }
        public Nullable<decimal> P90InPlaceGasPR { get; set; }
        public string P90InPlaceGasPRUoM { get; set; }
        public Nullable<decimal> P50InPlaceGasPR { get; set; }
        public string P50InPlaceGasPRUoM { get; set; }
        public Nullable<decimal> PMeanInPlaceGasPR { get; set; }
        public string PMeanInPlaceGasPRUoM { get; set; }
        public Nullable<decimal> P10InPlaceGasPR { get; set; }
        public string P10InPlaceGasPRUoM { get; set; }
        public Nullable<decimal> P90InPlaceTotalPR { get; set; }
        public string P90InPlaceTotalPRUoM { get; set; }
        public Nullable<decimal> P50InPlaceTotalPR { get; set; }
        public string P50InPlaceTotalPRUoM { get; set; }
        public Nullable<decimal> PMeanInPlaceTotalPR { get; set; }
        public string PMeanInPlaceTotalPRUoM { get; set; }
        public Nullable<decimal> P10InPlaceTotalPR { get; set; }
        public string P10InPlaceTotalPRUoM { get; set; }
        public Nullable<decimal> RFOilPR { get; set; }
        public Nullable<decimal> RFGasPR { get; set; }
        public Nullable<decimal> P90RROil { get; set; }
        public string P90RROilUoM { get; set; }
        public Nullable<decimal> P50RROil { get; set; }
        public string P50RROilUoM { get; set; }
        public Nullable<decimal> PMeanRROil { get; set; }
        public string PMeanRROilUoM { get; set; }
        public Nullable<decimal> P10RROil { get; set; }
        public string P10RROilUoM { get; set; }
        public Nullable<decimal> P90RRGas { get; set; }
        public string P90RRGasUoM { get; set; }
        public Nullable<decimal> P50RRGas { get; set; }
        public string P50RRGasUoM { get; set; }
        public Nullable<decimal> PMeanRRGas { get; set; }
        public string PMeanRRGasUoM { get; set; }
        public Nullable<decimal> P10RRGas { get; set; }
        public string P10RRGasUoM { get; set; }
        public Nullable<decimal> P90RRTotal { get; set; }
        public string P90RRTotalUoM { get; set; }
        public Nullable<decimal> P50RRTotal { get; set; }
        public string P50RRTotalUoM { get; set; }
        public Nullable<decimal> PMeanRRTotal { get; set; }
        public string PMeanRRTotalUoM { get; set; }
        public Nullable<decimal> P10RRTotal { get; set; }
        public string P10RRTotalUoM { get; set; }
        public string HydrocarbonTypePRParID { get; set; }
        public Nullable<decimal> GCFSRPR { get; set; }
        public string GCFSRPRUoM { get; set; }
        public Nullable<decimal> GCFTMPR { get; set; }
        public string GCFTMPRUoM { get; set; }
        public Nullable<decimal> GCFReservoirPR { get; set; }
        public string GCFReservoirPRUoM { get; set; }
        public Nullable<decimal> GCFClosurePR { get; set; }
        public string GCFClosurePRUoM { get; set; }
        public Nullable<decimal> GCFContainmentPR { get; set; }
        public string GCFContainmentPRUoM { get; set; }
        public Nullable<decimal> GCFPGTotalPR { get; set; }
        public string GCFPGTotalPRUoM { get; set; }
        public Nullable<decimal> ExpectedPG { get; set; }
        public Nullable<decimal> CurrentPG { get; set; }
        public string MethodParID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
