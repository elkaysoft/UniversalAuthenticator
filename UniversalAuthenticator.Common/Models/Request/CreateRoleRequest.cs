using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.Request
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> Permission { get; set; }

    }
}
