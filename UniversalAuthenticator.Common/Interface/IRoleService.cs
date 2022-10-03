using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Common.Interface
{
    public interface IRoleService
    {
        Task<List<RoleDto>> ValidateApplicationRole(List<Guid> roles);

        Task<Tuple<GenericResponse<bool>, ErrorResponse>> AddRole(CreateRoleRequest request);

        Task AddUserRole(List<Guid> roleIds, Guid userId);

    }
}
