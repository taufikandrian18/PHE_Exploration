using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class VWDIMEntityQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT [EntityID]
                  ,[EntityName]
                  ,[EntityType]
                  ,[IsActive]
                  ,[CreatedDate]
                  ,[UpdatedDate]
              FROM [DB_PHE_Exploration].[dbo].[vw_DIM_Entity]";

        public override string PagedRoles => @"
                        SELECT [EntityID]
                              ,[EntityName]
                              ,[EntityType]
                              ,[IsActive]
                              ,[CreatedDate]
                              ,[UpdatedDate]
                          FROM [DB_PHE_Exploration].[dbo].[vw_DIM_Entity]";

        public override string CountQuery => @"
                select count(1) FROM [DB_PHE_Exploration].[dbo].[vw_DIM_Entity]";

        public override string LookupTextQuery => @"
            SELECT [EntityName]
              FROM [DB_PHE_Exploration].[dbo].[vw_DIM_Entity]
              WHERE EntityID = '{0}'";

        public override string LookupListTextQuery => @"
                SELECT [EntityID]
                      ,[EntityName]
                      ,[EntityType]
                      ,[IsActive]
                      ,[CreatedDate]
                      ,[UpdatedDate]
                  FROM [DB_PHE_Exploration].[dbo].[vw_DIM_Entity]
                  WHERE EntityID = '{0}'";

        public override string GenerateID => @"SELECT COUNT(*) FROM [DB_PHE_Exploration].[dbo].[vw_DIM_Entity]";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
