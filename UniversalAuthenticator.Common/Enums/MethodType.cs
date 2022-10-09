using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Enums
{
    public enum MethodType
    {
        [Display(Name = "POST")]
        POST,
        [Display(Name = "GET")]
        GET,
        [Display(Name = "PUT")]
        PUT,
        [Display(Name = "DELETE")]
        DELETE
    }
}
