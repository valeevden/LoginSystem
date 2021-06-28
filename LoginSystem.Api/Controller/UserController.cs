using LoginSystem.Services;
using LoginSystem.Services.Models;
using LoginSystem.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LoginSystem.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private MemoryCacheSingleton _userCache;

        public UserController(IUserService userService, MemoryCacheSingleton userCache)
        {
            _userService = userService;
            _userCache = userCache;
        }

        /// <summary>Register new User</summary>
        /// <param name="inputModel">information from user to register</param>
        /// <returns>return information about added user</returns>
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<int> Register([FromBody] LoginModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException();
            }
            var id = _userService.AddUser(inputModel);
            //var user = _userService.GetUserFromCache(key);
            return Ok(id);
        }

        /// <summary>Get User by Id</summary>
        /// <param name="id">user Id</param>
        /// <returns>return information about user</returns>
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id")]
        public ActionResult<UserModel> GetUserById([FromRoute] int Id)
        {
            var model = _userService.GetUserFromCacheById(Id);
            return model == null ? NotFound("Invalid User Id") : Ok(model);
        }
    }
}
