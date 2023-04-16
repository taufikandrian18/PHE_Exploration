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
    public class MDExplorationWellValidator : AbstractValidator<MDExplorationWellDto>
    {
        private IMDExplorationWellService _explorationWellService;
        private FormState _state;
        public MDExplorationWellValidator(FormState state, IMDExplorationWellService explorationWellService)
        {
            _state = state;
            _explorationWellService = explorationWellService;

            RuleFor(x => x.xWellName).NotEmpty().WithName("Name");
        }
    }
}
