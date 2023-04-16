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
    public class MDParameterListValidator : AbstractValidator<MDParameterListDto>
    {
        private IMDParameterListService _service;
        private FormState _state;
        public MDParameterListValidator(FormState state, IMDParameterListService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.Schema).NotEmpty().WithName("Name");
            RuleFor(x => x.ParamID).NotEmpty().WithName("Name");
            RuleFor(x => x.ParamListID).NotEmpty().WithName("Name");
        }
    }
}
