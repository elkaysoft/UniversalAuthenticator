using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Interface;

namespace UniversalAuthenticator.Common.Services
{
    public class RequestTransmitter : IRequestTransmitter
    {
        public string RequestingUserName { get; set; }
        public string RequestingIPAddress { get; set; }
        public string Authorization { get; set; }
        public Guid RequestingUserId { get; set; }
    }
}
