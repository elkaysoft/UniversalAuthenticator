using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Common.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly AppSettings _appSettings;

        public CommunicationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public Task<Tuple<bool, ErrorResponse>> SendEmail(string toEmail, string subject, string body)
        {
            bool result = false;
            var error = new ErrorResponse();

            try
            {
                result = true;
            }
            catch(Exception ex)
            {

            }

            return new Task<Tuple<bool, ErrorResponse>>
        }

        public Task<Tuple<bool, ErrorResponse>> SendSms(string destinationPhone, string body)
        {
            throw new NotImplementedException();
        }
    }
}
