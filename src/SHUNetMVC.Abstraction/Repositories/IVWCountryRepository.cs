using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface IVWCountryRepository : ICrudRepository<VWCountryDto, VWCountryDto>
    {
        Task<string> GetCountryByTwoParam(string paramID, string paramValueText);

        Task<string> GetCountryByCountryID(string paramListID);
    }
}
