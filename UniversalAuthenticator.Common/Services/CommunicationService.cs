using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Enums;
using UniversalAuthenticator.Common.Extensions;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Common.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpService _httpService;

        public CommunicationService(IOptions<AppSettings> appSettings, IHttpService httpService)
        {
            _appSettings = appSettings.Value;
            _httpService = httpService;
        }

        public async Task<Tuple<bool, ErrorResponse>> SendEmail(string toEmail, string subject, string body)
        {
            bool result = false;
            var error = new ErrorResponse();

            try
            {
                var destinationEmail = new List<string>();
                destinationEmail.Add(toEmail);

                var emailObj = new
                {
                    from = _appSettings.DefaultEmailSender,
                    ccs = new List<string>(),
                    tos = destinationEmail,
                    subject = subject,
                    body = body
                };

                string json = JsonConvert.SerializeObject(emailObj);
                await _httpService.MakeRequest(_appSettings.EmailApiBaseUrl, "/Email/Send", json, GetCustomHeaders());
                result = true;
            }
            catch(Exception ex)
            {
                //log exceptions
                ex.ToString();
                result = false;
            }

            return new Tuple<bool, ErrorResponse>(result, error);
        }

        public Task<Tuple<bool, ErrorResponse>> SendSms(string destinationPhone, string body)
        {
            throw new NotImplementedException();
        }

        #region Private Implementation
        private Dictionary<string, string> GetCustomHeaders(Dictionary<string, string> additonalHeaders = null)
        {
            var headers = new Dictionary<string, string>();
            //if (_requestDataTransmitter.Authorization == null)
            //    _requestDataTransmitter.Authorization = $"Bearer {ExtensionsService.GenerateAlphaNumeric(12)}";

            //headers.Add(CustomHeaders.AuthorizationHeaderKey, _requestDataTransmitter.Authorization);
            
            //Set Default headers
            headers.Add("Accept", "application/json");
            headers.Add("AppId", "TEST");
            headers.Add("AppKey", "TEST");
            if (additonalHeaders != null)
            {
                foreach (var item in additonalHeaders)
                {
                    headers.Add(item.Key, item.Value);
                }
            }
            return headers;

        }
        #endregion

    }
}
