using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXESDCForecastQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
                        SELECT [xStructureID]
                          ,[Year]
                          ,[TPFOil]
                          ,[TPFCondensate]
                          ,[TPFAssociated]
                          ,[TPFNonAssociated]
                          ,[SFOil]
                          ,[SFCondensate]
                          ,[SFAssociated]
                          ,[SFNonAssociated]
                          ,[CIOOil]
                          ,[CIOCondensate]
                          ,[CIOAssociated]
                          ,[CIONonAssociated]
                          ,[LPOil]
                          ,[LPCondensate]
                          ,[LPAssociated]
                          ,[LPNonAssociated]
                          ,[AverageGrossHeat]
                          ,[Remarks]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCForecast]";

        public override string PagedRoles => @"
                        SELECT [xStructureID]
                          ,[Year]
                          ,[TPFOil]
                          ,[TPFCondensate]
                          ,[TPFAssociated]
                          ,[TPFNonAssociated]
                          ,[SFOil]
                          ,[SFCondensate]
                          ,[SFAssociated]
                          ,[SFNonAssociated]
                          ,[CIOOil]
                          ,[CIOCondensate]
                          ,[CIOAssociated]
                          ,[CIONonAssociated]
                          ,[LPOil]
                          ,[LPCondensate]
                          ,[LPAssociated]
                          ,[LPNonAssociated]
                          ,[AverageGrossHeat]
                          ,[Remarks]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCForecast]";

        public override string CountQuery => @"
            select count(1) FROM [DB_PHE_Exploration].[xplore].[TX_ESDCForecast]";

        public override string LookupTextQuery => @"select pt.TargetName from xplore.TX_ProsResourcesTarget pt";

        public override string LookupListTextQuery => @"
            SELECT [xStructureID]
                          ,[Year]
                          ,[TPFOil]
                          ,[TPFCondensate]
                          ,[TPFAssociated]
                          ,[TPFNonAssociated]
                          ,[SFOil]
                          ,[SFCondensate]
                          ,[SFAssociated]
                          ,[SFNonAssociated]
                          ,[CIOOil]
                          ,[CIOCondensate]
                          ,[CIOAssociated]
                          ,[CIONonAssociated]
                          ,[LPOil]
                          ,[LPCondensate]
                          ,[LPAssociated]
                          ,[LPNonAssociated]
                          ,[AverageGrossHeat]
                          ,[Remarks]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCForecast]
                          where [xStructureID] = '{0}'";

        public override string GenerateID => @"select TOP 1 TargetID from xplore.TX_ProsResourcesTarget order by TargetID desc";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
