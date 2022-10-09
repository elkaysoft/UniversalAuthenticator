using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Interface;

namespace UniversalAuthenticator.Common.Services
{
    public class HttpService : IHttpService
    {
        public async Task<RestResponse> MakeRequest(string baseUrl, string resourceUrl, object payload, Dictionary<string, string> headerValues = null)
        {
            var client = new RestClient(baseUrl);

            var request = new RestRequest(resourceUrl);
            if (headerValues != null && headerValues.Count() > 0)
            {
                foreach (var header in headerValues)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            request.AddBody(payload);
            var response = await client.ExecuteAsync(request);

            return response;
        }
    }
}
