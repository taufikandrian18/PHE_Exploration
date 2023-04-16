using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXESDCProductionQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT prd.[xStructureID]
                  ,[GCPPrevOil]
                  ,[GCPPrevCondensate]
                  ,[GCPPrevAssociated]
                  ,[GCPPrevNonAssociated]
                  ,[SCPPrevOil]
                  ,[SCPPrevCondensate]
                  ,[SCPPrevAssociated]
                  ,[SCPPrevNonAssociated]
                  ,[GCPOil]
                  ,[GCPCondensate]
                  ,[GCPAssociated]
                  ,[GCPNonAssociated]
                  ,[SCPOil]
                  ,[SCPCondensate]
                  ,[SCPAssociated]
                  ,[SCPNonAssociated]
                  ,prd.[CreatedDate]
                  ,prd.[CreatedBy]
                  ,prd.[UpdatedDate]
                  ,prd.[UpdatedBy]
              FROM [DB_PHE_Exploration].[xplore].[TX_ESDCProd] prd
              join [DB_PHE_Exploration].[xplore].[TX_ESDC] esdc
              on esdc.xStructureID = prd.xStructureID";

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
            select count(1) from [DB_PHE_Exploration].[xplore].[TX_ESDCProd]";
        public override string GenerateID => @"SELECT COUNT(*) [DB_PHE_Exploration].[xplore].[TX_ESDCProd]";

        public override string LookupTextQuery => @"select es.xStructureName from xplore.TX_ProsResources pr join dbo.MD_ExplorationStructure es on pr.xStructureID = es.xStructureID";
        public override string LookupListTextQuery => @"
            SELECT prd.[xStructureID]
                  ,[GCPPrevOil]
                  ,[GCPPrevCondensate]
                  ,[GCPPrevAssociated]
                  ,[GCPPrevNonAssociated]
                  ,[SCPPrevOil]
                  ,[SCPPrevCondensate]
                  ,[SCPPrevAssociated]
                  ,[SCPPrevNonAssociated]
                  ,[GCPOil]
                  ,[GCPCondensate]
                  ,[GCPAssociated]
                  ,[GCPNonAssociated]
                  ,[SCPOil]
                  ,[SCPCondensate]
                  ,[SCPAssociated]
                  ,[SCPNonAssociated]
                  ,prd.[CreatedDate]
                  ,prd.[CreatedBy]
                  ,prd.[UpdatedDate]
                  ,prd.[UpdatedBy]
              FROM [DB_PHE_Exploration].[xplore].[TX_ESDCProd] prd
              join [DB_PHE_Exploration].[xplore].[TX_ESDC] esdc
              on esdc.xStructureID = prd.xStructureID
              where prd.xStructureID = '{0}'";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
