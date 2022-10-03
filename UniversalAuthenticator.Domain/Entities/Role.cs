using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class Role: BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public RoleClaim RoleClaim { get; set; }
    }
}
