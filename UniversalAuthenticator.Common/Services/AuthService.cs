using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Constants;
using UniversalAuthenticator.Common.Enums;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;
using UniversalAuthenticator.Common.Security;
using UniversalAuthenticator.Domain.Data;
using UniversalAuthenticator.Domain.Entities;

namespace UniversalAuthenticator.Common.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<ApplicationUser> _appUserRepository;
        private readonly IRepository<RoleClaim> _roleClaim;
        private readonly IRepository<Permission> _permissionRepository;
        private readonly IRepository<ValidationToken> _validationTokenRepository;
        private readonly IRequestTransmitter _requestTransmitter;
        private readonly ILogManager _logManager;
        private readonly IConfigurationService _configurationService;
        private readonly ICommunicationService _communicationService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly ITokenHandler _tokenHandler;

        private readonly IHttpContextAccessor _httpContextAccessor;


        public AuthService(IRepository<ApplicationUser> appUserRepository, IRequestTransmitter requestTransmitter, ILogManager logManager,
            IConfigurationService configurationService, ICommunicationService communicationService, IRepository<RoleClaim> roleClaim,
            IRepository<Permission> permissionRepository, IMapper mapper, IOptions<AppSettings> appSettings, IRepository<ValidationToken> validationTokenRepository, IHttpContextAccessor httpContextAccessor, ITokenHandler tokenHandler)
        {
            _appUserRepository = appUserRepository;
            _requestTransmitter = requestTransmitter;
            _logManager = logManager;
            _configurationService = configurationService;
            _communicationService = communicationService;
            _roleClaim = roleClaim;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _validationTokenRepository = validationTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenHandler = tokenHandler;

            var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                _requestTransmitter.RequestingIPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _requestTransmitter.RequestingUserName = identity?.Name;
            }            
        }

        public async Task<Tuple<GenericResponse<LoginResponseDto>, ErrorResponse>> Authenticate(LoginRequest model)
        {
            var result = new GenericResponse<LoginResponseDto>();
            var error = new ErrorResponse();

            try
            {
                var tokenResult = new LoginResponseDto();

                var user = await _appUserRepository.GetSingleAsync(x => x.Username == model.username && !x.IsDeleted);
                if(user == null)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.UserNotFound);

                if(!user.IsActive)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.AccountInactive);

                var (isExist, emailTemplate) = await _configurationService.GetEmailTemplate(EmailTemplateEnum.Login.DisplayName());
                if(!isExist)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.EmailTemplateNotFound);

                var (configError, configuration) = await _configurationService.GetSystemConfiguration();
                if(configError.Errors.Count() > 0)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.SystemConfigNotFound);

                if(user.PasswordTries >= configuration.MaximumPasswordTries)
                {
                    user.LockoutEnabled = true;
                    await _appUserRepository.UpdateAsync(user);
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.Phone_already_exists);
                }

                if(!IsPasswordValid(model.password, user.SaltedHashedPassword))
                {
                    user.PasswordTries++;
                    await _appUserRepository.UpdateAsync(user);

                    int passwordTrialLeft = configuration.MaximumPasswordTries - user.PasswordTries;
                    string attemptMsg = passwordTrialLeft <= 1 ? "attempt" : "attempts";
                    throw new CustomException(CustomCodes.ModelValidationError, $"{CustomMessages.InvalidLogin} You have {passwordTrialLeft} more {attemptMsg}");
                }

                //check if the user is yet to change password
                if (user.ForceChangeOfPassword)
                {
                    var passwordExpiration = user.DateCreated.AddHours(configuration.TempPasswordExpirationPeriod);
                    if (DateTime.Now > passwordExpiration)
                        throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.TemporaryPasswordExpired);

                    result.code = CustomCodes.ForceChangePasswordRequired;
                    result.message = CustomMessages.PromptChangePassword;
                    return new Tuple<GenericResponse<LoginResponseDto>, ErrorResponse>(result, error);
                }

                //check if multi-factor authentication is enabled
                if (user.EnableMultiFactorAuthentication)
                {
                    await HandleMultiFactorAuthentication(user.MultiFactorAuthenticationType, user, configuration);                    
                    result.code = CustomCodes.MultiFactorAuthenticationRequired;
                    result.message = CustomMessages.PromptMultifactorAuthentication;
                    return new Tuple<GenericResponse<LoginResponseDto>, ErrorResponse>(result, error);
                }                   

                if (user.PasswordTries > 0)
                    user.PasswordTries = 0;

                user.LastLoginTime = DateTime.UtcNow;

                var userRole = user.UserRole;
                var roleIds = string.Join(",", userRole.Select(x => x.RoleId).ToList());
                var claimList = await _roleClaim.ListAllAsync(x => roleIds.Contains(x.RoleId.ToString()));
                var permissionList = new List<PermissionDto>();
                if (claimList.Any())
                {
                    foreach (var claim in claimList)
                    {                       
                        var permissions = await GetRolesPermission(claim.ClaimValue);
                        permissionList.AddRange(permissions);
                    }                    
                }

                //get user details                
                var userMapping = _mapper.Map<UserOnboardingDto>(user);
                var tokenObj = _tokenHandler.CreateAccessToken(user, permissionList.Select(x => x.Code).ToList());

                tokenResult.RefreshToken = tokenObj.RefreshToken.Token;
                tokenResult.AccessToken = tokenObj.Token;
                tokenResult.Permission = permissionList;
                tokenResult.ExpiresIn = tokenObj.Expiration;
                tokenResult.User = _mapper.Map<UserOnboardingDto>(user);
                tokenResult.Roles = roleIds;

                result.data = tokenResult;
                result.code = CustomCodes.Successful;
                result.message = CustomMessages.Successful;
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
                await _logManager.LogException(ex.ToString());
                error.Errors.Add(new ErrorModel
                {
                    code = CustomCodes.Something_went_wrong,
                    message = CustomMessages.SomethingWentWrong
                });
            }

            return new Tuple<GenericResponse<LoginResponseDto>, ErrorResponse>(result, error);
        }

        public async Task<Tuple<GenericResponse, ErrorResponse>> RefreshToken(string refreshToken, string username)
        {
            var response = new GenericResponse();
            var error = new ErrorResponse();

            try
            {
                var token = _tokenHandler.TakeRefreshToken(refreshToken);
                if (token == null)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.InvalidRefreshToken);

                if(token.IsExpired())
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.InvalidRefreshToken);

                var user = await _appUserRepository.GetSingleAsync(x => x.Username == username);
                if(user == null)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.InvalidUsername);

                var accessToken = _tokenHandler.CreateAccessToken(user, new List<string>());
            }
            catch(CustomException cEx)
            {
                error.Errors.Add(new ErrorModel
                {
                    code = cEx.ErrorCode,
                    message = cEx.Message,
                });
            }
            catch(Exception ex)
            {
                await _logManager.LogException(ex.ToString());
                error.Errors.Add(new ErrorModel
                {
                    code = CustomCodes.Something_went_wrong,
                    message = CustomMessages.SomethingWentWrong
                });
            }           


            return new Tuple<GenericResponse, ErrorResponse>(response, error);
        }

        

        #region Private Methods
        private bool IsPasswordValid(string plainPassword, string hashedPassword)
        {
            if (BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword))
                return true;
            return false;
        }

        private async Task HandleMultiFactorAuthentication(string mfaType, ApplicationUser user, SystemConfigurationDto configuration)
        {
            switch (mfaType.ToLower())
            {
                case "email":
                    //send mfa email
                    await InitiateMFA_Email(user.Email, user.Id, configuration);
                    break;
                case "sms":
                    await InitiateMFA_Sms(user.PhoneNumber, user.Id, configuration);
                    break;
            }
        }

        private async Task InitiateMFA_Email(string recepient, Guid userId, SystemConfigurationDto config)
        {
            var emailTemplate = await _configurationService.GetEmailTemplate(EmailTemplateEnum.MFA.DisplayName());
            if (emailTemplate.Item1)
            {
                int token = ExtensionsService.GenerateRandomNumbers(8);

                var validationToken = new ValidationToken
                {
                    CreatedBy = _requestTransmitter.RequestingUserName,
                    DateCreated = DateTime.Now,
                    DateGenerated = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddMinutes(config.ValidationTokenExpiration),
                    IsDeleted = false,
                    Token = BCrypt.Net.BCrypt.HashPassword(token.ToString()),
                    UserId = userId,
                    TokenType = ValidationTokenEnum.OneTimePassword.DisplayName()
                };
                await _validationTokenRepository.AddAsync(validationToken); 

                var templateInfo = emailTemplate.Item2;
                var body = templateInfo.Body;
                body = body.Replace("[token]", token.ToString());
                await _communicationService.SendEmail(recepient, templateInfo.Subject, body);               
            }
        }

        private async Task InitiateMFA_Sms(string recepient, Guid userId, SystemConfigurationDto config)
        {
            var emailTemplate = await _configurationService.GetEmailTemplate(EmailTemplateEnum.MFA.DisplayName());
            if (emailTemplate.Item1)
            {
                int token = ExtensionsService.GenerateRandomNumbers(8);

                var validationToken = new ValidationToken
                {
                    CreatedBy = "SYSTEM",
                    DateCreated = DateTime.Now,
                    DateGenerated = DateTime.Now,
                    ExpiryDate = DateTime.Now.AddMinutes(config.ValidationTokenExpiration),
                    IsDeleted = false,
                    Token = token.ToString(),
                    UserId = userId,
                    TokenType = ValidationTokenEnum.OneTimePassword.DisplayName()
                };
                await _validationTokenRepository.AddAsync(validationToken);

                var templateInfo = emailTemplate.Item2;
                var body = templateInfo.Body;
                body = body.Replace("[token]", token.ToString());
                await _communicationService.SendEmail(recepient, templateInfo.Subject, body);
            }
        }

        private async Task<string> SetPasswordChangeToken(string username, string tempPassword, DateTime tokenExpiration)
        {
            var dataToEncrypt = $"{username}|{tempPassword}";
            var recordToHash = BCrypt.Net.BCrypt.HashPassword(tempPassword);
                        

            var changePasswordValidationToken = new ValidationToken
            {
                Token = recordToHash,
                UserId = _requestTransmitter.RequestingUserId,
                CreatedBy = _requestTransmitter.RequestingUserName,
                CreatedByIp = _requestTransmitter.RequestingIPAddress,
                DateCreated = DateTime.Now,
                IsDeleted = false,
                TokenType = ValidationTokenEnum.ChangePassword.DisplayName(),
                DateGenerated = DateTime.Now, 
                ExpiryDate = tokenExpiration                
            };

            string encryptedText = ExtensionsService.Encrypt(dataToEncrypt, _appSettings.PasswordChangeKey);
            await _validationTokenRepository.AddAsync(changePasswordValidationToken);
            return ExtensionsService.Encode("");
        }


        private async Task<List<PermissionDto>> GetRolesPermission(string claim)
        {
            var result = new List<PermissionDto>();
            var unpackedPermissions = PermissionPackers.UnPackPermissionValuesFromString(claim).ToList();
            result = await GetUserPermissions(unpackedPermissions);

            return result;
        }

        private async Task<List<PermissionDto>> GetUserPermissions(List<string> permission)
        {
            var result = new List<PermissionDto>();

            if (permission.Count > 0)
            {
                string permission_csv = string.Join(",", permission);
                var perm = await _permissionRepository.ListAsync(x => permission_csv.Contains(x.Code));                
                result = _mapper.Map<List<PermissionDto>>(perm.ToList());
            }

            return result;
        }

        #endregion

    }
}
