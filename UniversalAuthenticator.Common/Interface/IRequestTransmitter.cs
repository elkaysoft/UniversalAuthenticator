using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Interface
{
    public interface IRequestTransmitter
    {
        public Guid RequestingUserId { get; set; }
        public string RequestingUserName { get; set; }
        public string RequestingIPAddress { get; set; }
        public string Authorization { get; set; }
    }
}
