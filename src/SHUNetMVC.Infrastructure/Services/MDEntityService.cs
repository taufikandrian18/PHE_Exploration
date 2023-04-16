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
    public class MDEntityService : BaseCrudService<MDEntityDto, MDEntityDto>, IMDEntityService
    {
        private readonly IMDEntityRepository _repo;
        public MDEntityService(IMDEntityRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }
    }
}
