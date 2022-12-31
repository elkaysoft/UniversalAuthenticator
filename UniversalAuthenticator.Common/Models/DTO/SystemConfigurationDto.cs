using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.DTO
{
    public class SystemConfigurationDto: BaseDto
    {
        public int MaximumPasswordTries { get; set; }
        public int MaximumSecurityQuestionTries { get; set; }
        public int OTPExpirationPeriod { get; set; }
        public int TempPasswordExpirationPeriod { get; set; }
        public bool EnforceMultiFactorAuthentication { get; set; }
        public bool EnforceUserPasswordExpiry { get; set; }
        public int UserPasswordExpiryInDays { get; set; }
        public int ValidationTokenExpiration { get; set; }
    }
}
