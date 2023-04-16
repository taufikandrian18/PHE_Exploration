using ASPNetMVC.Abstraction.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Model.Dto
{
    public class VWCountryDto : BaseDtoAutoMapper<vw_Country>
    {
        public VWCountryDto()
        {

        }

        public VWCountryDto(vw_Country entity) : base(entity)
        {

        }

        public string CountriesID { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string ISOCountriesID { get; set; }
        public string NameIDNVer { get; set; }
    }
}
