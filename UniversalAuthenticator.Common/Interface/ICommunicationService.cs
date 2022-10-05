using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Common.Interface
{
    public interface ICommunicationService
    {
        Task<Tuple<bool, ErrorResponse>> SendEmail(string toEmail, string subject, string body);
        Task<Tuple<bool, ErrorResponse>> SendSms(string destinationPhone, string body);
    }
}
