using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class DoTransactionResult
    {
        public bool Status { get; set; }
        public Object Object { get; set; }
        public string Message { get; set; }
    }
    public class Object
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
