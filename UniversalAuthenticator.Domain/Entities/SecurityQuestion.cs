using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class SecurityQuestion: BaseEntity<Guid>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
