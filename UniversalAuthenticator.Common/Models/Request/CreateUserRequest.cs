using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Enums;

namespace UniversalAuthenticator.Common.Models.Request
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? OtherName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<Guid> Roles { get; set; } = new List<Guid>();
    }
}
