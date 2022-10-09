using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.DTO
{
    public class BaseDto
    {
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedByIp { get; set; }
    }
}
