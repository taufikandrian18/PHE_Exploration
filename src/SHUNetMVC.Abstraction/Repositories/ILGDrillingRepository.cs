﻿using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Repositories
{
    public interface ILGDrillingRepository : ICrudRepository<LGDrillingDto, LGDrillingDto>
    {
        List<LGDrillingDto> GetAll();
        Task<List<LGDrillingDto>> GetDrillingByStructureID(string structureID);
    }
}
