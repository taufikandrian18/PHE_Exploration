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
    public class TXESDCProductionValidator : AbstractValidator<TXESDCProductionDto>
    {
        private ITXESDCProductionService _esdcProductionService;
        private FormState _state;
        public TXESDCProductionValidator(FormState state, ITXESDCProductionService esdcProductionService)
        {
            _state = state;
            _esdcProductionService = esdcProductionService;

            RuleFor(x => x.SCPCondensate).NotEmpty().WithName("Name");
        }
    }
}
