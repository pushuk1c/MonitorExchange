using Microsoft.AspNetCore.Mvc;
using MonitorExchange.Data;
using MonitorExchange.Dtos.User;
using MonitorExchange.Models;

namespace MonitorExchange.Controllers.Core
{
    [ApiController]
    [Route("api/core/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo) { _authRepo = authRepo; }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<Guid>>> Register(UserRegisterDto request)
        {
            var respons = await _authRepo.Register(new User { UserName = request.Username }, request.Password);

            if (respons is null)
                return BadRequest();

            return Ok(respons);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<Guid>>> Login(UserLoginDto request)
        {
            var respons = await _authRepo.Login(request.Username, request.Password);

            if (respons is null)
                return BadRequest();

            return Ok(respons);

        }

    }
}
