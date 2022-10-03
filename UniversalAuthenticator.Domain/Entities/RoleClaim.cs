using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class RoleClaim: BaseEntity<Int64>
    {
        public Guid RoleId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public Role Role { get; set; }
    }
}
