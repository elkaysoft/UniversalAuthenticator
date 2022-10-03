using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Domain.Entities
{
    public class BaseEntity<TId>
    {
        public BaseEntity()
        {
            DateCreated = DateTime.Now; 
            IsDeleted = false;
        }

        public TId Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedByIp { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedByIp { get; set; }
        public bool IsDeleted { get; set; }
    }
}
