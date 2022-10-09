using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Enums
{
    public enum MultiFactorAuthenticationEnum
    {
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Sms")]
        Sms,
        [Display(Name = "Email_and_Sms")]
        EmailSms,
        [Display(Name = "Token")]
        Token

    }
}
