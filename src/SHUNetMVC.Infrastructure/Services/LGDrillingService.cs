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
    public class LGDrillingService : BaseCrudService<LGDrillingDto, LGDrillingDto>, ILGDrillingService
    {
        private readonly ILGDrillingRepository _repo;
        public LGDrillingService(ILGDrillingRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public List<LGDrillingDto> GetAll()
        {
            return _repo.GetAll();
        }

        public Task<List<LGDrillingDto>> GetDrillingByStructureID(string structureID)
        {
            return _repo.GetDrillingByStructureID(structureID);
        }
    }
}
