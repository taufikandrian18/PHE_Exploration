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
    public class TXESDCVolumetricValidator : AbstractValidator<TXESDCVolumetricDto>
    {
        private ITXESDCVolumetricService _esdcVolumetricService;
        private FormState _state;
        public TXESDCVolumetricValidator(FormState state, ITXESDCVolumetricService esdcVolumetricService)
        {
            _state = state;
            _esdcVolumetricService = esdcVolumetricService;

            RuleFor(x => x.xStructureID).NotEmpty().WithName("Name");
        }
    }
}
