using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXContingenResourcesQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            select cr.[xStructureID],
	               es.[xStructureName],
	               cr.[1COil],
	               cr.[1COilUoM],
                   cr.[2COil],
                   cr.[2COilUoM],
                   cr.[3COil],
                   cr.[3COilUoM],
                   cr.[1CGas],
                   cr.[1CGasUoM],
                   cr.[2CGas],
                   cr.[2CGasUoM],
                   cr.[3CGas],
                   cr.[3CGasUoM],
                   cr.[1CTotal],
                   cr.[1CTotalUoM],
                   cr.[2CTotal],
                   cr.[2CTotalUoM],
                   cr.[3CTotal],
                   cr.[3CTotalUoM],
                   cr.[CreatedDate],
                   cr.[CreatedBy]
            from xplore.TX_ContingentResources cr
            join dbo.MD_ExplorationStructure es
            on cr.[xStructureID] = es.[xStructureID]";

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
            select count(1) from xplore.TX_ContingentResources cr join dbo.MD_ExplorationStructure es on cr.xStructureID = es.xStructureID";

        public override string GenerateID => @"SELECT COUNT(*) FROM xplore.TX_ContingentResources cr";

        public override string LookupTextQuery => @"select es.xStructureName from xplore.TX_ContingentResources cr join dbo.MD_ExplorationStructure es on cr.xStructureID = es.xStructureID";
        public override string LookupListTextQuery => @"
            select cr.[xStructureID],
	               es.[xStructureName],
	               cr.[1COil],
	               cr.[1COilUoM],
                   cr.[2COil],
                   cr.[2COilUoM],
                   cr.[3COil],
                   cr.[3COilUoM],
                   cr.[1CGas],
                   cr.[1CGasUoM],
                   cr.[2CGas],
                   cr.[2CGasUoM],
                   cr.[3CGas],
                   cr.[3CGasUoM],
                   cr.[1CTotal],
                   cr.[1CTotalUoM],
                   cr.[2CTotal],
                   cr.[2CTotalUoM],
                   cr.[3CTotal],
                   cr.[3CTotalUoM],
                   cr.[CreatedDate],
                   cr.[CreatedBy]
            from xplore.TX_ContingentResources cr
            join dbo.MD_ExplorationStructure es
            on cr.[xStructureID] = es.[xStructureID]
            where cr.[xStructureID] = '{0}'";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => @"
            select cr.[xStructureID],
	               es.[xStructureName],
	               cr.[1COil],
	               cr.[1COilUoM],
                   cr.[2COil],
                   cr.[2COilUoM],
                   cr.[3COil],
                   cr.[3COilUoM],
                   cr.[1CGas],
                   cr.[1CGasUoM],
                   cr.[2CGas],
                   cr.[2CGasUoM],
                   cr.[3CGas],
                   cr.[3CGasUoM],
                   cr.[1CTotal],
                   cr.[1CTotalUoM],
                   cr.[2CTotal],
                   cr.[2CTotalUoM],
                   cr.[3CTotal],
                   cr.[3CTotalUoM],
                   cr.[CreatedDate]
            from xplore.TX_ContingentResources cr
            join dbo.MD_ExplorationStructure es
            on cr.[xStructureID] = es.[xStructureID]
            where cr.[xStructureID] = '{0}'";
    }
}
