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
    public class TXDrillingValidator : AbstractValidator<TXDrillingDto>
    {
        private ITXDrillingService _drillingService;
        private FormState _state;
        public TXDrillingValidator(FormState state, ITXDrillingService drillingService)
        {
            _state = state;
            _drillingService = drillingService;

            RuleFor(x => x.xWellName).NotEmpty().WithName("Name");
            RuleFor(x => x.DrillingLocation).NotEmpty().WithName("LocationName");
        }
    }
}
