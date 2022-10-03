using AutoMapper;
using UniversalAuthenticator.Common.Constants;
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

        public UserService(IRoleService roleService, IRepository<ApplicationUser> userRepository, IMapper mapper)
        {
            _roleService = roleService;
            _userRepository = userRepository;
            _mapper = mapper;
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
                await _userRepository.AddAsync(user);
                await _roleService.AddUserRole(request.Roles, user.Id);

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

        #endregion


    }
}
