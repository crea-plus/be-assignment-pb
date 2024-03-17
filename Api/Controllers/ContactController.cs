using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
	private readonly IContactService _contactService;

	public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

	/// <summary>
	/// Get collection of all Contacts
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	public async Task<IActionResult> GetAsync()
	{
		return Ok(await _contactService.GetContactsAsync());
	}
}
