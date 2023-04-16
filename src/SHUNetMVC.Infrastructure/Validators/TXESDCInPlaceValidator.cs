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
    public class TXESDCInPlaceValidator : AbstractValidator<TXESDCInPlaceDto>
    {
        private ITXESDCInPlaceService _esdcInPlaceService;
        private FormState _state;
        public TXESDCInPlaceValidator(FormState state, ITXESDCInPlaceService esdcInPlaceService)
        {
            _state = state;
            _esdcInPlaceService = esdcInPlaceService;

            RuleFor(x => x.xStructureID).NotEmpty().WithName("Name");
        }
    }
}
