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
    public class TXProsResourcesTargetValidator : AbstractValidator<TXProsResourcesTargetDto>
    {
        private ITXProsResourcesTargetService _prosResourcesTargetService;
        private FormState _state;
        public TXProsResourcesTargetValidator(FormState state, ITXProsResourcesTargetService prosResourcesTargetService)
        {
            _state = state;
            _prosResourcesTargetService = prosResourcesTargetService;

            RuleFor(x => x.TargetName).NotEmpty().WithName("Name");
        }
    }
}
