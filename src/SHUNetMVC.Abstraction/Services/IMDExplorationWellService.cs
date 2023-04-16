using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface IMDExplorationWellService : ICrudService<MDExplorationWellDto, MDExplorationWellDto>
    {
        Task<string> GenerateNewID();
        void DestroyWell(string wellId);
        Task Destroy(string wellID);
    }
}
