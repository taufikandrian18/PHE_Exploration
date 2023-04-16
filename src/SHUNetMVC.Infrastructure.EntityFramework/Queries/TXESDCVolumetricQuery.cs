using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXESDCVolumetricQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
                        SELECT [xStructureID]
                              ,[UncertaintyLevel]
                              ,[GRRPrevOil]
                              ,[GRRPrevCondensate]
                              ,[GRRPrevAssociated]
                              ,[GRRPrevNonAssociated]
                              ,[ReservesPrevOil]
                              ,[ReservesPrevCondensate]
                              ,[ReservesPrevAssociated]
                              ,[ReservesPrevNonAssociated]
                              ,[GOIOil]
                              ,[GOICondensate]
                              ,[GOIAssociated]
                              ,[GOINonAssociated]
                              ,[ReservesOil]
                              ,[ReservesCondensate]
                              ,[ReservesAssociated]
                              ,[ReservesNonAssociated]
                              ,[Remarks]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCVolumetric]";

        public override string PagedRoles => @"
                        SELECT [xStructureID]
                              ,[UncertaintyLevel]
                              ,[GRRPrevOil]
                              ,[GRRPrevCondensate]
                              ,[GRRPrevAssociated]
                              ,[GRRPrevNonAssociated]
                              ,[ReservesPrevOil]
                              ,[ReservesPrevCondensate]
                              ,[ReservesPrevAssociated]
                              ,[ReservesPrevNonAssociated]
                              ,[GOIOil]
                              ,[GOICondensate]
                              ,[GOIAssociated]
                              ,[GOINonAssociated]
                              ,[ReservesOil]
                              ,[ReservesCondensate]
                              ,[ReservesAssociated]
                              ,[ReservesNonAssociated]
                              ,[Remarks]
                          FROM [DB_PHE_Exploration].[xplore].[TX_ESDCVolumetric]";

        public override string CountQuery => @"
            select count(1) FROM [DB_PHE_Exploration].[xplore].[TX_ESDCVolumetric]";

        public override string LookupTextQuery => @"select pt.TargetName from xplore.TX_ProsResourcesTarget pt";

        public override string LookupListTextQuery => @"
            SELECT [xStructureID]
                    ,[UncertaintyLevel]
                    ,[GRRPrevOil]
                    ,[GRRPrevCondensate]
                    ,[GRRPrevAssociated]
                    ,[GRRPrevNonAssociated]
                    ,[ReservesPrevOil]
                    ,[ReservesPrevCondensate]
                    ,[ReservesPrevAssociated]
                    ,[ReservesPrevNonAssociated]
                    ,[GOIOil]
                    ,[GOICondensate]
                    ,[GOIAssociated]
                    ,[GOINonAssociated]
                    ,[ReservesOil]
                    ,[ReservesCondensate]
                    ,[ReservesAssociated]
                    ,[ReservesNonAssociated]
                    ,[Remarks]
                FROM [DB_PHE_Exploration].[xplore].[TX_ESDCVolumetric]
                where [xStructureID] = '{0}'";

        public override string GenerateID => @"select TOP 1 TargetID from xplore.TX_ProsResourcesTarget order by TargetID desc";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
