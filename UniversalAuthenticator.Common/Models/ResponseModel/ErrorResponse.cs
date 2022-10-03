using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.ResponseModel
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new List<ErrorModel>();
        }

        public List<ErrorModel> Errors { get; set; }
    }

    public class ErrorModel
    {
        public string code { get; set; }
        public string message;
    }
}
