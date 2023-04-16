using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class ExportExcelESDCQuery
    {
        public string ESDCGeneralInfoExport => @"
            SELECT  esdc.xStructureID,
					esdc.ESDCProjectID,
					esdc.ESDCProjectName,
					esdc.ESDCFieldID,
					(select plm.ParamValue1Text from dbo.MD_ParamaterList plm where plm.ParamListID = 'Expcel-1') + ' ' +  es.RegionalID as RegionalID,
					es.xStructureName,
					CASE
						WHEN es.xStructureStatusParID = 4 THEN 'UD Type'
						ELSE 'exploration structure'
					END AS ProjectLevel
				FROM xplore.TX_ESDC esdc
				LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
				LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
				LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
				LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
				LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
				WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ESDCGeneralInfoExportCount => @"
			SELECT  COUNT(*)
				FROM xplore.TX_ESDC esdc
				LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
				LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
				LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
				LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
				LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
				WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ESDCProdExport => @"
			SELECT  es.xStructureID,
					esdc.ESDCProjectID,
					es.xAssetID,
					esdc.ESDCProjectName,
					COALESCE(GCPPrevOil,0) as GCPPrevOil,
					COALESCE(GCPPrevCondensate,0) as GCPPrevCondensate,
					COALESCE(GCPPrevAssociated,0) as GCPPrevAssociated,
					COALESCE(GCPPrevNonAssociated,0) as GCPPrevNonAssociated,
					COALESCE(SCPPrevOil,0) as SCPPrevOil,
					COALESCE(SCPPrevCondensate,0) as SCPPrevCondensate,
					COALESCE(SCPPrevAssociated,0) as SCPPrevAssociated,
					COALESCE(SCPPrevNonAssociated,0) as SCPPrevNonAssociated,
					COALESCE(GCPOil,0) as GCPOil,
					COALESCE(GCPCondensate,0) as GCPCondensate,
					COALESCE(GCPAssociated,0) as GCPAssociated,
					COALESCE(GCPNonAssociated,0) as GCPNonAssociated,
					COALESCE(SCPOil,0) as SCPOil,
					COALESCE(SCPCondensate,0) as SCPCondensate,
					COALESCE(SCPAssociated,0) as SCPAssociated,
					COALESCE(SCPNonAssociated,0) as SCPNonAssociated
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCProd prd on esdc.xStructureID = prd.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ESDCProdExportCount => @"
			SELECT COUNT(*)
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCProd prd on esdc.xStructureID = prd.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ESDCVolumetricExport => @"
			SELECT  es.xStructureID,
					esdc.ESDCProjectID,
					es.xAssetID,
					esdc.ESDCProjectName,
					UncertaintyLevel,
					GRRPrevOil,
					GRRPrevCondensate,
					GRRPrevAssociated,
					GRRPrevNonAssociated,
					ReservesPrevOil,
					ReservesPrevCondensate,
					ReservesPrevAssociated,
					ReservesPrevNonAssociated,
					GOIOil,
					GOICondensate,
					GOIAssociated,
					GOINonAssociated,
					ReservesOil,
					ReservesCondensate,
					ReservesAssociated,
					ReservesNonAssociated,
					COALESCE(Remarks, '') as Remarks,
					CASE
						WHEN UncertaintyLevel like '%Low%' THEN 1
						WHEN UncertaintyLevel like '%Middle%' THEN 2
						ELSE 3
					END AS row_numberUncertainty
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCVolumetric vol on esdc.xStructureID = vol.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus' AND vol.UncertaintyLevel != 'P-Mean'";

		public string ESDCVolumetricExportCount => @"
			SELECT COUNT(*)
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCVolumetric vol on esdc.xStructureID = vol.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus' AND vol.UncertaintyLevel != 'P-Mean'";

		public string ESDCForecastExport => @"
			SELECT  es.xStructureID,
					esdc.ESDCProjectID,
					es.xAssetID,
					esdc.ESDCProjectName,
					COALESCE(frc.Year,0) as Year,
					COALESCE(TPFOil,0) as TPFOil,
					COALESCE(TPFCondensate,0) as TPFCondensate,
					COALESCE(TPFAssociated,0) as TPFAssociated,
					COALESCE(TPFNonAssociated,0) as TPFNonAssociated,
					COALESCE(SFOil,0) as SFOil,
					COALESCE(SFCondensate,0) as SFCondensate,
					COALESCE(SFAssociated,0) as SFAssociated,
					COALESCE(SFNonAssociated,0) as SFNonAssociated,
					COALESCE(CIOOil,0) as CIOOil,
					COALESCE(CIOCondensate,0) as CIOCondensate,
					COALESCE(CIOAssociated,0) as CIOAssociated,
					COALESCE(CIONonAssociated,0) as CIONonAssociated,
					COALESCE(LPOil,0) as LPOil,
					COALESCE(LPCondensate,0) as LPCondensate,
					COALESCE(LPAssociated,0) as LPAssociated,
					COALESCE(LPNonAssociated,0) as LPNonAssociated,
					COALESCE(AverageGrossHeat,0) as AverageGrossHeat,
					COALESCE(Remarks, '') as Remarks
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCForecast frc on esdc.xStructureID = frc.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ESDCForecastExportCount => @"
				SELECT COUNT(*)
				FROM xplore.TX_ESDC esdc
				LEFT JOIN xplore.TX_ESDCForecast frc on esdc.xStructureID = frc.xStructureID
				LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID
				LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
				LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
				LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
				LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
				WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ESDCDiscrepancyExport => @"
			SELECT  es.xStructureID,
					esdc.ESDCProjectID,
					es.xAssetID,
					esdc.ESDCProjectName,
					UncertaintyLevel,
					CFUMOil,
					CFUMCondensate,
					CFUMAssociated,
					CFUMNonAssociated,
					CFPPAOil,
					CFPPACondensate,
					CFPPAAssociated,
					CFPPANonAssociated,
					CFWIOil,
					CFWICondensate,
					CFWIAssociated,
					CFWINonAssociated,
					CFCOil,
					CFCCondensate,
					CFCAssociated,
					CFCNonAssociated,
					UCOil,
					UCCondensate,
					UCAssociated,
					UCNonAssociated,
					CIOOil,
					CIOCondensate,
					CIOAssociated,
					CIONonAssociated,
					CASE
						WHEN UncertaintyLevel like '%Low%' THEN 1
						WHEN UncertaintyLevel like '%Middle%' THEN 2
						ELSE 3
					END AS row_numberUncertainty
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCDiscrepancy dis on esdc.xStructureID = dis.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus' AND dis.UncertaintyLevel != 'P-Mean'";

		public string ESDCDiscrepancyExportCount => @"
				SELECT COUNT(*)
				FROM xplore.TX_ESDC esdc
				LEFT JOIN xplore.TX_ESDCDiscrepancy dis on esdc.xStructureID = dis.xStructureID
				LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID
				LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
				LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
				LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
				LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
				WHERE pl.ParamID = 'ExplorationStructureStatus' AND dis.UncertaintyLevel != 'P-Mean'";

		public string ESDCInPlaceExport => @"
			SELECT  es.xStructureID,
					esdc.ESDCFieldID,
					es.xAssetID,
					es.xStructureName,
					UncertaintyLevel,
					P90IOIP,
					P90IGIP,
					CASE
						WHEN UncertaintyLevel like '%Low%' THEN 1
						WHEN UncertaintyLevel like '%Middle%' THEN 2
						ELSE 3
					END AS row_numberUncertainty
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCInPlace ipl on esdc.xStructureID = ipl.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus' AND ipl.UncertaintyLevel != 'P-Mean'";

		public string ESDCInPlaceExportCount => @"
			SELECT COUNT(*)
			FROM xplore.TX_ESDC esdc
			LEFT JOIN xplore.TX_ESDCInPlace ipl on esdc.xStructureID = ipl.xStructureID
			LEFT JOIN dbo.MD_ExplorationStructure es on esdc.xStructureID = es.xStructureID
			LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
			LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
			LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
			LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
			WHERE pl.ParamID = 'ExplorationStructureStatus' AND ipl.UncertaintyLevel != 'P-Mean'";
	}
}
