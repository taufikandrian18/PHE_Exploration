using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHUNetMVC.Web.Models
{
    public class MultiTabMenu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Controller { get; set; }
    }
}