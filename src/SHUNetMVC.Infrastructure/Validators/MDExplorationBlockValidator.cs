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
    public class MDExplorationBlockValidator : AbstractValidator<MDExplorationBlockDto>
    {
        private IMDExplorationBlockService _explorationBlockService;
        private FormState _state;
        public MDExplorationBlockValidator(FormState state, IMDExplorationBlockService explorationBlockService)
        {
            _state = state;
            _explorationBlockService = explorationBlockService;

            RuleFor(x => x.xBlockName).NotEmpty().WithName("Name");
        }
    }
}
