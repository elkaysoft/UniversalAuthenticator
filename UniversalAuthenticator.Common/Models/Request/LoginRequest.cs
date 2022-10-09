using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Enums;

namespace UniversalAuthenticator.Common.Models.Request
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public PlatformTypeEnum Platform { get; set; }
    }
}
