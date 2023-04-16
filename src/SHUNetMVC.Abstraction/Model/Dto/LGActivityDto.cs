using ASPNetMVC.Abstraction.Model.Entities;
using SHUNetMVC.Abstraction.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class LGActivityDto : BaseDtoAutoMapper<LG_Activity>
    {
        public string IP { get; set; }
        public string Menu { get; set; }
        public string Action { get; set; }
        public string TransactionID { get; set; }
        public string Status { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
