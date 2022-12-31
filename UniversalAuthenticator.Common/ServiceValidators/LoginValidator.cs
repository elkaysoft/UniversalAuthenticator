using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Models.Request;

namespace UniversalAuthenticator.Common.ServiceValidators
{
    public class LoginValidator: AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.username).Cascade(CascadeMode.Stop).NotNull().NotEmpty().WithMessage(CustomMessages.UsernameRequired);

            RuleFor(x => x.password).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.PasswordRequired);
        }
    }
}
