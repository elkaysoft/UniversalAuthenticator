using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Enums
{
    public enum PlatformTypeEnum
    {
        [Display(Name = "Desktop")]
        Desktop,
        [Display(Name = "Mobile")]
        Mobile,
        [Display(Name = "Web")]
        Web       
    }
}
