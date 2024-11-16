using Examen_2_Lenguajes.Dto.Auth;
using Examen_2_Lenguajes.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Examen_2_Lenguajes.Controllers
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

        // Login endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            if (response.Status)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }

        // Register endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var response = await _authService.RegisterAsync(registerDto);
            if (response.Status)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }

}

