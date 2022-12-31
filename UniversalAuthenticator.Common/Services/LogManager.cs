using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Interface;

namespace UniversalAuthenticator.Common.Services
{
    public class LogManager : ILogManager
    {
        public Task LogException(string message)
        {
            throw new NotImplementedException();
        }

        public Task LogMessage(string message)
        {
            throw new NotImplementedException();
        }

        public Task LogWarning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
