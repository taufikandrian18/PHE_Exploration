using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class HRISDevQuery : BaseCrudQuery
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
            select count(1) from dbo.MP_Entity e";

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
            select top 1 EntityLvl
	        from [DB_PHE_HRIS_DEV].[dbo].[DIM_OrgUnitHierarchy]
	        UNPIVOT (EntityLvl FOR HierLevel IN (Lvl3EntityID, Lvl2EntityID, Lvl1EntityID)) AS unpvt
	        where OrgUnitID = '{0}'
	        and LEFT(EntityLvl, 1) != 'O'
	        AND LEFT(EntityLvl, 4) != 'PDSI'
	        AND EntityLvl != 'SHUOLD'
	        AND EntityLvl is not null";

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
