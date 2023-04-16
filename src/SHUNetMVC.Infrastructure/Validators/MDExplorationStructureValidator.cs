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
    public class MDExplorationStructureValidator : AbstractValidator<MDExplorationStructureDto>
    {
        private IMDExplorationStructureService _explorationStructureService;
        private FormState _state;
        public MDExplorationStructureValidator(FormState state, IMDExplorationStructureService explorationStructureService)
        {
            _state = state;
            _explorationStructureService = explorationStructureService;

            RuleFor(x => x.xStructureName).NotEmpty().WithName("Name");
            RuleFor(x => x).Must(MDExplorationStructureNotExist).WithName("StructureName").WithMessage("An exploration structure with this name already exists. Use a different name.");
        }
        private bool MDExplorationStructureNotExist(MDExplorationStructureDto model)
        {
            var existingExplorationStructures = _explorationStructureService.GetByStructureName(model.xStructureName);
            if (!existingExplorationStructures.Any())
            {
                return true;
            }
            if (_state == FormState.Edit)
            {
                var isCurrentExplorationStructure = existingExplorationStructures.Any(o => o.xStructureID == (model.xStructureID));
                if (isCurrentExplorationStructure)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
