using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Enums
{
    public enum EmailTemplateEnum
    {
        [Display(Name = "Login")]
        Login,
        [Display(Name = "Onboarding")]
        Onboarding,
        [Display(Name = "Reset Password")]
        PasswordReset,
        [Display(Name = "MultiFactor Authentication")]
        MFA
    }
}
