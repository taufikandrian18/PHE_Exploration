using FluentValidation;
using SHUNetMVC.Abstraction.Model.Dto;
using SHUNetMVC.Abstraction.Model.View;
using SHUNetMVC.Abstraction.Services;

namespace SHUNetMVC.Infrastructure.Validators
{
    public class MDExplorationAssetValidator : AbstractValidator<MDExplorationAssetDto>
    {
        private IMDExplorationAssetService _explorationAssetService;
        private FormState _state;
        public MDExplorationAssetValidator(FormState state, IMDExplorationAssetService explorationAssetService)
        {
            _state = state;
            _explorationAssetService = explorationAssetService;

            RuleFor(x => x.xAssetName).NotEmpty().WithName("Name");
        }
    }
}
