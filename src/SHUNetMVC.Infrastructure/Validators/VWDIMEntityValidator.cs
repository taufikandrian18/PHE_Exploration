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
    public class VWDIMEntityValidator : AbstractValidator<VWDIMEntityDto>
    {
        private IVWDIMEntityService _service;
        private FormState _state;
        public VWDIMEntityValidator(FormState state, IVWDIMEntityService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.EntityID).NotEmpty().WithName("Name");
            RuleFor(x => x.EntityType).NotEmpty().WithName("Name");
        }
    }
}
