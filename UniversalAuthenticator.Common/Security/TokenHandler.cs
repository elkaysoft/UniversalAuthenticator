using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Security.Tokens;
using UniversalAuthenticator.Domain.Entities;


namespace UniversalAuthenticator.Common.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly ISet<Tokens.RefreshToken> _refreshTokens = new HashSet<Tokens.RefreshToken>();

        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;

        public TokenHandler(TokenOptions tokenOptions, SigningConfigurations signingConfigurations)
        {
            _tokenOptions = tokenOptions;
            _signingConfigurations = signingConfigurations;
        }        
        

        public AccessToken CreateAccessToken(ApplicationUser user, List<string> permissions)
        {
            var refreshToken = GenerateRefreshToken();
            var accessToken = GenerateAccessToken(user, permissions, refreshToken);
            _refreshTokens.Add(refreshToken);

            return accessToken;
        }

        public void RevokeRefreshToken(string token)
        {
            TakeRefreshToken(token);
        }

        public Tokens.RefreshToken TakeRefreshToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var refreshTokens = _refreshTokens.SingleOrDefault(t => t.Token == token);
            if(refreshTokens != null)
                _refreshTokens.Remove(refreshTokens);

            return refreshTokens;
        }

        #region Private Implementation
        private Tokens.RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new Tokens.RefreshToken
            (
                token: BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString()),
                expiration: DateTime.UtcNow.AddMinutes(_tokenOptions.RefreshTokenExpiration)
            );

            return refreshToken;
        }

        private AccessToken GenerateAccessToken(ApplicationUser user, List<string> permissions, Tokens.RefreshToken refreshToken)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: accessTokenExpiration,
                signingCredentials: _signingConfigurations.SigningCredentials,
                claims: GenerateClaims(user, permissions)
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration, refreshToken);
        }

        private Claim[] GenerateClaims(ApplicationUser user, List<string> permissions)
        {
            var claim = new[]
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("IssueDate", user.LastLoginTime.ToString()),
                new Claim("Permission", String.Join(",", permissions)),
            };

            return claim;
        }

        #endregion


    }
}
