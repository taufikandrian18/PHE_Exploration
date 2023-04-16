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
    public class TXProsResourcesValidator : AbstractValidator<TXProsResourceDto>
    {
        private ITXProsResourcesService _prosResourcesService;
        private FormState _state;
        public TXProsResourcesValidator(FormState state, ITXProsResourcesService prosResourcesService)
        {
            _state = state;
            _prosResourcesService = prosResourcesService;

            RuleFor(x => x.ExplorationStructureName).NotEmpty().WithName("Name");
        }
    }
}
