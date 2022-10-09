using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalAuthenticator.Common.Models.DTO
{
    public class SmsTemplateDto: BaseDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SMSType { get; set; }
        public bool Status { get; set; }
    }
}
