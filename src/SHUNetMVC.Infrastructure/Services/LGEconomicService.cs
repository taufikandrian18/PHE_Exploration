using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Repositories;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Services
{
    public class LGEconomicService : BaseCrudService<LGEconomicDto, LGEconomicDto>, ILGEconomicService
    {
        private readonly ILGEconomicRepository _repo;
        public LGEconomicService(ILGEconomicRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string structureID)
        {
            return _repo.Destroy(structureID);
        }

        public List<LGEconomicDto> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
