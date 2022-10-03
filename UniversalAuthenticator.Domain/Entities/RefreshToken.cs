using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class RefreshToken: BaseEntity<Int64>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public ApplicationUser User { get; set; }
    }
}
