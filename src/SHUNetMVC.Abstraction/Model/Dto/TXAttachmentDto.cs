using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class TXAttachmentDto : BaseDtoAutoMapper<TX_Attachment>
    {
        public string Schema { get; set; }
        public string TransactionID { get; set; }
        public string SubTranscationID { get; set; }
        public string Menu { get; set; }
        public string PathParID { get; set; }
        public int FileID { get; set; }
        public string FileCategoryParID { get; set; }
        public string FileName { get; set; }
        public string FileNameSave { get; set; }
        public string DocumentType { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> Size { get; set; }
        public string SizeUoM { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.Guid> RefID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public TXAttachmentDto()
        {

        }

        public TXAttachmentDto(TX_Attachment entity) : base(entity)
        {

        }
    }
}
