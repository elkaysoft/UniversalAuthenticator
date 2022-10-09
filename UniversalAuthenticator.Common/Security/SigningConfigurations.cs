using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Security
{
    public class SigningConfigurations
    {
        public SecurityKey securityKey { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(string key)
        {
            var key_in_bytes = Encoding.UTF8.GetBytes(key);

            securityKey = new SymmetricSecurityKey(key_in_bytes);
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
