using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class Permission: BaseEntity<int>
    {
        public string GroupName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
    }
}
