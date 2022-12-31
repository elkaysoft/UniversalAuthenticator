using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Interface
{
    public interface ILogManager
    {
        Task LogException(string message);
        Task LogWarning(string message);
        Task LogMessage(string message);
    }
}
