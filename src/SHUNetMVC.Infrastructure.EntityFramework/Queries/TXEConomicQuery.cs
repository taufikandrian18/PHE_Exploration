using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXEConomicQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT  e.xStructureID,
                    e.DevConcept,
                    e.EconomicAssumption,
                    e.CAPEX,
                    e.CAPEXCurr,
                    e.OPEXProduction,
                    e.OPEXProductionCurr,
                    e.OPEXFacility,
                    e.OPEXFacilityCurr,
                    e.ASR,
                    e.ASRCurr,
                    e.EconomicResult,
                    e.ContractorNPV,
                    e.ContractorNPVCurr,
                    e.IRR,
                    e.ContractorPOT,
                    e.ContractorPOTUoM,
                    e.PIncome,
                    e.PIncomeCurr,
                    e.EMV,
                    e.EMVCurr,
                    e.NPV,
                    e.NPVCurr,
                    e.CreatedDate,
                    e.CreatedBy
            FROM xplore.TX_Economic e";

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
            select count(1) from xplore.TX_Economic e";
        public override string GenerateID => @"SELECT COUNT(*) FROM xplore.TX_Economic e";

        public override string LookupTextQuery => @"select e.DevConcept from xplore.TX_Economic e";
        public override string LookupListTextQuery => @"
            SELECT  e.xStructureID,
                    e.DevConcept,
                    e.EconomicAssumption,
                    e.CAPEX,
                    e.CAPEXCurr,
                    e.OPEXProduction,
                    e.OPEXProductionCurr,
                    e.OPEXFacility,
                    e.OPEXFacilityCurr,
                    e.ASR,
                    e.ASRCurr,
                    e.EconomicResult,
                    e.ContractorNPV,
                    e.ContractorNPVCurr,
                    e.IRR,
                    e.ContractorPOT,
                    e.ContractorPOTUoM,
                    e.PIncome,
                    e.PIncomeCurr,
                    e.EMV,
                    e.EMVCurr,
                    e.NPV,
                    e.NPVCurr,
                    e.CreatedDate,
                    e.CreatedBy
            FROM xplore.TX_Economic e
            where e.xStructureID = {0}";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => @"
                SELECT  e.xStructureID,
		                es.xBlockID,
                        bl.xBlockName,
		                bl.AwardDate,
		                bl.ExpiredDate,
		                COALESCE((select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'Operators' and ParamListID = bl.OperatorshipStatusParID),'') as OperatorStatusName,
		                bl.OperatorName,
                        e.DevConcept,
                        e.EconomicAssumption,
                        e.CAPEX,
                        e.CAPEXCurr,
                        e.OPEXProduction,
                        e.OPEXProductionCurr,
                        e.OPEXFacility,
                        e.OPEXFacilityCurr,
                        e.ASR,
                        e.ASRCurr,
                        e.EconomicResult,
                        e.ContractorNPV,
                        e.ContractorNPVCurr,
                        e.IRR,
                        e.ContractorPOT,
                        e.ContractorPOTUoM,
                        e.PIncome,
                        e.PIncomeCurr,
                        e.EMV,
                        e.EMVCurr,
                        e.NPV,
                        e.NPVCurr,
                        e.CreatedDate
                FROM xplore.TX_Economic e
                JOIN dbo.MD_ExplorationStructure es on e.xStructureID = es.xStructureID
                JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
                where e.xStructureID = '{0}'";
    }
}
