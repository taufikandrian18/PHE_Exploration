using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.View
{
    public class GridViewFilter
    {
        public bool IsForLookup { get; set; }
        public string GridId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string OrderBy { get; set; }
        public List<List<GridFilterItem>> FilterItems { get; set; }
    }

    public class GridFilterItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string FilterType { get; set; }
    }
}
