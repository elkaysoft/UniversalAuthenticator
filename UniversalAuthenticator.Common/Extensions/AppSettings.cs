using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Extensions
{
    public class AppSettings
    {
        public string BlacklistedUsernameCharacters { get; set; }
        public string EmailApiBaseUrl { get; set; }
        public string SmsApiBaseUrl { get; set; }
    }
}
