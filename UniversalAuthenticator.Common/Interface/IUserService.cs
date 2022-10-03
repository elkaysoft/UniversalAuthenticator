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
    public interface IUserService
    {
        Task<Tuple<ErrorResponse, GenericResponse<UserOnboardingDto>>> OnboardUser(CreateUserRequest request);
    }
}
