using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Services
{
    public class VWCountryService : BaseCrudService<VWCountryDto, VWCountryDto>, IVWCountryService
    {
        private readonly IVWCountryRepository _repo;
        public VWCountryService(IVWCountryRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public async Task<string> GetCountryByCountryID(string paramListID)
        {
            return await _repo.GetCountryByCountryID(paramListID);
        }

        public async Task<string> GetCountryByTwoParam(string paramID, string paramValueText)
        {
            return await _repo.GetCountryByTwoParam(paramID, paramValueText);
        }
    }
}
