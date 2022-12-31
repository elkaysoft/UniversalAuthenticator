using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversalAuthenticator.Common.Interface;
using UniversalAuthenticator.Common.Models.DTO;
using UniversalAuthenticator.Common.Models.Request;
using UniversalAuthenticator.Common.Models.ResponseModel;

namespace UniversalAuthenticator.Api.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    /// <returns></returns>

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseApiController
    {
        private readonly IValidator<LoginRequest> _loginValidator;
        private readonly IAuthService _authService;

        public AuthenticationController(IValidator<LoginRequest> loginValidator, IAuthService authService)
        {
            _loginValidator = loginValidator;
            _authService = authService;
        }

        /// <summary>
        /// Login to generate bearer token
        /// </summary>        
        /// <returns></returns>    
        /// <response code="200">Returns a bearer token</response>
        /// <response code="400">If authentication fails</response>
        [AllowAnonymous]
        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Produces("application/json")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var validationResult = await _loginValidator.ValidateAsync(request);
            if(!validationResult.IsValid)
                return BadRequest(GetValidationErrors(validationResult.Errors));

            var (response, error) = await _authService.Authenticate(request);
            if (error != null && error.Errors.Count > 0)
                return BadRequest(error);

            return Ok(response);
        }

    }
}
