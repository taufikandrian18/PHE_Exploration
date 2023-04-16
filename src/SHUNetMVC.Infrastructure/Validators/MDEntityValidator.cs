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
    public class MDEntityValidator : AbstractValidator<MDEntityDto>
    {
        private IMDEntityService _service;
        private FormState _state;
        public MDEntityValidator(FormState state, IMDEntityService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.EffectiveYear).NotEmpty().WithName("Name");
            RuleFor(x => x.SubholdingID).NotEmpty().WithName("Name");
        }
    }
}
