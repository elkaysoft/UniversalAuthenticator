using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;
using UniversalAuthenticator.Domain.Entities;


namespace UniversalAuthenticator.Common.Services
{
    public class UserService: IUserService
    {
        private readonly IRoleService _roleService;

        public UserService(IRoleService roleService)
        {
            _roleService = roleService;
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

        #endregion


    }
}
