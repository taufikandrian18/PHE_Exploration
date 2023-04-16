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
    public class LGActivityService : BaseCrudService<LGActivityDto, LGActivityDto>, ILGActivityService
    {
        private readonly ILGActivityRepository _repo;
        public LGActivityService(ILGActivityRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public List<LGActivityDto> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
