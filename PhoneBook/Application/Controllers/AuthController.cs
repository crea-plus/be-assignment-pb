using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.Core;
using PhoneBook.Application.Services;

namespace PhoneBook.Application.Controllers
{
    [ApiController]
    [Authorize("OnlyAllowAnonymous")]
    [Route("api/v1/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var user = await _authService.Login(model);
                if (user == null)
                {
                    return Unauthorized("Invalid username or password.");
                }

                var token = _authService.GenerateJwtToken(user);

                return Ok(new { Token = token, UserId = user.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during login: {ex.Message}");
            }
        }
    }
}
