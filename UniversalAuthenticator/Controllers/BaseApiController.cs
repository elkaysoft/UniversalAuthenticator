using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ErrorResponse GetValidationErrors(List<ValidationFailure> failures)
        {
            var errors = failures.Select(x => new ErrorModel
            {
                code = x.ErrorCode,
                message = x.ErrorMessage
            }).ToList();

            return new ErrorResponse { Errors = errors };
        }
    }
}
