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
    public class MDParameterListService : BaseCrudService<MDParameterListDto, MDParameterListDto>, IMDParameterListService
    {
        private readonly IMDParameterListRepository _repo;
        public MDParameterListService(IMDParameterListRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public async Task<string> GetParamDescByParamListID(string paramListID)
        {
            return await _repo.GetParamDescByParamListID(paramListID);
        }

        public async Task<string> GetParamListIDByTwoParam(string paramID, string paramValueText)
        {
            return await _repo.GetParamListIDByTwoParam(paramID, paramValueText);
        }
    }
}
