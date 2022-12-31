using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Common.Interface
{
    public interface IConfigurationService
    {
        Task<Tuple<bool, EmailTemplateDto>> GetEmailTemplate(string emailType);

        Task<Tuple<bool, SmsTemplateDto>> GetSmsTemplate(string smsType);

        Task<Tuple<ErrorResponse, SystemConfigurationDto>> GetSystemConfiguration();

        Task<Tuple<ErrorResponse, GenericResponse<bool>>> UpdateSystemConfiguration(UpdateSystemConfigRequest model);
    }
}
