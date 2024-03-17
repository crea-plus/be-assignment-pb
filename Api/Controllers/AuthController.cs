using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly IUserService _userService;

	public AuthController(IUserService userService)
	{
		_userService = userService;
	}

	/// <summary>
	/// Register new user
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("register")]
	public async Task<IActionResult> Register(Service.DTOs.Requests.RegisterRequest request)
	{
		return Ok(await _userService.RegisterAsync(request));
	}
}
