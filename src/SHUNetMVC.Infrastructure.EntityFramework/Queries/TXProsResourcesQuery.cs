using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXProsResourcesQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select pr.xStructureID,
	               es.xStructureName,
	               pr.P90RROil,
	               pr.P90RROilUoM,
                   pr.P50RROil,
                   pr.P50RROilUoM,
                   pr.PMeanRROil,
                   pr.PMeanRROilUoM,
	               pr.P10RROil,
                   pr.P10RROilUoM,
                   pr.P90RRGas,
                   pr.P90RRGasUoM,
                   pr.P50RRGas,
                   pr.P50RRGasUoM,
                   pr.PMeanRRGas,
                   pr.PMeanRRGasUoM,
                   pr.P10RRGas,
                   pr.P10RRGasUoM,
                   pr.P90RRTotal,
                   pr.P90RRTotalUoM,
                   pr.P50RRTotal,
                   pr.P50RRTotalUoM,
                   pr.PMeanRRTotal,
                   pr.PMeanRRTotalUoM,
                   pr.P10RRTotal,
                   pr.P10RRTotalUoM,
                   pr.ExpectedPG,
                   pr.CurrentPG,
                   pr.CreatedDate,
                   pr.CreatedBy
            from xplore.TX_ProsResources pr 
            join dbo.MD_ExplorationStructure es
            on pr.xStructureID = es.xStructureID";

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
            select count(1) from xplore.TX_ProsResources pr join dbo.MD_ExplorationStructure es on pr.xStructureID = es.xStructureID";
        public override string GenerateID => @"SELECT COUNT(*) FROM xplore.TX_ProsResources pr";

        public override string LookupTextQuery => @"select es.xStructureName from xplore.TX_ProsResources pr join dbo.MD_ExplorationStructure es on pr.xStructureID = es.xStructureID";
        public override string LookupListTextQuery => @"
            select pr.xStructureID,
	               es.xStructureName,
	               pr.P90RROil,
	               pr.P90RROilUoM,
                   pr.P50RROil,
                   pr.P50RROilUoM,
                   pr.PMeanRROil,
                   pr.PMeanRROilUoM,
	               pr.P10RROil,
                   pr.P10RROilUoM,
                   pr.P90RRGas,
                   pr.P90RRGasUoM,
                   pr.P50RRGas,
                   pr.P50RRGasUoM,
                   pr.PMeanRRGas,
                   pr.PMeanRRGasUoM,
                   pr.P10RRGas,
                   pr.P10RRGasUoM,
                   pr.P90RRTotal,
                   pr.P90RRTotalUoM,
                   pr.P50RRTotal,
                   pr.P50RRTotalUoM,
                   pr.PMeanRRTotal,
                   pr.PMeanRRTotalUoM,
                   pr.P10RRTotal,
                   pr.P10RRTotalUoM,
                   pr.ExpectedPG,
                   pr.CurrentPG,
                   pr.CreatedDate,
                   pr.CreatedBy
            from xplore.TX_ProsResources pr 
            join dbo.MD_ExplorationStructure es
            on pr.xStructureID = es.xStructureID
            where pr.xStructureID = '{0}'";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => @"
            select pr.xStructureID,
	               es.xStructureName,
                   pr.P90InPlaceOilPR,
                   pr.P90InPlaceOilPRUoM,
                   pr.P50InPlaceOilPR,
                   pr.P50InPlaceOilPRUoM,
                   pr.PMeanInPlaceOilPR,
                   pr.PMeanInPlaceOilPRUoM,
                   pr.P10InPlaceOilPR,
                   pr.P10InPlaceOilPRUoM,
                   pr.P90InPlaceGasPR,
                   pr.P90InPlaceGasPRUoM,
                   pr.P50InPlaceGasPR,
                   pr.P50InPlaceGasPRUoM,
                   pr.PMeanInPlaceGasPR,
                   pr.PMeanInPlaceGasPRUoM,
                   pr.P10InPlaceGasPR,
                   pr.P10InPlaceGasPRUoM,
                   pr.P90InPlaceTotalPR,
                   pr.P90InPlaceTotalPRUoM,
                   pr.P50InPlaceTotalPR,
                   pr.P50InPlaceTotalPRUoM,
                   pr.PMeanInPlaceTotalPR,
                   pr.PMeanInPlaceTotalPRUoM,
                   pr.P10InPlaceTotalPR,
                   pr.P10InPlaceTotalPRUoM,
                   pr.RFOilPR,
                   pr.RFGasPR,
	               pr.P90RROil,
	               pr.P90RROilUoM,
                   pr.P50RROil,
                   pr.P50RROilUoM,
                   pr.PMeanRROil,
                   pr.PMeanRROilUoM,
	               pr.P10RROil,
                   pr.P10RROilUoM,
                   pr.P90RRGas,
                   pr.P90RRGasUoM,
                   pr.P50RRGas,
                   pr.P50RRGasUoM,
                   pr.PMeanRRGas,
                   pr.PMeanRRGasUoM,
                   pr.P10RRGas,
                   pr.P10RRGasUoM,
                   pr.P90RRTotal,
                   pr.P90RRTotalUoM,
                   pr.P50RRTotal,
                   pr.P50RRTotalUoM,
                   pr.PMeanRRTotal,
                   pr.PMeanRRTotalUoM,
                   pr.P10RRTotal,
                   pr.P10RRTotalUoM,
                   pr.HydrocarbonTypePRParID,
                   pr.GCFSRPR,
                   pr.GCFSRPRUoM,
                   pr.GCFTMPR,
                   pr.GCFTMPRUoM,
                   pr.GCFReservoirPR,
                   pr.GCFReservoirPRUoM,
                   pr.GCFClosurePR,
                   pr.GCFClosurePRUoM,
                   pr.GCFContainmentPR,
                   pr.GCFContainmentPRUoM,
                   pr.GCFPGTotalPR,
                   pr.GCFPGTotalPRUoM,
                   pr.ExpectedPG,
                   pr.CurrentPG,
                   pr.CreatedDate
            from xplore.TX_ProsResources pr 
            join dbo.MD_ExplorationStructure es
            on pr.xStructureID = es.xStructureID
            where pr.xStructureID = '{0}'";
    }
}
