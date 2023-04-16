using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.EntityFramework.Queries
{
    public class TXAttachmentQuery : BaseCrudQuery
    {
        public override string SelectPagedQuery => @"
            SELECT a.[Schema],
                  a.TransactionID,
                  a.PathParID,
                  a.FileID,
                  a.FileCategoryParID,
                  a.FileName,
                  a.Remarks,
                  a.SizeUoM,
                  a.Size,
                  a.IsDeleted,
                  a.RefID
              FROM dbo.TX_Attachment a
              WHERE a.IsDeleted = 0";

        public override string PagedRoles => @"
            SELECT a.[Schema],
                  a.TransactionID,
                  a.PathParID,
                  a.FileID,
                  a.FileCategoryParID,
                  a.FileName,
                  a.Remarks,
                  a.SizeUoM,
                  a.Size,
                  a.IsDeleted,
                  a.RefID
              FROM dbo.TX_Attachment a
              WHERE a.IsDeleted = 0 AND a.TransactionID = '{0}'";

        public override string CountQuery => @"
            select count(1) from dbo.TX_Attachment a where a.IsDeleted = 0";
        public override string GenerateID => @"SELECT COUNT(*) FROM dbo.TX_Attachment a";

        public override string LookupTextQuery => @"
            select a.FileName
            from dbo.TX_Attachment a
            WHERE a.IsDeleted = 0 AND a.TransactionID = '{0}'";
        public override string LookupListTextQuery => @"
            SELECT a.[Schema],
                  a.TransactionID,
                  a.PathParID,
                  a.FileID,
                  a.FileCategoryParID,
                  a.FileName,
                  a.Remarks,
                  a.SizeUoM,
                  a.Size,
                  a.IsDeleted,
                  a.RefID
              FROM dbo.TX_Attachment a
              WHERE a.IsDeleted = 0 AND a.TransactionID = '{0}'";

        public override string PagedReport => throw new NotImplementedException();

        public override string ExcelExportQuery => throw new NotImplementedException();
    }
}
