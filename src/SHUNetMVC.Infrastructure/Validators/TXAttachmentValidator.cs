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
    public class TXAttachmentValidator : AbstractValidator<TXAttachmentDto>
    {
        private ITXAttachmentService _service;
        private FormState _state;
        public TXAttachmentValidator(FormState state, ITXAttachmentService service)
        {
            _state = state;
            _service = service;

            RuleFor(x => x.Schema).NotEmpty().WithName("Name");
        }
    }
}
