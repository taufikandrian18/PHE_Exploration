using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class MDExplorationStructureQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT  s.xStructureID,
                    s.xStructureName,
                    s.xStructureStatusParID,
                    s.SingleOrMultiParID,
                    s.ExplorationTypeParID,
                    pl.ParamValue1Text,
                    ba.BasinID,
                    ba.BasinName,
                    s.RegionalID,
                    (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.RegionalID) as RegionalName,
                    s.ZonaID,
					(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.ZonaID) as ZonaName,
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
                    s.CreatedBy,
                    s.UpdatedDate,
					s.UpdatedBy,
                    s.MadamTransID
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus' and s.StatusData in ('Draft','Released','Reject Submitted','Reject Revision','Submitted','Revision')";

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
                    (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.RegionalID) as RegionalName,
                    s.ZonaID,
					(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.ZonaID) as ZonaName,
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
                    s.CreatedBy,
                    s.UpdatedDate,
					s.UpdatedBy,
                    s.MadamTransID
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus' AND s.StatusData IN ('Approved','Submitted','Revision')";

        public string CountQueryRoles => @"SELECT count(*)
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus' AND s.StatusData IN ('Approved','Submitted','Revision')";

        public string CountQueryReport => @"SELECT count(*)
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus'";

        public override string CountQuery => @"
            SELECT  count(*)
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus' and s.StatusData in ('Draft','Released','Reject Submitted','Reject Revision','Submitted')";

        public override string LookupTextQuery => @"select s.xStructureName from dbo.MD_ExplorationStructure s";
        public override string GenerateID => @"select TOP 1 xStructureID from dbo.MD_ExplorationStructure order by xStructureID desc";
        public override string LookupListTextQuery => @"
            SELECT  s.xStructureID,
                    s.xStructureName,
                    s.xStructureStatusParID,
                    s.SingleOrMultiParID,
                    s.ExplorationTypeParID,
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
                    s.CreatedBy,
                    s.MadamTransID
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                where s.xStructureID = {0}";

        public override string PagedReport => @"
            SELECT  s.xStructureID,
                    s.xStructureName,
                    s.xStructureStatusParID,
                    s.SingleOrMultiParID,
                    s.ExplorationTypeParID,
                    pl.ParamValue1Text,
                    ba.BasinID,
                    ba.BasinName,
                    s.RegionalID,
                    (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.RegionalID) as RegionalName,
                    s.ZonaID,
					(select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = s.ZonaID) as ZonaName,
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
                    s.CreatedBy,
                    s.UpdatedDate,
					s.UpdatedBy,
                    s.MadamTransID
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ParamaterList pl on s.xStructureStatusParID = pl.ParamListID
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                WHERE pl.ParamID = 'ExplorationStructureStatus'";

        public override string ExcelExportQuery => @"
            SELECT es.[xStructureID]
                  ,[xStructureName]
				  ,COALESCE(MAX(dr.RKAPFiscalYear), 0) as EffectiveYear
	              , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationStructureStatus' and ParamListID = [xStructureStatusParID]) as xStructureStatusName
	              , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SingleOrMulti' and ParamListID = [SingleOrMultiParID]) as SingleOrMultiName
	              , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationType' and ParamListID = [ExplorationTypeParID]) as ExplorationTypeName
	              , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [SubholdingID]) as SubholdingName
	              ,bs.BasinName
	              , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [RegionalID]) as RegionalName
	              , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [ZonaID]) as ZonaName
	              , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [APHID]) as APHName
	              ,ass.xAssetName
	              ,bl.xBlockName
	              ,ar.xAreaName
	              , COALESCE((select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'UDClassification' and ParamListID = [UDClassificationParID]), '') as UDClassificationName
	              , CASE
			            WHEN [UDSubClassificationParID] = '1' THEN 'K7A'
			            WHEN [UDSubClassificationParID] = '2' THEN 'K7B'
			            ELSE ''
		            END as UDSubClassificationName
	              , COALESCE((select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SubUDStatus' and ParamListID = [UDSubTypeParID]),'') as UDSubTypeName
                  ,[ExplorationAreaParID]
                  ,es.[CountriesID]
                  ,[Play]
                  ,[StatusData]
              FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
			  LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_Drilling] dr on es.xStructureID = dr.xStructureID
              WHERE es.xStructureID = '{0}'
			  GROUP BY es.[xStructureID]
                  ,[xStructureName]
                  ,[xStructureStatusParID]
				  ,[ExplorationTypeParID]
                  ,[SingleOrMultiParID]
                  ,[SubholdingID]
                  ,es.[BasinID]
	              ,bs.BasinName
                  ,[RegionalID]
                  ,[ZonaID]
                  ,[APHID]
                  ,es.[xAssetID]
	              ,ass.xAssetName
                  ,es.[xBlockID]
	              ,bl.xBlockName
                  ,es.[xAreaID]
	              ,ar.xAreaName
                  ,[UDClassificationParID]
                  ,[UDSubClassificationParID]
                  ,[UDSubTypeParID]
                  ,[ExplorationAreaParID]
                  ,es.[CountriesID]
                  ,[Play]
                  ,[StatusData]
                  ,es.[IsDeleted]
                  ,es.[CreatedDate]
                  ,es.[CreatedBy]";
    }
}
