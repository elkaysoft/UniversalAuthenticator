using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class ApplicationUserRole: BaseEntity<Guid>
    {
        public Guid RoleId { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
