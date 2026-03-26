using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wasla.Service.Abstract.Services;
using Wasla.Service.DTOs.Auth;

namespace Wasla.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var res = await _authService.RegisterAsync(request);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var res = await _authService.LoginAsync(request.Email, request.Password);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string request)
        {
            var res = await _authService.RefreshTokenAsync(request);
            if (!res.Success)
                return BadRequest(res);
            return Ok(res);
        }
    }
}
