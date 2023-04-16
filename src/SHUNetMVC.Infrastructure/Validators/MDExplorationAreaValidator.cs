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
    public class MDExplorationAreaValidator : AbstractValidator<MDExplorationAreaDto>
    {
        private IMDExplorationAreaService _explorationAreaService;
        private FormState _state;
        public MDExplorationAreaValidator(FormState state, IMDExplorationAreaService explorationAreaService)
        {
            _state = state;
            _explorationAreaService = explorationAreaService;

            RuleFor(x => x.xAreaName).NotEmpty().WithName("Name");
        }
    }
}
