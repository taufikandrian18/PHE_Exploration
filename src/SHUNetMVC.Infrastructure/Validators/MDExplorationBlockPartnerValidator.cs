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
    public class MDExplorationBlockPartnerValidator : AbstractValidator<MDExplorationBlockPartnerDto>
    {
        private IMDExplorationBlockPartnerService _explorationBlockPartnerService;
        private FormState _state;
        public MDExplorationBlockPartnerValidator(FormState state, IMDExplorationBlockPartnerService explorationBlockPartnerService)
        {
            _state = state;
            _explorationBlockPartnerService = explorationBlockPartnerService;

            RuleFor(x => x.PartnerName).NotEmpty().WithName("Name");
        }
    }
}
