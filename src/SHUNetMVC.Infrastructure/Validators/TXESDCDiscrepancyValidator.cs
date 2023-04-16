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
    public class TXESDCDiscrepancyValidator : AbstractValidator<TXESDCDiscrepancyDto>
    {
        private ITXESDCDiscrepancyService _esdcDiscrepancyService;
        private FormState _state;
        public TXESDCDiscrepancyValidator(FormState state, ITXESDCDiscrepancyService esdcDiscrepancyService)
        {
            _state = state;
            _esdcDiscrepancyService = esdcDiscrepancyService;

            RuleFor(x => x.xStructureID).NotEmpty().WithName("Name");
        }
    }
}
