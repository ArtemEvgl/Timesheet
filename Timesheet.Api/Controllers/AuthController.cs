using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Timesheet.Api.Models;
using Timesheet.BussinessLogic.Exceptions;
using Timesheet.Domain;

namespace Timesheet.Api.Controllers
{
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

        [HttpPost]
        public ActionResult<string> Login(LoginRequest request)
        {
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