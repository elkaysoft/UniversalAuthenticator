using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class ValidationToken: BaseEntity<Int64>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string TokenType { get; set; }
        public DateTime DateGenerated { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
