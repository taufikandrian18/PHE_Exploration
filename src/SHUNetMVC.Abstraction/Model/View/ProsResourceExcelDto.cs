using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class ProsResourceExcelDto
    {
        public string xStructureID { get; set; }
        public string ParamValue1Text { get; set; }
        public string xStructureName { get; set; }
        public string Play { get; set; }
        public string RegionalID { get; set; }
        public string xBlockName { get; set; }
        public string ZonaID { get; set; }
        public decimal PI { get; set; }
        public decimal GCFSRPR { get; set; }
        public decimal GCFTMPR { get; set; }
        public decimal GCFReservoirPR { get; set; }
        public decimal GCFClosurePR { get; set; }
        public decimal GCFContainmentPR { get; set; }
        public decimal GCFPGTotalPR { get; set; }
        public decimal P90InPlaceOilPR { get; set; }
        public decimal P50InPlaceOilPR { get; set; }
        public decimal PMeanInPlaceOilPR { get; set; }
        public decimal P10InPlaceOilPR { get; set; }
        public decimal RFOilPR { get; set; }
        public decimal P90RROil { get; set; }
        public decimal P50RROil { get; set; }
        public decimal PMeanRROil { get; set; }
        public decimal P10RROil { get; set; }
        public decimal P90InPlaceGasPR { get; set; }
        public decimal P50InPlaceGasPR { get; set; }
        public decimal PMeanInPlaceGasPR { get; set; }
        public decimal P10InPlaceGasPR { get; set; }
        public decimal RFGasPR { get; set; }
        public decimal P90RRGas { get; set; }
        public decimal P50RRGas { get; set; }
        public decimal PMeanRRGas { get; set; }
        public decimal P10RRGas { get; set; }

        public Nullable<decimal> PIP90InPlaceOilPR { get; set; }
        public Nullable<decimal> PIP50InPlaceOilPR { get; set; }
        public Nullable<decimal> PIPMeanInPlaceOilPR { get; set; }
        public Nullable<decimal> PIP10InPlaceOilPR { get; set; }
        public Nullable<decimal> PIRFOilPR { get; set; }
        public Nullable<decimal> PIP90RROil { get; set; }
        public Nullable<decimal> PIP50RROil { get; set; }
        public Nullable<decimal> PIPMeanRROil { get; set; }
        public Nullable<decimal> PIP10RROil { get; set; }
        public Nullable<decimal> PIP90InPlaceGasPR { get; set; }
        public Nullable<decimal> PIP50InPlaceGasPR { get; set; }
        public Nullable<decimal> PIPMeanInPlaceGasPR { get; set; }
        public Nullable<decimal> PIP10InPlaceGasPR { get; set; }
        public Nullable<decimal> PIRFGasPR { get; set; }
        public Nullable<decimal> PIP90RRGas { get; set; }
        public Nullable<decimal> PIP50RRGas { get; set; }
        public Nullable<decimal> PIPMeanRRGas { get; set; }
        public Nullable<decimal> PIP10RRGas { get; set; }
    }
    public class ExploreRJPPExcelDto
    {
        public string xStructureID { get; set; }
        public string xWellName { get; set; }
        public string PlayOpener { get; set; }
        public string GroupDrill { get; set; }
        public int DrillingCompletionPeriod { get; set; }
        public string Location { get; set; }
        public DateTime ExpectedDrillingDate { get; set; }
        public string APHName { get; set; }
        public string xAssetName { get; set; }
        public string Play { get; set; }
        public string ParamValue1Text { get; set; }
        public string ParamValue1TextUDClass { get; set; }
        public string ParamValue1TextSubUDClass { get; set; }
        public string ParamValue1TextSingleMulti { get; set; }
        public string WellTypeParID { get; set; }
        public string ParamValue1TextOperatorStatus { get; set; }
        public string CommitmentWell { get; set; }
        public string RigTypeParID { get; set; }
        public string OperationalContext { get; set; }
        public string DrillingLocation { get; set; }
        public int TotalDepthMeter { get; set; }
        public string PotentialDelay { get; set; }
        public decimal PartnerName { get; set; }
        public decimal NetRevenueInterest { get; set; }
        public Nullable<decimal> P90ResourceOil { get; set; }
        public Nullable<decimal> P50ResourceOil { get; set; }
        public Nullable<decimal> P10ResourceOil { get; set; }
        public Nullable<decimal> P90ResourceGas { get; set; }
        public Nullable<decimal> P50ResourceGas { get; set; }
        public Nullable<decimal> P10ResourceGas { get; set; }
        public Nullable<decimal> CurrentPG { get; set; }
        public Nullable<decimal> ExpectedPG { get; set; }
        public Nullable<decimal> ChanceComponentSource { get; set; }
        public Nullable<decimal> ChanceComponentTiming { get; set; }
        public Nullable<decimal> ChanceComponentReservoir { get; set; }
        public Nullable<decimal> ChanceComponentClosure { get; set; }
        public Nullable<decimal> ChanceComponentContainment { get; set; }
        public Nullable<decimal> DrillingCost { get; set; }
        public Nullable<decimal> DrillingCostDHB { get; set; }
        public Nullable<decimal> P90NPVProfitabilityOil { get; set; }
        public Nullable<decimal> P50NPVProfitabilityOil { get; set; }
        public Nullable<decimal> P10NPVProfitabilityOil { get; set; }
        public Nullable<decimal> P90NPVProfitabilityGas { get; set; }
        public Nullable<decimal> P50NPVProfitabilityGas { get; set; }
        public Nullable<decimal> P10NPVProfitabilityGas { get; set; }
    }
    public class ESDCExcelDto
    {
        public string xStructureID { get; set; }
        public string ESDCProjectID { get; set; }
        public string xAssetID { get; set; }
        public string ESDCProjectName { get; set; }
        public string ESDCFieldID { get; set; }
        public string RegionalID { get; set; }
        public string xStructureName { get; set; }
        public string ProjectLevel { get; set; }
    }
    public class ESDCProdExcelDto
    {
        public string xStructureID { get; set; }
        public string ESDCProjectID { get; set; }
        public string xAssetID { get; set; }
        public string ESDCProjectName { get; set; }
        public Nullable<decimal> GCPPrevOil { get; set; }
        public Nullable<decimal> GCPPrevCondensate { get; set; }
        public Nullable<decimal> GCPPrevAssociated { get; set; }
        public Nullable<decimal> GCPPrevNonAssociated { get; set; }
        public Nullable<decimal> SCPPrevOil { get; set; }
        public Nullable<decimal> SCPPrevCondensate { get; set; }
        public Nullable<decimal> SCPPrevAssociated { get; set; }
        public Nullable<decimal> SCPPrevNonAssociated { get; set; }
        public Nullable<decimal> GCPOil { get; set; }
        public Nullable<decimal> GCPCondensate { get; set; }
        public Nullable<decimal> GCPAssociated { get; set; }
        public Nullable<decimal> GCPNonAssociated { get; set; }
        public Nullable<decimal> SCPOil { get; set; }
        public Nullable<decimal> SCPCondensate { get; set; }
        public Nullable<decimal> SCPAssociated { get; set; }
        public Nullable<decimal> SCPNonAssociated { get; set; }
    }
    public class ESDCVolumetricExcelDto
    {
        public string xStructureID { get; set; }
        public string ESDCProjectID { get; set; }
        public string xAssetID { get; set; }
        public string ESDCProjectName { get; set; }
        public string UncertaintyLevel { get; set; }
        public Nullable<decimal> GRRPrevOil { get; set; }
        public Nullable<decimal> GRRPrevCondensate { get; set; }
        public Nullable<decimal> GRRPrevAssociated { get; set; }
        public Nullable<decimal> GRRPrevNonAssociated { get; set; }
        public Nullable<decimal> ReservesPrevOil { get; set; }
        public Nullable<decimal> ReservesPrevCondensate { get; set; }
        public Nullable<decimal> ReservesPrevAssociated { get; set; }
        public Nullable<decimal> ReservesPrevNonAssociated { get; set; }
        public Nullable<decimal> GOIOil { get; set; }
        public Nullable<decimal> GOICondensate { get; set; }
        public Nullable<decimal> GOIAssociated { get; set; }
        public Nullable<decimal> GOINonAssociated { get; set; }
        public Nullable<decimal> ReservesOil { get; set; }
        public Nullable<decimal> ReservesCondensate { get; set; }
        public Nullable<decimal> ReservesAssociated { get; set; }
        public Nullable<decimal> ReservesNonAssociated { get; set; }
        public string Remarks { get; set; }
    }
    public class ESDCForecastExcelDto
    {
        public string xStructureID { get; set; }
        public string ESDCProjectID { get; set; }
        public string xAssetID { get; set; }
        public string ESDCProjectName { get; set; }
        public int Year { get; set; }
        public Nullable<decimal> TPFOil { get; set; }
        public Nullable<decimal> TPFCondensate { get; set; }
        public Nullable<decimal> TPFAssociated { get; set; }
        public Nullable<decimal> TPFNonAssociated { get; set; }
        public Nullable<decimal> SFOil { get; set; }
        public Nullable<decimal> SFCondensate { get; set; }
        public Nullable<decimal> SFAssociated { get; set; }
        public Nullable<decimal> SFNonAssociated { get; set; }
        public Nullable<decimal> CIOOil { get; set; }
        public Nullable<decimal> CIOCondensate { get; set; }
        public Nullable<decimal> CIOAssociated { get; set; }
        public Nullable<decimal> CIONonAssociated { get; set; }
        public Nullable<decimal> LPOil { get; set; }
        public Nullable<decimal> LPCondensate { get; set; }
        public Nullable<decimal> LPAssociated { get; set; }
        public Nullable<decimal> LPNonAssociated { get; set; }
        public Nullable<decimal> AverageGrossHeat { get; set; }
        public string Remarks { get; set; }
    }
    public class ESDCDiscrepancyExcelDto
    {
        public string xStructureID { get; set; }
        public string ESDCProjectID { get; set; }
        public string xAssetID { get; set; }
        public string ESDCProjectName { get; set; }
        public string UncertaintyLevel { get; set; }
        public Nullable<decimal> CFUMOil { get; set; }
        public Nullable<decimal> CFUMCondensate { get; set; }
        public Nullable<decimal> CFUMAssociated { get; set; }
        public Nullable<decimal> CFUMNonAssociated { get; set; }
        public Nullable<decimal> CFPPAOil { get; set; }
        public Nullable<decimal> CFPPACondensate { get; set; }
        public Nullable<decimal> CFPPAAssociated { get; set; }
        public Nullable<decimal> CFPPANonAssociated { get; set; }
        public Nullable<decimal> CFWIOil { get; set; }
        public Nullable<decimal> CFWICondensate { get; set; }
        public Nullable<decimal> CFWIAssociated { get; set; }
        public Nullable<decimal> CFWINonAssociated { get; set; }
        public Nullable<decimal> CFCOil { get; set; }
        public Nullable<decimal> CFCCondensate { get; set; }
        public Nullable<decimal> CFCAssociated { get; set; }
        public Nullable<decimal> CFCNonAssociated { get; set; }
        public Nullable<decimal> UCOil { get; set; }
        public Nullable<decimal> UCCondensate { get; set; }
        public Nullable<decimal> UCAssociated { get; set; }
        public Nullable<decimal> UCNonAssociated { get; set; }
        public Nullable<decimal> CIOOil { get; set; }
        public Nullable<decimal> CIOCondensate { get; set; }
        public Nullable<decimal> CIOAssociated { get; set; }
        public Nullable<decimal> CIONonAssociated { get; set; }
    }
    public class ESDCInPlaceExcelDto
    {
        public string xStructureID { get; set; }
        public string ESDCFieldID { get; set; }
        public string xAssetID { get; set; }
        public string xStructureName { get; set; }
        public string UncertaintyLevel { get; set; }
        public Nullable<decimal> P90IOIP { get; set; }
        public Nullable<decimal> P90IGIP { get; set; }
    }
}
