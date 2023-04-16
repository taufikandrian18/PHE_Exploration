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
    public class TXESDCForecastValidator : AbstractValidator<TXESDCForecastDto>
    {
        private ITXESDCForecastService _esdcForecastService;
        private FormState _state;
        public TXESDCForecastValidator(FormState state, ITXESDCForecastService esdcForecastService)
        {
            _state = state;
            _esdcForecastService = esdcForecastService;

            RuleFor(x => x.xStructureID).NotEmpty().WithName("Name");
        }
    }
}
