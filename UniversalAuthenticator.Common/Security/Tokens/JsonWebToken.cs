using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Security.Tokens
{
    public class JsonWebToken
    {
        public string Token { get; protected set; }
        public DateTime Expiration { get; protected set; }

        public JsonWebToken(string token, DateTime expiration)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid Token");

            if (expiration == DateTime.MinValue)
                throw new ArgumentException("Invalid Expiration time");

            Token = token;
            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.Now > Expiration;
    }
}
