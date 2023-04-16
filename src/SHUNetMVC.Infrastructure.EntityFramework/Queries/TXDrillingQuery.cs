using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXDrillingQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT dr.xStructureID,
	               dr.xWellID,
                   ew.xWellName,
                   ew.DrillingLocation,
                   ew.RigTypeParID,
                   ew.WellTypeParID,
                   ew.BHLocationLatitude,
                   ew.BHLocationLongitude,
                   dr.RKAPFiscalYear,
                   dr.WaterDepthFeet,
                   dr.WaterDepthMeter,
                   dr.TotalDepthFeet,
                   dr.TotalDepthMeter,
                   dr.SurfaceLocationLatitude,
                   dr.SurfaceLocationLongitude,
                   dr.DrillingCost,
                   dr.DrillingCostCurr,
                   dr.ExpectedDrillingDate,
                   dr.P90ResourceOil,
                   dr.P90ResourceOilUoM,
                   dr.P50ResourceOil,
                   dr.P50ResourceOilUoM,
                   dr.P10ResourceOil,
                   dr.P10ResourceOilUoM,
                   dr.P90ResourceGas,
                   dr.P90ResourceGasUoM,
                   dr.P50ResourceGas,
                   dr.P50ResourceGasUoM,
                   dr.P10ResourceGas,
                   dr.P10ResourceGasUoM,
                   dr.CurrentPG,
                   dr.ExpectedPG,
                   dr.ChanceComponentSource,
                   dr.ChanceComponentTiming,
                   dr.ChanceComponentReservoir,
                   dr.ChanceComponentClosure,
                   dr.ChanceComponentContainment,
                   dr.P90NPVProfitabilityOil,
                   dr.P90NPVProfitabilityOilCurr,
                   dr.P50NPVProfitabilityOil,
                   dr.P50NPVProfitabilityOilCurr,
                   dr.P10NPVProfitabilityOil,
                   dr.P10NPVProfitabilityOilCurr,
                   dr.P90NPVProfitabilityGas,
                   dr.P90NPVProfitabilityGasCurr,
                   dr.P50NPVProfitabilityGas,
                   dr.P50NPVProfitabilityGasCurr,
                   dr.P10NPVProfitabilityGas,
                   dr.P10NPVProfitabilityGasCurr,
                   dr.CreatedDate,
                   dr.CreatedBy
              FROM xplore.TX_Drilling dr
              JOIN dbo.MD_ExplorationWell ew
              ON dr.xWellID = ew.xWellID";

        public override string PagedRoles => @"
            SELECT  s.xStructureID,
                    s.xStructureName,
                    s.xStructureStatusParID,
                    s.SingleOrMultiParID,
                    s.ExplorationTypeParID,
                    pl.ParamValue1Text,
                    ba.BasinID,
                    ba.BasinName,
                    s.RegionalID,
                    s.ZonaID,
                    s.APHID,
                    a.xAssetID,
                    a.xAssetName,
                    bl.xBlockID,
                    bl.xBlockName,
                    s.UDClassificationParID,
                    s.UDSubClassificationParID,
                    s.UDSubTypeParID,
                    s.ExplorationAreaParID,
                    s.CountriesID,
                    s.Play,
                    s.StatusData,
                    s.CreatedDate,
                    s.CreatedBy
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus' AND s.StatusData = 'Draft'";

        public override string CountQuery => @"
            select count(1) FROM xplore.TX_Drilling dr JOIN dbo.MD_ExplorationWell ew ON dr.xWellID = ew.xWellID";

        public override string GenerateID => @"SELECT COUNT(*) FROM xplore.TX_Drilling dr";

        public override string LookupTextQuery => @"select ew.xWellName FROM xplore.TX_Drilling dr JOIN dbo.MD_ExplorationWell ew ON dr.xWellID = ew.xWellID";
        public override string LookupListTextQuery => @"
            SELECT dr.xStructureID,
	               dr.xWellID,
                   ew.xWellName,
                   ew.DrillingLocation,
                   ew.RigTypeParID,
                   ew.WellTypeParID,
                   ew.BHLocationLatitude,
                   ew.BHLocationLongitude,
                   dr.RKAPFiscalYear,
                   dr.PlayOpener,
                   dr.DrillingCompletionPeriod,
                   dr.Location,
                   dr.WaterDepthFeet,
                   dr.WaterDepthMeter,
                   dr.TotalDepthFeet,
                   dr.TotalDepthMeter,
                   dr.SurfaceLocationLatitude,
                   dr.SurfaceLocationLongitude,
                   dr.DrillingCost,
                   dr.DrillingCostCurr,
                   dr.ExpectedDrillingDate,
                   dr.CommitmentWell,
                   dr.OperationalContextParId,
                   dr.PotentialDelay,
                   dr.NetRevenueInterest,
                   dr.DrillingCostDHB,
                   dr.DrillingCostDHBCurr,
                   dr.P90ResourceOil,
                   dr.P90ResourceOilUoM,
                   dr.P50ResourceOil,
                   dr.P50ResourceOilUoM,
                   dr.P10ResourceOil,
                   dr.P10ResourceOilUoM,
                   dr.P90ResourceGas,
                   dr.P90ResourceGasUoM,
                   dr.P50ResourceGas,
                   dr.P50ResourceGasUoM,
                   dr.P10ResourceGas,
                   dr.P10ResourceGasUoM,
                   dr.CurrentPG,
                   dr.ExpectedPG,
                   dr.ChanceComponentSource,
                   dr.ChanceComponentTiming,
                   dr.ChanceComponentReservoir,
                   dr.ChanceComponentClosure,
                   dr.ChanceComponentContainment,
                   dr.P90NPVProfitabilityOil,
                   dr.P90NPVProfitabilityOilCurr,
                   dr.P50NPVProfitabilityOil,
                   dr.P50NPVProfitabilityOilCurr,
                   dr.P10NPVProfitabilityOil,
                   dr.P10NPVProfitabilityOilCurr,
                   dr.P90NPVProfitabilityGas,
                   dr.P90NPVProfitabilityGasCurr,
                   dr.P50NPVProfitabilityGas,
                   dr.P50NPVProfitabilityGasCurr,
                   dr.P10NPVProfitabilityGas,
                   dr.P10NPVProfitabilityGasCurr,
                   dr.CreatedDate,
                   dr.CreatedBy
              FROM xplore.TX_Drilling dr
              LEFT JOIN dbo.MD_ExplorationWell ew
              ON dr.xWellID = ew.xWellID
              where dr.xStructureID = '{0}'";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => @"
            SELECT dr.xStructureID,
	               dr.xWellID,
                   ew.xWellName,
                   ew.DrillingLocation,
                   ew.RigTypeParID,
                   ew.WellTypeParID,
                   ew.BHLocationLatitude,
                   ew.BHLocationLongitude,
                   dr.RKAPFiscalYear,
                   dr.PlayOpener,
                   dr.DrillingCompletionPeriod,
                   dr.Location,
                   dr.WaterDepthFeet,
                   dr.WaterDepthMeter,
                   dr.TotalDepthFeet,
                   dr.TotalDepthMeter,
                   dr.SurfaceLocationLatitude,
                   dr.SurfaceLocationLongitude,
                   dr.CommitmentWell,
                   dr.OperationalContextParId,
                   dr.PotentialDelay,
                   dr.NetRevenueInterest,
                   dr.DrillingCostDHB,
                   dr.DrillingCostDHBCurr,
                   dr.DrillingCost,
                   dr.DrillingCostCurr,
                   dr.ExpectedDrillingDate,
                   dr.P90ResourceOil,
                   dr.P90ResourceOilUoM,
                   dr.P50ResourceOil,
                   dr.P50ResourceOilUoM,
                   dr.P10ResourceOil,
                   dr.P10ResourceOilUoM,
                   dr.P90ResourceGas,
                   dr.P90ResourceGasUoM,
                   dr.P50ResourceGas,
                   dr.P50ResourceGasUoM,
                   dr.P10ResourceGas,
                   dr.P10ResourceGasUoM,
                   dr.CurrentPG,
                   dr.ExpectedPG,
                   dr.ChanceComponentSource,
                   dr.ChanceComponentTiming,
                   dr.ChanceComponentReservoir,
                   dr.ChanceComponentClosure,
                   dr.ChanceComponentContainment,
                   dr.P90NPVProfitabilityOil,
                   dr.P90NPVProfitabilityOilCurr,
                   dr.P50NPVProfitabilityOil,
                   dr.P50NPVProfitabilityOilCurr,
                   dr.P10NPVProfitabilityOil,
                   dr.P10NPVProfitabilityOilCurr,
                   dr.P90NPVProfitabilityGas,
                   dr.P90NPVProfitabilityGasCurr,
                   dr.P50NPVProfitabilityGas,
                   dr.P50NPVProfitabilityGasCurr,
                   dr.P10NPVProfitabilityGas,
                   dr.P10NPVProfitabilityGasCurr,
                   dr.CreatedDate
              FROM xplore.TX_Drilling dr
              LEFT JOIN dbo.MD_ExplorationWell ew
              ON dr.xWellID = ew.xWellID
              where dr.xStructureID = '{0}'";
    }
}
