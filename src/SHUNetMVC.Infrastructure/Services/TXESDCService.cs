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
    public class TXESDCService : BaseCrudService<TXESDCDto, TXESDCDto>, ITXESDCService
    {
        private readonly ITXESDCRepository _repo;
        public TXESDCService(ITXESDCRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }
    }
}
