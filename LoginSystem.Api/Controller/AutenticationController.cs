using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginSystem.Services.Models;


namespace LoginSystem.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authService;
        private ISecurityService _securityService;

        public AuthenticationController(IAuthenticationService authService, ISecurityService securityService)
        {
            _securityService = securityService;
            _authService = authService;
        }
        /// <summary>Log in</summary>
        /// <param name="model">User credentials</param>
        /// <returns>Token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomExceptionOutputModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthenticationResponse>> Authentificate([FromBody] LoginModel model)
        {
            var User = await _authService.GetAuthenticatedUserAsync(model.Login);
            if (User == null)
                return NotFound(new CustomExceptionOutputModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = string.Format(Constants.ERROR_User_NOT_FOUND_BY_LOGIN, model.Login)
                });

            if (!_securityService.VerifyPassword(User.Password, model.Password))
                throw new WrongCredentialsException();

            var token = _authService.GenerateToken(User);
            return Ok(token);
        }
    }
}

//_validatedModelCache = MemoryCacheSingleton.GetCacheInstance();
