using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace UniversalAuthenticator.Common.Interface
{
    public interface IHttpService
    {
        Task<RestResponse> MakeRequest(string baseUrl, string resourceUrl, object payload, Dictionary<string, string> headerValues = null);
    }
}
