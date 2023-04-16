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
    public class TXESDCValidator : AbstractValidator<TXESDCDto>
    {
        private ITXESDCService _service;
        private FormState _state;
        public TXESDCValidator(FormState state, ITXESDCService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.ESDCProjectID).NotEmpty().WithName("Name");
        }
    }
}
