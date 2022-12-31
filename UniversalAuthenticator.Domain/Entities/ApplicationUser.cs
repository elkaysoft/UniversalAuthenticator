using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class ApplicationUser : BaseEntity<Guid>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OtherName { get; set; }        
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public string SaltedHashedPassword { get; set; }
        public int PasswordTries { get; set; }
        public bool HasSecurityQuestion { get; set; }
        public bool ForceChangeOfPassword { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? EmailConfirmationDate { get; set; }
        public DateTime? PhoneNumberConfirmationDate { get; set; }
        public bool EnableMultiFactorAuthentication { get; set; }
        public string? MultiFactorAuthenticationType { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ApplicationUserRole> UserRole { get; set; }

        public ICollection<RefreshToken> refreshTokens { get; set; }
    }
}
