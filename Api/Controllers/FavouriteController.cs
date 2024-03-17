﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FavouriteController : ControllerBase
{
	private readonly IFavouriteService _favouriteService;

	public FavouriteController(IFavouriteService favouriteService)
	{
		_favouriteService = favouriteService;
	}

	/// <summary>
	/// Get collection of all Contacts
	/// </summary>
	/// <returns></returns>
	[HttpGet, Authorize]
	public async Task<IActionResult> Get()
	{
		return Ok(await _favouriteService.GetFavouriteContacts());
	}

	/// <summary>
	/// Add favourite contact to logged user
	/// </summary>
	/// <param name="contactId"></param>
	/// <returns></returns>
	[HttpPost("{contactId}"), Authorize]
	public async Task<IActionResult> AddFavourite(Guid contactId)
	{
		return Ok(await _favouriteService.AddFavouriteContact(contactId));
	}
}
