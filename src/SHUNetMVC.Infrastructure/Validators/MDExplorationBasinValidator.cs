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
    public class MDExplorationBasinValidator : AbstractValidator<MDExplorationBasinDto>
    {
        private IMDExplorationBasinService _explorationBasinService;
        private FormState _state;
        public MDExplorationBasinValidator(FormState state, IMDExplorationBasinService explorationBasinService)
        {
            _state = state;
            _explorationBasinService = explorationBasinService;

            RuleFor(x => x.BasinName).NotEmpty().WithName("Name");
        }
    }
}
