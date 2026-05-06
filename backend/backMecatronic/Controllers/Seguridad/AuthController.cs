using backMecatronic.Models.DTOs.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                var user = await _authService.Registrar(dto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message, code = "email_exists" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message, code = "invalid_input" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, code = "register_failed" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                var result = await _authService.Login(dto);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message, code = "user_not_found" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message, code = "invalid_password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, code = "login_failed" });
            }
        }
    }
}
