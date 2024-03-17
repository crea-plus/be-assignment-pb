using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Requests;
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

	/// <summary>
	/// Create new Contact
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost, Authorize(Policy = "Admin")]
	public async Task<IActionResult> CreateAsync(CreateContactRequest request)
	{
		return Ok(await _contactService.CreateContactAsync(request));
	}

	/// <summary>
	/// Update specific Contact
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPut, Authorize(Policy = "Admin")]
	public async Task<IActionResult> UpdateAsync([FromBody] UpdateContactRequest request)
	{
		return Ok(await _contactService.UpdateContactAsync(request));
	}
}
