using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface IMDParameterListService : ICrudService<MDParameterListDto, MDParameterListDto>
    {
        Task<string> GetParamListIDByTwoParam(string paramID, string paramValueText);

        Task<string> GetParamDescByParamListID(string paramListID);
    }
}
