using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class EmailTemplate: BaseEntity<int>
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string EmailType { get; set; }
        public bool Status { get; set; }
    }
}
