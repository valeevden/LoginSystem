using LoginSystem.Api.Models;
using LoginSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginSystem.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class UserController : ControllerBase
        {
            private IUserService _userService;
            private MemoryCacheSingleton _userCache;


            public LeadController(IMapper mapper,
                                    IUserService leadService,
                                    MemoryCacheSingleton userCache)
            {
                _leadService = leadService;
                _checker = checker;
                _cityService = cityService;
                _mapper = mapper;
                _emailMessage = emailMessage;
                _publishEndpoint = publishEndpoint;
            }

            /// <summary>Adds new user</summary>
            /// <param name="inputModel">Information about user to add</param>
            /// <returns>Information about added user</returns>
            [ProducesResponseType(typeof(LeadOutputModel), StatusCodes.Status200OK)]
            [ProducesResponseType(typeof(CustomExceptionOutputModel), StatusCodes.Status409Conflict)]
            [ProducesResponseType(typeof(CustomExceptionOutputModel), StatusCodes.Status404NotFound)]
            [AllowAnonymous]
            [HttpPost]
            public async Task<ActionResult<LoginModel>> AddUserAsync([FromBody] LoginModel inputModel)
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
        }
    }
}
