using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXESDCInPlaceQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
                        SELECT [xStructureID]
                              ,[UncertaintyLevel]
                              ,[P90IOIP]
                              ,[P90IGIP]
                              ,[CreatedDate]
                              ,[CreatedBy]
                              ,[UpdatedDate]
                              ,[UpdatedBy]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCInPlace]";

        public override string PagedRoles => @"
                        SELECT [xStructureID]
                              ,[UncertaintyLevel]
                              ,[P90IOIP]
                              ,[P90IGIP]
                              ,[CreatedDate]
                              ,[CreatedBy]
                              ,[UpdatedDate]
                              ,[UpdatedBy]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCInPlace]";

        public override string CountQuery => @"
            select count(1) FROM [DB_PHE_Exploration].[xplore].[TX_ESDCInPlace]";

        public override string LookupTextQuery => @"select pt.TargetName from xplore.TX_ProsResourcesTarget pt";

        public override string LookupListTextQuery => @"
            SELECT [xStructureID]
                    ,[UncertaintyLevel]
                    ,[P90IOIP]
                    ,[P90IGIP]
                    ,[CreatedDate]
                    ,[CreatedBy]
                    ,[UpdatedDate]
                    ,[UpdatedBy]
                FROM [DB_PHE_Exploration].[xplore].[TX_ESDCInPlace]
                where [xStructureID] = '{0}'";

        public override string GenerateID => @"select TOP 1 TargetID from xplore.TX_ProsResourcesTarget order by TargetID desc";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
