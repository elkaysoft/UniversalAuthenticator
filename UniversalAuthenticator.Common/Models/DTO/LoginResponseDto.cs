using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.DTO
{
    public class LoginResponseDto
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }
        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>The refresh token.</value>
        public string RefreshToken { get; set; }
        /// <summary>
        /// Gets or sets the expiry time.
        /// </summary>
        /// <value>The expires in.</value>
        public DateTime ExpiresIn { get; set; }
        /// <summary>
        /// Gets or sets the logged in user Permissions.
        /// </summary>
        /// <value>The Permission item</value>
        public List<PermissionDto> Permission { get; set; }
        /// <summary>
        /// Gets or sets the logged in user Roles.
        /// </summary>
        /// <value>The Role item</value>
        public string Roles { get; set; }
        public UserOnboardingDto User { get; set; }

    }
}
