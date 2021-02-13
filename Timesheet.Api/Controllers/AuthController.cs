using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Timesheet.Api.Models;
using Timesheet.BussinessLogic.Exceptions;
using Timesheet.Domain;

namespace Timesheet.Api.Controllers
{
    /// <summary>
    /// Controller to work with auth service
    /// </summary>
    /// <remarks>Test controllers text</remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IOptions<JwtConfig> _jwtConfig;

        public AuthController(IAuthService authService, IOptions<JwtConfig> jwtConfig)
        {
            _authService = authService;
            _jwtConfig = jwtConfig;
        }

        /// <summary>
        /// Login in timesheet api
        /// </summary>
        /// <remarks>Test methods text</remarks>
        /// <param name="request">login request</param>
        /// <returns>jwt token</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public ActionResult<string> Login(LoginRequest request)
        {
            if (ModelState.IsValid == false)
                return BadRequest();

            try
            {
                var secret = _jwtConfig.Value.Secret;
                var token = _authService.Login(request.LastName, secret);

                return Ok(token);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}