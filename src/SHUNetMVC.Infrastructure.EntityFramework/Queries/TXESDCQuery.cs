using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXESDCQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
           SELECT es.[xStructureID]
                    ,[xStructureName]
                    ,COALESCE(MAX(dr.RKAPFiscalYear), 0) as EffectiveYear
                    ,[xStructureStatusParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationStructureStatus' and ParamListID = [xStructureStatusParID]) as xStructureStatusName
                    ,[SingleOrMultiParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SingleOrMulti' and ParamListID = [SingleOrMultiParID]) as SingleOrMultiName
                    ,[ExplorationTypeParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationType' and ParamListID = [ExplorationTypeParID]) as ExplorationTypeName
                    ,[SubholdingID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [SubholdingID]) as SubholdingName
                    ,es.[BasinID]
                    ,bs.BasinName
                    ,[RegionalID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [RegionalID]) as RegionalName
                    ,[ZonaID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [ZonaID]) as ZonaName
                    ,[APHID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [APHID]) as APHName
                    ,es.[xAssetID]
                    ,ass.xAssetName
                    ,es.[xBlockID]
                    ,bl.xBlockName
                    ,es.[xAreaID]
                    ,ar.xAreaName
                    ,[UDClassificationParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'UDClassification' and ParamListID = [UDClassificationParID]) as UDClassificationName
                    ,[UDSubClassificationParID]
                    , CASE
	                    WHEN [UDSubClassificationParID] = '1' THEN 'K7A'
	                    WHEN [UDSubClassificationParID] = '2' THEN 'K7B'
	                    ELSE null
                    END as UDSubClassificationName
                    ,[UDSubTypeParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SubUDStatus' and ParamListID = [UDSubTypeParID]) as UDSubTypeName
                    ,[ExplorationAreaParID]
                    ,es.[CountriesID]
                    ,[Play]
                    ,es.[IsDeleted]
                    ,es.StatusData
                    ,es.[CreatedDate]
                    ,es.[CreatedBy]
                    ,esdc.xStructureID
                    FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
                    LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_Drilling] dr on es.xStructureID = dr.xStructureID
                    LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_ESDC] esdc on es.xStructureID = esdc.xStructureID
                    WHERE esdc.xStructureID is null
                    AND es.StatusData = 'Approved'
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
                    ,es.[IsDeleted]
                    ,es.StatusData
                    ,es.[CreatedDate]
                    ,es.[CreatedBy]
                    ,esdc.xStructureID";

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
            SELECT count(1)
              FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
              JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID ";

        public override string LookupTextQuery => @"
            select top 1 EntityLvl
	        from [DB_PHE_HRIS_DEV].[dbo].[DIM_OrgUnitHierarchy]
	        UNPIVOT (EntityLvl FOR HierLevel IN (Lvl3EntityID, Lvl2EntityID, Lvl1EntityID)) AS unpvt
	        where OrgUnitID = '{0}'
	        and LEFT(EntityLvl, 1) != 'O'
	        AND LEFT(EntityLvl, 4) != 'PDSI'
	        AND EntityLvl != 'SHUOLD'
	        AND EntityLvl is not null";

        public override string LookupListTextQuery => @"
            SELECT es.[xStructureID]
                    ,[xStructureName]
                    ,COALESCE(MAX(dr.RKAPFiscalYear), 0) as EffectiveYear
                    ,[xStructureStatusParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationStructureStatus' and ParamListID = [xStructureStatusParID]) as xStructureStatusName
                    ,[SingleOrMultiParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SingleOrMulti' and ParamListID = [SingleOrMultiParID]) as SingleOrMultiName
                    ,[ExplorationTypeParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'ExplorationType' and ParamListID = [ExplorationTypeParID]) as ExplorationTypeName
                    ,[SubholdingID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [SubholdingID]) as SubholdingName
                    ,es.[BasinID]
                    ,bs.BasinName
                    ,[RegionalID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [RegionalID]) as RegionalName
                    ,[ZonaID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [ZonaID]) as ZonaName
                    ,[APHID]
                    , (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = [APHID]) as APHName
                    ,es.[xAssetID]
                    ,ass.xAssetName
                    ,es.[xBlockID]
                    ,bl.xBlockName
                    ,es.[xAreaID]
                    ,ar.xAreaName
                    ,[UDClassificationParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'UDClassification' and ParamListID = [UDClassificationParID]) as UDClassificationName
                    ,[UDSubClassificationParID]
                    , CASE
	                    WHEN [UDSubClassificationParID] = '1' THEN 'K7A'
	                    WHEN [UDSubClassificationParID] = '2' THEN 'K7B'
	                    ELSE null
                    END as UDSubClassificationName
                    ,[UDSubTypeParID]
                    , (select pl.ParamValue1Text from dbo.MD_ParamaterList pl where pl.ParamID = 'SubUDStatus' and ParamListID = [UDSubTypeParID]) as UDSubTypeName
                    ,[ExplorationAreaParID]
                    ,es.[CountriesID]
                    ,[Play]
                    ,es.[IsDeleted]
                    ,es.StatusData
                    ,es.[CreatedDate]
                    ,es.[CreatedBy]
                    ,esdc.xStructureID
                    FROM [DB_PHE_Exploration].[dbo].[MD_ExplorationStructure] es
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBasin] bs on es.BasinID = bs.BasinID
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationAsset] ass on es.xAssetID = ass.xAssetID
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationBlock] bl on es.xBlockID = bl.xBlockID
                    JOIN [DB_PHE_Exploration].[dbo].[MD_ExplorationArea] ar on es.xAreaID = ar.xAreaID 
                    LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_Drilling] dr on es.xStructureID = dr.xStructureID
                    LEFT JOIN [DB_PHE_Exploration].[xplore].[TX_ESDC] esdc on es.xStructureID = esdc.xStructureID
                    WHERE esdc.xStructureID is null
                    AND es.StatusData = 'Approved'
                    AND (es.ZonaID = '{1}' or es.RegionalID = '{1}' or es.SubholdingID = '{1}')
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
                    ,es.[IsDeleted]
                    ,es.StatusData
                    ,es.[CreatedDate]
                    ,es.[CreatedBy]
                    ,esdc.xStructureID";

        public override string GenerateID => @"SELECT COUNT(*) FROM [dbo].[DIM_OrgUnitHierarchy] a";

        public string LookupEntityLevelQuery => @"
            select a.Lvl1EntityID, 
	               a.Lvl2EntityID, 
	               a.Lvl3EntityID 
            from [dbo].[DIM_OrgUnitHierarchy] a 
            where OrgUnitID = '{0}'";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
