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
    public class TXContingenResourcesValidator : AbstractValidator<TXContingenResourcesDto>
    {
        private ITXContingenResourcesService _contResourcesService;
        private FormState _state;
        public TXContingenResourcesValidator(FormState state, ITXContingenResourcesService contResourcesService)
        {
            _state = state;
            _contResourcesService = contResourcesService;

            RuleFor(x => x.ExplorationStructureName).NotEmpty().WithName("Name");
        }
    }
}
