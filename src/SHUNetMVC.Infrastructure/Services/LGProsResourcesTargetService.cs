using ASPNetMVC.Abstraction.Model.Entities;
using Serilog;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.Response;
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
    public class LGProsResourcesTargetService : BaseCrudService<LGProsResourcesTargetDto, TXPropectiveResourceTargetWthFields>, ILGProsResourcesTargetService
    {
        private readonly ILGProsResourcesTargetRepository _repo;
        public LGProsResourcesTargetService(ILGProsResourcesTargetRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public Task Destroy(string targetID)
        {
            return _repo.Destroy(targetID);
        }

        public void DestroyTarget(string targetID)
        {
            _repo.DestroyTarget(targetID);
        }

        public async Task<string> GenerateNewID()
        {
            return await _repo.GenerateNewID();
        }

        public List<LGProsResourcesTargetDto> GetAll()
        {
            return _repo.GetAll();
        }
    }
}
