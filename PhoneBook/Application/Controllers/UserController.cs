using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.Core;
using PhoneBook.Application.Services;

namespace PhoneBook.Application.Controllers
{
    [ApiController]
    [Authorize("OnlyAllowAnonymous")]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> RegisterUser(UserModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _userService.UserExists(model.Email))
                {
                    return BadRequest("Email is already registered.");
                }

                var registeredUser = await _userService.RegisterUser(model);

                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during registration: {ex.Message}");
            }
        }
    }
}