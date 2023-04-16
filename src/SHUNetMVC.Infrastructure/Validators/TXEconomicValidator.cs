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
    public class TXEconomicValidator : AbstractValidator<TXEconomicDto>
    {
        private ITXEconomicService _economicService;
        private FormState _state;
        public TXEconomicValidator(FormState state, ITXEconomicService economicService)
        {
            _state = state;
            _economicService = economicService;

            RuleFor(x => x.DevConcept).NotEmpty().WithName("Name");
        }
    }
}
