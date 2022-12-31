using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UniversalAuthenticator.Common.Enums
{
    public enum ValidationTokenEnum
    {
        [Display(Name = "ChangePassword")]
        ChangePassword,
        [Display(Name = "OTP")]
        OneTimePassword,
        [Display(Name = "PasswordReset")]
        PasswordReset       
    }
}
