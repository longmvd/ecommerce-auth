using AuthService.BL.AuthBL;
using AuthService.Common.Records;
using ECommerce.Common.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthBL _authBL;

        public AuthsController(IAuthBL authBL)
        {
            _authBL = authBL;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if(loginRequest == null)
            {
                return BadRequest();
            }
            var response = await _authBL.Login(loginRequest);

            return Ok(response);
        }

        [HttpPost("token-validation")]
        public async Task<IActionResult> VaidateToken([FromBody]string token)
        {
            if (token == null)
            {
                return BadRequest();
            }
            var response = _authBL.ValidateToken(token);

            return Ok(response);
        }

        [HttpPost("token-info")]
        public async Task<IActionResult> GetInfoFromToken([FromBody] string token)
        {
            if (token == null)
            {
                return BadRequest();
            }
            var response = new ServiceResponse();
            
            var info = _authBL.GetInfoFromToken(token?.ToString());

            if(info == null)
            {
                response.OnError(info);
                return BadRequest(response);
            }
            else
            {
                response.OnSuccess(info);
            }

            return Ok(response);
        }

    }
}
