using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class ConfigurationService : IConfigurationService
    {
        private readonly IRepository<EmailTemplate> _emailTemplateRepo;
        private readonly IRepository<SMSTemplate> _smsTemplateRepo;
        private readonly IRepository<SystemConfiguration> _systemConfigRepository;
        private readonly IMapper _mapper;

        public ConfigurationService(IRepository<EmailTemplate> emailTemplateRepo, IMapper mapper, IRepository<SMSTemplate> smsTemplateRepo, IRepository<SystemConfiguration> systemConfigRepository)
        {
            _emailTemplateRepo = emailTemplateRepo;
            _mapper = mapper;
            _smsTemplateRepo = smsTemplateRepo;
            _systemConfigRepository = systemConfigRepository;
        }

        public async Task<Tuple<bool, EmailTemplateDto>> GetEmailTemplate(string emailType)
        {
            bool status = false;
            var result = new EmailTemplateDto();

            try
            {
                var data = await _emailTemplateRepo.GetFirstAsync(x => x.EmailType == emailType && x.Status);
                if(data != null)
                {
                    status = true;
                    result = _mapper.Map<EmailTemplateDto>(data);
                }
            }
            catch(Exception ex)
            {
                status = false;
            }

            return new Tuple<bool, EmailTemplateDto>(status, result);
        }

        public async Task<Tuple<bool, SmsTemplateDto>> GetSmsTemplate(string smsType)
        {
            bool status = false;
            var result = new SmsTemplateDto();

            try
            {
                var data = await _smsTemplateRepo.GetFirstAsync(x => x.SMSType == smsType && x.Status);
                if (data != null)
                {
                    status = true;
                    result = _mapper.Map<SmsTemplateDto>(data);
                }
            }
            catch (Exception ex)
            {
                status = false;
            }

            return new Tuple<bool, SmsTemplateDto>(status, result);
        }

        public async Task<Tuple<ErrorResponse, GenericResponse<SystemConfigurationDto>>> GetSystemConfiguration()
        {
            var error = new ErrorResponse();
            var result = new GenericResponse<SystemConfigurationDto>();

            try 
            {
                var config = await _systemConfigRepository.GetFirstAsync(x => !x.IsDeleted);
                if(config == null)
                    throw new CustomException(CustomCodes.ModelValidationError, CustomMessages.SystemConfigNotFound);

                var data = _mapper.Map<SystemConfigurationDto>(config);
                result.data = data;
                result.message = CustomMessages.Successful;
                result.code = CustomCodes.Successful;
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

            return new Tuple<ErrorResponse, GenericResponse<SystemConfigurationDto>>(error, result);
        }

        public async Task<Tuple<ErrorResponse, GenericResponse<bool>>> UpdateSystemConfiguration(UpdateSystemConfigRequest model)
        {
            var error = new ErrorResponse();    
            var result = new GenericResponse<bool>(); 
            
            try
            {
                var config = await _systemConfigRepository.GetFirstAsync();

                config.EnforceMultiFactorAuthentication = model.EnforceMultiFactorAuthentication;
                config.OTPExpirationPeriod = model.OTPExpirationPeriod;
                config.UserPasswordExpiryInDays = model.UserPasswordExpiryInDays;
                config.TempPasswordExpirationPeriod = model.TempPasswordExpirationPeriod;
                config.EnforceUserPasswordExpiry = model.EnforceUserPasswordExpiry;
                config.DateModified = DateTime.Now;
                config.ModifiedBy = "";
                config.ModifiedByIp = "";
                config.MaximumPasswordTries = model.MaximumPasswordTries;
                config.MaximumSecurityQuestionTries = model.MaximumSecurityQuestionTries;

                await _systemConfigRepository.UpdateAsync(config);

                result.data = true;
                result.message = CustomMessages.Successful;
                result.code = CustomCodes.Successful;

            }
            catch(Exception ex)
            {
                //log exception
                ex.ToString();
                error.Errors.Add(new ErrorModel
                {
                    code = CustomCodes.Something_went_wrong,
                    message = CustomMessages.SomethingWentWrong
                });
            }

            return new Tuple<ErrorResponse, GenericResponse<bool>>(error, result);
        }

    }
}
