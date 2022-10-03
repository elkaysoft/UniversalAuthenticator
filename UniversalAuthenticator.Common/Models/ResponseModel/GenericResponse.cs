using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.ResponseModel
{
    public class GenericResponse
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class GenericResponse<T>: GenericResponse
    {
        public T data { get; set; }
    }

}
