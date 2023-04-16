using ASPNetMVC.Abstraction.Model.Entities;
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
    public class LGContingenResourcesService : BaseCrudService<LGContingenResourcesDto, LGContingenResourcesDto>, ILGContingenResourcesService
    {
        private readonly ILGContingenResourcesRepository _repo;
        public LGContingenResourcesService(ILGContingenResourcesRepository repo, ILogger logger) : base(repo, logger)
        {
            _repo = repo;
        }

        public List<LGContingenResourcesDto> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<LG_ContingentResources> GetContResourceTargetByStructureID(string structureID)
        {
            return await _repo.GetContResourceTargetByStructureID(structureID);
        }
    }
}
