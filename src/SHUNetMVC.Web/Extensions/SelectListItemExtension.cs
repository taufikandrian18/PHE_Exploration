using SHUNetMVC.Abstraction.Model.View;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SHUNetMVC.Web.Extensions
{
    public static class SelectListItemExtension
    {
        public static SelectList ToSelectList(this LookupList lookupList)
        {
            return new SelectList(lookupList.Items.ToList(), "Value", "Text");
        }
    }
}