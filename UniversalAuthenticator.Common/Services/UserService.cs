using AutoMapper;
using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Enums;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;
using UniversalAuthenticator.Domain.Data;
using UniversalAuthenticator.Domain.Entities;



namespace UniversalAuthenticator.Common.Services
{
    public class UserService: IUserService
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IRepository<ApplicationUser> _userRepository;
        private readonly IConfigurationService _configurationService;
        private readonly ICommunicationService _communicationService;

        public UserService(IRoleService roleService, IRepository<ApplicationUser> userRepository, IMapper mapper, IConfigurationService configurationService,
                                ICommunicationService communicationService)
        {
            _roleService = roleService;
            _userRepository = userRepository;
            _mapper = mapper;
            _configurationService = configurationService;
            _communicationService = communicationService;
        }

        public async Task<Tuple<ErrorResponse, GenericResponse<UserOnboardingDto>>> OnboardUser(CreateUserRequest request)
        {
            var response = new GenericResponse<UserOnboardingDto>();
            var error = new ErrorResponse();

            try
            {
                var validatedRole = await _roleService.ValidateApplicationRole(request.Roles);
                if (validatedRole.Count == 0)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.Invalid_roles_supplied);

                var emailTemplate = await _configurationService.GetEmailTemplate(EmailTemplateEnum.Onboarding.DisplayName());
                if(!emailTemplate.Item1)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.EmailTemplateNotFound);

                string temporaryPassword = ExtensionsService.GenerateAlphaNumeric(12);
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    OtherName = request.OtherName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    IsActive = true,
                    DateCreated = DateTime.Now,
                    Gender = request.Gender.DisplayName(),
                    EmailConfirmed = false,
                    IsDeleted = false,
                    PasswordTries = 0,
                    Username = request.UserName,
                    ForceChangeOfPassword = true,
                    SaltedHashedPassword = hashPassword(temporaryPassword)
                };

                if (user.EnableMultiFactorAuthentication) 
                    user.MultiFactorAuthenticationType = request.MultiFactorAuthenticationType.DisplayName();

                await _userRepository.AddAsync(user);
                await _roleService.AddUserRole(request.Roles, user.Id);

                await SendOnboardingEmail(emailTemplate.Item2.Body, emailTemplate.Item2.Subject, temporaryPassword, user);                

                response.data = _mapper.Map<UserOnboardingDto>(user);
                response.code = CustomCodes.Successful;
                response.message = CustomMessages.Successful;               

            }
            catch (CustomException cEx)
            {
                error.Errors.Add(new ErrorModel
                {
                    code = cEx.ErrorCode,
                    message = cEx.Message,
                });
            }
            catch (Exception ex)
            {
                //log exception
                ex.ToString();
                error.Errors.Add(new ErrorModel
                {
                    code = CustomCodes.Something_went_wrong,
                    message = CustomMessages.SomethingWentWrong
                });
            }


            return new Tuple<ErrorResponse, GenericResponse<UserOnboardingDto>>(error, response);
        }


        #region Private Methods

        private string hashPassword(string plainText)
        {
            int saltRound = 10;
            return BCrypt.Net.BCrypt.HashPassword(plainText, saltRound);
        }

        private async Task SendOnboardingEmail(string body, string subject, string password, ApplicationUser user)
        {
            body = body.Replace("{FIRST_NAME}", user.FirstName).Replace("{USERNAME}", user.Username)
               .Replace("{PASSWORD}", password);

            await _communicationService.SendEmail(user.Email, subject, body);            
        }

        #endregion


    }
}
