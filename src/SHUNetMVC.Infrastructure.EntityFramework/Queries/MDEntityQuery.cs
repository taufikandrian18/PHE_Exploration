using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class MDEntityQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT e.EffectiveYear,
                  e.SubholdingID,
                  e.RegionalID,
                  e.ZonaID,
                  e.xBlockID,
                  e.BasinID,
                  e.xAssetID,
                  e.APHID,
                  e.xAreaID,
                  e.IsActive,
                  e.CreatedDate,
                  e.CreatedBy,
                  e.UpdatedDate,
                  e.UpdatedBy
              FROM dbo.MP_Entity e";

        public override string CountQuery => @"
            select count(1) from dbo.MP_Entity e";

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

        public override string LookupTextQuery => @"
            select e.EffectiveYear, e.SubholdingID
            from dbo.MP_Entity e
            where e.EffectiveYear = {0} and e.SubholdingID = {1}";

        public override string GenerateID => @"SELECT COUNT(*) FROM dbo.MP_Entity e";

        public override string LookupListTextQuery => @"
            SELECT e.EffectiveYear,
                e.SubholdingID,
                e.RegionalID,
                e.ZonaID,
                e.xBlockID,
	            bl.xBlockName,
                e.BasinID,
	            bs.BasinName,
                e.xAssetID,
	            ass.xAssetName,
                e.APHID,
                e.xAreaID,
                ar.xAreaName,
                e.IsActive,
                e.CreatedDate,
                e.CreatedBy,
                e.UpdatedDate,
                e.UpdatedBy
            FROM dbo.MP_Entity e
            LEFT JOIN dbo.MD_ExplorationArea ar on e.xAreaID = ar.xAreaID
            LEFT JOIN dbo.MD_ExplorationAsset ass on e.xAssetID = ass.xAssetID
            LEFT JOIN dbo.MD_ExplorationBasin bs on e.BasinID = bs.BasinID
            LEFT JOIN dbo.MD_ExplorationBlock bl on e.xBlockID = bl.xBlockID
            where e.EffectiveYear = {0} and (e.ZonaID = '{1}' or e.RegionalID = '{1}' or e.SubholdingID = '{1}')";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
