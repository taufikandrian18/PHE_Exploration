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
    public class VWDIMEntityService : BaseCrudService<VWDIMEntityDto, VWDIMEntityDto>, IVWDIMEntityService
    {
        private readonly IVWDIMEntityRepository _repo;
        public VWDIMEntityService(IVWDIMEntityRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }
    }
}
