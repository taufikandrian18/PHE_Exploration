using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class ExportExcelExplorationQuery
    {
		public string ExploreProsResourceExcel => @"
				SELECT  es.[xStructureID]
						,(select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationStructureStatus' and ParamListID = [xStructureStatusParID]) as ParamValue1Text
						,[xStructureName]
						,[Play] as Play
						, (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [RegionalID]) as RegionalID
						,bl.xBlockName as xBlockName
						, (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [ZonaID]) as ZonaID
						,COALESCE((select bp.PI from dbo.MD_ExplorationBlockPartner bp where bp.xBlockID = es.xBlockID and bp.PartnerName like '%Pertamina%'), 0) as PI
						,COALESCE(pt.GCFSRPR,0) as GCFSRPR
						,COALESCE(pt.GCFTMPR, 0) as GCFTMPR
						,COALESCE(pt.GCFReservoirPR, 0) as GCFReservoirPR
						,COALESCE(pt.GCFClosurePR, 0) as GCFClosurePR
						,COALESCE(pt.GCFContainmentPR, 0) as GCFContainmentPR
						,COALESCE(pt.GCFPGTotalPR,0) as GCFPGTotalPR
						,COALESCE(pt.P90InPlaceOilPR, 0) as P90InPlaceOilPR
						,COALESCE(pt.P50InPlaceOilPR, 0) as P50InPlaceOilPR
						,COALESCE(pt.PMeanInPlaceOilPR, 0) as PMeanInPlaceOilPR
						,COALESCE(pt.P10InPlaceOilPR, 0) as P10InPlaceOilPR
						,COALESCE(pt.RFOilPR, 0) as RFOilPR
						,COALESCE(pt.P90RROil, 0) as P90RROil
						,COALESCE(pt.P50RROil,0) as P50RROil
						,COALESCE(pt.PMeanRROil, 0) as PMeanRROil
						,COALESCE(pt.P10RROil, 0) as P10RROil
						,COALESCE(pt.P90InPlaceGasPR, 0) as P90InPlaceGasPR
						,COALESCE(pt.P50InPlaceGasPR, 0) as P50InPlaceGasPR
						,COALESCE(pt.PMeanInPlaceGasPR, 0) as PMeanInPlaceGasPR
						,COALESCE(pt.P10InPlaceGasPR, 0) as P10InPlaceGasPR
						,COALESCE(pt.RFGasPR, 0) as RFGasPR
						,COALESCE(pt.P90RRGas, 0) as P90RRGas
						,COALESCE(pt.P50RRGas, 0) as P50RRGas
						,COALESCE(pt.PMeanRRGas, 0) as PMeanRRGas
						,COALESCE(pt.P10RRGas, 0) as P10RRGas
						FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
						LEFT JOIN [DB_PHE_Exploration].[dbo].[MD_ParamaterList] pl on es.xStructureStatusParID = pl.ParamListID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
						LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_ProsResources] pt on es.xStructureID = pt.xStructureID
						WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ExploreProsResourceExcelCount => @"
				SELECT  COUNT(*)
						FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
						LEFT JOIN [DB_PHE_Exploration].[dbo].[MD_ParamaterList] pl on es.xStructureStatusParID = pl.ParamListID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
						LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_ProsResources] pt on es.xStructureID = pt.xStructureID
						WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ExploreRJPPExcel => @"
				SELECT  es.[xStructureID]
				,COALESCE((select wl.xWellName from dbo.MD_ExplorationWell wl where wl.xWellID = dr.xWellID), '') as xWellName
				,CASE
					WHEN dr.PlayOpener = 1 THEN 'Yes'
					WHEN dr.PlayOpener = 0 THEN 'No'
					ELSE ''
				END as PlayOpener
				,COALESCE(dr.DrillingCompletionPeriod, 0) as DrillingCompletionPeriod
				,COALESCE(dr.Location,'') as Location
				,dr.ExpectedDrillingDate as ExpectedDrillingDate
				,COALESCE((select eid.EntityName from dbo.vw_DIM_Entity eid where eid.EntityID = es.APHID), '') as APHName
				,ass.xAssetName as xAssetName
				,[Play] as Play
				,(select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationStructureStatus' and ParamListID = [xStructureStatusParID]) as ParamValue1Text
				,COALESCE((select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'UDClassification' and ParamListID = [UDClassificationParID]), '') as ParamValue1TextUDClass
				,(select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SingleOrMulti' and ParamListID = [SingleOrMultiParID]) as ParamValue1TextSingleMulti
				,COALESCE((select wl.[WellTypeParID] from dbo.MD_ExplorationWell wl where wl.xWellID = dr.xWellID), '') as WellTypeParID
				,(select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'Operators' and ParamListID = bl.[OperatorshipStatusParID]) as ParamValue1TextOperatorStatus
				,CASE
					WHEN dr.CommitmentWell = 1 THEN 'Yes'
					WHEN dr.CommitmentWell = 0 THEN 'No'
					ELSE ''
				END as CommitmentWell
				,COALESCE((select wl.[DrillingLocation] from dbo.MD_ExplorationWell wl where wl.xWellID = dr.xWellID), '') as DrillingLocation
				,COALESCE(dr.OperationalContextParId,'') as OperationalContext
				,COALESCE(dr.[TotalDepthMeter], 0) as TotalDepthMeter
				,CASE
					WHEN dr.PotentialDelay = 1 THEN 'Yes'
					WHEN dr.PotentialDelay = 0 THEN 'No'
					ELSE ''
				END as PotentialDelay
				,COALESCE((select bp.PI from dbo.MD_ExplorationBlockPartner bp where es.xBlockID = bp.xBlockID and bp.PartnerName like '%Pertamina%'), 0) as PartnerName
				,COALESCE(dr.NetRevenueInterest, 0) as NetRevenueInterest
				,COALESCE([P90ResourceOil], 0) as P90ResourceOil
				,COALESCE([P50ResourceOil], 0) as P50ResourceOil
				,COALESCE([P10ResourceOil], 0) as P10ResourceOil
				,COALESCE([P90ResourceGas], 0) as P90ResourceGas
				,COALESCE([P50ResourceGas], 0) as P50ResourceGas
				,COALESCE([P10ResourceGas], 0) as P10ResourceGas
				,COALESCE([CurrentPG], 0) as CurrentPG
				,COALESCE([ExpectedPG], 0) as ExpectedPG
				,COALESCE([ChanceComponentSource], 0) as ChanceComponentSource
				,COALESCE([ChanceComponentTiming], 0) as ChanceComponentTiming
				,COALESCE([ChanceComponentReservoir], 0) as ChanceComponentReservoir
				,COALESCE([ChanceComponentClosure], 0) as ChanceComponentClosure
				,COALESCE([ChanceComponentContainment], 0) as ChanceComponentContainment
				,COALESCE([DrillingCostDHB], 0) as DrillingCostDHB
				,COALESCE([DrillingCost], 0) as DrillingCost
				,COALESCE([P90NPVProfitabilityOil], 0) as P90NPVProfitabilityOil
				,COALESCE([P50NPVProfitabilityOil], 0) as P50NPVProfitabilityOil
				,COALESCE([P10NPVProfitabilityOil], 0) as P10NPVProfitabilityOil
				,COALESCE([P90NPVProfitabilityGas], 0) as P90NPVProfitabilityGas
				,COALESCE([P50NPVProfitabilityGas], 0) as P50NPVProfitabilityGas
				,COALESCE([P10NPVProfitabilityGas], 0) as P10NPVProfitabilityGas
				FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
				LEFT JOIN [DB_PHE_Exploration].[dbo].[MD_ParamaterList] pl on es.xStructureStatusParID = pl.ParamListID
				JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
				JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
				JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
				JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
				LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_Drilling] dr on es.xStructureID = dr.xStructureID
				WHERE pl.ParamID = 'ExplorationStructureStatus'";

		public string ExploreRJPPExcelCount => @"
						SELECT  count(*)
						FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
						LEFT JOIN [DB_PHE_Exploration].[dbo].[MD_ParamaterList] pl on es.xStructureStatusParID = pl.ParamListID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
						JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
						LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_Drilling] dr on es.xStructureID = dr.xStructureID
						WHERE pl.ParamID = 'ExplorationStructureStatus'";
	}
}
