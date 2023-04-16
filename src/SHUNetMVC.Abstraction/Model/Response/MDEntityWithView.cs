using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Response
{
    public class MDEntityWithView
    {
        public string EntityName { get; set; }
    }

    public class MDEntityDateWithView
    {
        public Nullable<DateTime> EntityName { get; set; }
    }

    public class MDEntityNumberWithView
    {
        public Nullable<decimal> EntityName { get; set; }
    }
}
