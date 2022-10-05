using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Models.DTO;

namespace UniversalAuthenticator.Common.Interface
{
    public interface IConfigurationService
    {
        Task<Tuple<bool, EmailTemplateDto>> GetEmailTemplate();
    }
}
