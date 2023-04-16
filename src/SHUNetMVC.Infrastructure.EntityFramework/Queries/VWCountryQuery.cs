using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class VWCountryQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT [NameIDNVer]
            FROM [DB_PHE_Exploration].[dbo].[vw_Country]
            WHERE [CountriesID] = '{0}' AND [Name] = '{1}'";

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

        public override string PagedReport => @"
            SELECT [NameIDNVer]
            FROM [DB_PHE_Exploration].[dbo].[vw_Country]
            WHERE [CountriesID] = '{0}'";

        public override string CountQuery => @"
            select count(1) FROM [DB_PHE_Exploration].[dbo].[vw_Country]";

        public override string LookupTextQuery => @"
            SELECT [NameIDNVer]
            FROM [DB_PHE_Exploration].[dbo].[vw_Country]
            WHERE [CountriesID] = '{0}'";

        public override string LookupListTextQuery => @"
            SELECT ct.[CountriesID]
                  ,ct.[Url]
                  ,ct.[Name]
                  ,ct.[Latitude]
                  ,ct.[Longitude]
                  ,ct.[ISOCountriesID]
                  ,ct.[NameIDNVer]
              FROM [DB_PHE_Exploration].[dbo].[vw_Country] ct
              WHERE ct.[CountriesID] = '{0}'";

        public override string GenerateID => @"SELECT COUNT(*) FROM [DB_PHE_Exploration].[dbo].[vw_Country] ct";

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
