using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class MDExplorationAssetQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select  a.xAssetID,
                a.xAssetName
            from dbo.MD_ExplorationAsset a";

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
            select count(1) from dbo.MD_ExplorationAsset a";

        public override string LookupTextQuery => @"
            select a.xAssetName 
            from dbo.MD_ExplorationAsset a
            where a.xAssetID = {0}";
        public override string GenerateID => @"SELECT COUNT(*) FROM dbo.MD_ExplorationAsset a";

        public override string LookupListTextQuery => @"
        select  a.xAssetID,
                a.xAssetName
            from dbo.MD_ExplorationAsset a
            where a.xAssetID = {0}";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
