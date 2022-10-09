using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Security.Tokens
{
    public class RefreshToken : JsonWebToken
    {
        public RefreshToken(string token, DateTime expiration) : base(token, expiration)
        {
        }
    }
}
