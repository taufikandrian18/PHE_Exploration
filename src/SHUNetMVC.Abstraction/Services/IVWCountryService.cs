﻿using SHUNetMVC.Abstraction.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface IVWCountryService : ICrudService<VWCountryDto, VWCountryDto>
    {
        Task<string> GetCountryByTwoParam(string paramID, string paramValueText);

        Task<string> GetCountryByCountryID(string paramListID);
    }
}