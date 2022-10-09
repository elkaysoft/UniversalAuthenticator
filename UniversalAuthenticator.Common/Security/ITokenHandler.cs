using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Security.Tokens;
using UniversalAuthenticator.Domain.Entities;
using RefreshToken = UniversalAuthenticator.Common.Security.Tokens.RefreshToken;

namespace UniversalAuthenticator.Common.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(ApplicationUser user, List<string> permissions);
        RefreshToken TakeRefreshToken(string token);
        void RevokeRefreshToken(string token);
    }
}
