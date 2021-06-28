using LoginSystem.Services;
using LoginSystem.Services.Models;
using LoginSystem.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        /// <summary>user registration</summary>
        /// <param name="inputModel">information from user to register</param>
        /// <returns>return information about added user</returns>
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost("register")]
        [AllowAnonymous]
        public ActionResult<UserModel> Register([FromBody] LoginModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException();
            }
            var outputModel = _userService.AddUser(inputModel);
            var user = _userService.GetUserById(id);
            var outputModel = _mapper.Map<UserOutputModel>(user);
            return Ok(outputModel);
        }

        /// <summary>Adds new user</summary>
        /// <param name="inputModel">Information about user to add</param>
        /// <returns>Information about added user</returns>
        [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CustomExceptionOutputModel), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(CustomExceptionOutputModel), StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<UserModel>> AddUserAsync([FromBody] LoginModel inputModel)
        {
            if (!ModelState.IsValid)
                throw new CustomValidationException(ModelState);

            if (await _cityService.GetCityByIdAsync(inputModel.CityId) == null)
                return NotFound(new CustomExceptionOutputModel
                {
                    Code = StatusCodes.Status404NotFound,
                    Message = string.Format(Constants.ERROR_CITY_NOT_FOUND, inputModel.CityId)
                });

            var dto = _mapper.Map<LeadDto>(inputModel);
            var addedLeadId = await _leadService.AddLeadAsync(dto);
            var outputModel = _mapper.Map<LeadOutputModel>(await _leadService.GetLeadByIdAsync(addedLeadId));
            return Ok(outputModel);

        }

        //if (!_userCache.Cache.TryGetValue(userInfoFromForm.Login, out UserModel cashedModel))
        //{
        //    return NotFound("Invalid Memory Cache key");
        //}
    }
}
