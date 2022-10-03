using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Domain.Data;
using UniversalAuthenticator.Domain.Entities;

namespace UniversalAuthenticator.Common.ServiceValidators
{
    public class OnboardUserValidator: AbstractValidator<CreateUserRequest>
    {
        private readonly AppSettings _appSettings;
        private readonly IRepository<ApplicationUser> _appUserRepository;
        public OnboardUserValidator(IOptions<AppSettings> appSettings, IRepository<ApplicationUser> appUserRepository)
        {
            _appSettings = appSettings.Value;
            _appUserRepository = appUserRepository;


            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.FirstName_Required);

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.LastName_Required);

            RuleFor(x => x.UserName).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.Username_Required)
                .MaximumLength(50).WithMessage(CustomMessages.Username_too_long)
                .Matches(@"^[^-=%, ""]*$").WithMessage(CustomMessages.InvalidUsername)
                .Must(pass => !_appSettings.BlacklistedUsernameCharacters.Any(word => pass.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0))
                .WithMessage("{PropertyName} contains a word that is not allowed")
                .MustAsync(HasUniqueUsername).WithMessage(CustomMessages.Username_already_exists);

            RuleFor(x => x.Gender).Cascade(CascadeMode.Stop).IsInEnum().WithMessage(CustomMessages.Gender_Required);

            RuleFor(x => x.Email).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.Email_Required)
                    .EmailAddress().WithMessage(CustomMessages.InvalidEmailFormat)
                    .MustAsync(HasUniqueEmailAddress).WithMessage(CustomMessages.Email_already_exists);

            RuleFor(x => x.PhoneNumber).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.Phone_Required)
                .Matches(@"\d").WithMessage("{PropertyName} must contain digits only.")
                .MustAsync(HasUniquePhoneNumber).WithMessage(CustomMessages.Phone_already_exists);

            RuleFor(x => x.Roles).Cascade(CascadeMode.Stop).NotEmpty().WithMessage(CustomMessages.Role_is_required);
        }

        private async Task<bool> HasUniqueUsername(string username, CancellationToken cancellationToken)
        {
            try
            {
                var appUser = await _appUserRepository.GetFirstAsync(x => x.Username == username);
                if (appUser == null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> HasUniquePhoneNumber(string mobile, CancellationToken cancellationToken)
        {
            try
            {
                var appUser = await _appUserRepository.GetFirstAsync(x => x.PhoneNumber == mobile);
                if (appUser == null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> HasUniqueEmailAddress(string email, CancellationToken cancellationToken)
        {
            try
            {
                var appUser = await _appUserRepository.GetFirstAsync(x => x.Email == email);
                if (appUser == null)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }


    }
}
