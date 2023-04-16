using FluentValidation;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUNetMVC.Infrastructure.Validators
{
    public class VWCountryValidator : AbstractValidator<VWCountryDto>
    {
        private IVWCountryService _service;
        private FormState _state;
        public VWCountryValidator(FormState state, IVWCountryService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.CountriesID).NotEmpty().WithName("Name");
            RuleFor(x => x.Name).NotEmpty().WithName("Name");
        }
    }
}
