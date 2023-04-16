using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class MDExplorationStructureESDCQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT  es.xStructureID,
                    es.xStructureName,
                    es.xStructureStatusParID,
                    es.SingleOrMultiParID,
                    es.ExplorationTypeParID,
                    pl.ParamValue1Text,
                    ba.BasinID,
                    ba.BasinName,
                    es.RegionalID,
                    (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.RegionalID) as RegionalName,
                    es.ZonaID,
                    (select dim.EntityName from dbo.vw_DIM_Entity dim where dim.EntityID = es.ZonaID) as ZonaName,
                    es.APHID,
                    a.xAssetID,
                    a.xAssetName,
                    bl.xBlockID,
                    bl.xBlockName,
                    es.UDClassificationParID,
                    es.UDSubClassificationParID,
                    es.UDSubTypeParID,
                    es.ExplorationAreaParID,
                    es.CountriesID,
                    es.Play,
                    esdc.StatusData,
                    esdc.CreatedDate,
                    COALESCE(esdc.CreatedBy,'') as CreatedBy,
		            COALESCE((select DISTINCT dr.RKAPFiscalYear from xplore.TX_Drilling dr where dr.xStructureID = esdc.xStructureID),0) as RKAPFiscalYear,
                    esdc.UpdatedDate,
		            COALESCE(esdc.UpdatedBy,'') as UpdatedBy
            FROM xplore.TX_ESDC esdc
            LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
            LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
            LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
            LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
            LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
            WHERE pl.ParamID = 'ExplorationStructureStatus'";

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
                WHERE pl.ParamID = 'ExplorationStructureStatus' AND s.StatusData = 'Submitted'";

        public override string CountQuery => @"
            SELECT  COUNT(*)
            FROM xplore.TX_ESDC esdc
            LEFT JOIN dbo.MD_ExplorationStructure es  on esdc.xStructureID = es.xStructureID
            LEFT JOIN dbo.MD_ParamaterList pl on es.xStructureStatusParID = pl.ParamListID
            LEFT JOIN dbo.MD_ExplorationAsset a on es.xAssetID = a.xAssetID
            LEFT JOIN dbo.MD_ExplorationBasin ba on es.BasinID = ba.BasinID
            LEFT JOIN dbo.MD_ExplorationBlock bl on es.xBlockID = bl.xBlockID
            WHERE pl.ParamID = 'ExplorationStructureStatus'";

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
                    s.CreatedBy
                FROM dbo.MD_ExplorationStructure s
                LEFT JOIN dbo.MD_ExplorationAsset a on s.xAssetID = a.xAssetID
                LEFT JOIN dbo.MD_ExplorationBasin ba on s.BasinID = ba.BasinID
                LEFT JOIN dbo.MD_ExplorationBlock bl on s.xBlockID = bl.xBlockID
                where s.xStructureID = {0}";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
