using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Models.DTO;

namespace UniversalAuthenticator.Common.Interface
{
    public interface IRoleService
    {
        Task<List<RoleDto>> ValidateApplicationRole(List<Guid> roles);

    }
}
