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
    public class MDParameterValidator : AbstractValidator<MDParameterDto>
    {
        private IMDParameterService _service;
        private FormState _state;
        public MDParameterValidator(FormState state, IMDParameterService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.Schema).NotEmpty().WithName("Name");
            RuleFor(x => x.ParamID).NotEmpty().WithName("Name");
        }
    }
}
