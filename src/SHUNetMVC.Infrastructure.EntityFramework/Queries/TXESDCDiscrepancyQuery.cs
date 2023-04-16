using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXESDCDiscrepancyQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
                        SELECT [xStructureID]
                          ,[UncertaintyLevel]
                          ,[CFUMOil]
                          ,[CFUMCondensate]
                          ,[CFUMAssociated]
                          ,[CFUMNonAssociated]
                          ,[CFPPAOil]
                          ,[CFPPACondensate]
                          ,[CFPPAAssociated]
                          ,[CFPPANonAssociated]
                          ,[CFWIOil]
                          ,[CFWICondensate]
                          ,[CFWIAssociated]
                          ,[CFWINonAssociated]
                          ,[CFCOil]
                          ,[CFCCondensate]
                          ,[CFCAssociated]
                          ,[CFCNonAssociated]
                          ,[UCOil]
                          ,[UCCondensate]
                          ,[UCAssociated]
                          ,[UCNonAssociated]
                          ,[CIOOil]
                          ,[CIOCondensate]
                          ,[CIOAssociated]
                          ,[CIONonAssociated]
                          ,[CreatedDate]
                          ,[CreatedBy]
                          ,[UpdatedDate]
                          ,[UpdatedBy]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCDiscrepancy]";

        public override string PagedRoles => @"
                        SELECT [xStructureID]
                          ,[UncertaintyLevel]
                          ,[CFUMOil]
                          ,[CFUMCondensate]
                          ,[CFUMAssociated]
                          ,[CFUMNonAssociated]
                          ,[CFPPAOil]
                          ,[CFPPACondensate]
                          ,[CFPPAAssociated]
                          ,[CFPPANonAssociated]
                          ,[CFWIOil]
                          ,[CFWICondensate]
                          ,[CFWIAssociated]
                          ,[CFWINonAssociated]
                          ,[CFCOil]
                          ,[CFCCondensate]
                          ,[CFCAssociated]
                          ,[CFCNonAssociated]
                          ,[UCOil]
                          ,[UCCondensate]
                          ,[UCAssociated]
                          ,[UCNonAssociated]
                          ,[CIOOil]
                          ,[CIOCondensate]
                          ,[CIOAssociated]
                          ,[CIONonAssociated]
                          ,[CreatedDate]
                          ,[CreatedBy]
                          ,[UpdatedDate]
                          ,[UpdatedBy]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCDiscrepancy]";

        public override string CountQuery => @"
            select count(1) FROM [DB_PHE_Exploration].[xplore].[TX_ESDCDiscrepancy]";

        public override string LookupTextQuery => @"select pt.TargetName from xplore.TX_ProsResourcesTarget pt";

        public override string LookupListTextQuery => @"
            SELECT [xStructureID]
                          ,[UncertaintyLevel]
                          ,[CFUMOil]
                          ,[CFUMCondensate]
                          ,[CFUMAssociated]
                          ,[CFUMNonAssociated]
                          ,[CFPPAOil]
                          ,[CFPPACondensate]
                          ,[CFPPAAssociated]
                          ,[CFPPANonAssociated]
                          ,[CFWIOil]
                          ,[CFWICondensate]
                          ,[CFWIAssociated]
                          ,[CFWINonAssociated]
                          ,[CFCOil]
                          ,[CFCCondensate]
                          ,[CFCAssociated]
                          ,[CFCNonAssociated]
                          ,[UCOil]
                          ,[UCCondensate]
                          ,[UCAssociated]
                          ,[UCNonAssociated]
                          ,[CIOOil]
                          ,[CIOCondensate]
                          ,[CIOAssociated]
                          ,[CIONonAssociated]
                          ,[CreatedDate]
                          ,[CreatedBy]
                          ,[UpdatedDate]
                          ,[UpdatedBy]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCDiscrepancy]
                          where [xStructureID] = '{0}'";

        public override string GenerateID => @"select TOP 1 TargetID from xplore.TX_ProsResourcesTarget order by TargetID desc";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
