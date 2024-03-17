using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services;
public class FavouriteService : IFavouriteService
{
	private readonly PhonebookContext _dbContext;
	private readonly IUserService _userService;
	private readonly IContactService _contactService;

	public FavouriteService(PhonebookContext dbContext, IUserService userService, IContactService contactService)
	{
		_dbContext = dbContext;
		_userService = userService;
		_contactService = contactService;
	}

	public async Task<FavouriteDto> GetFavouriteContacts()
	{
		UserDto user = await _userService.GetUserFromClaim();

		List<ContactDto> favourites = await _dbContext.Favourites
			.Include(x => x.Contact)
			.Where(x => x.UserId == user.Id)
			.Select(x => new ContactDto()
			{
				Id = x.Contact.Id,
				Name = x.Contact.Name,
				PhoneNumber = x.Contact.PhoneNumber,
				Email = x.Contact.Email,
				ImageUrl = x.Contact.ImageUrl
			}).ToListAsync();

		return new FavouriteDto() { Username = user.Username, Contacts = favourites };
	}

	public async Task<bool> AddFavouriteContact(Guid contactId)
	{
		ContactDto contact = await _contactService.GetContactByIdAsync(contactId);
		UserDto user = await _userService.GetUserFromClaim();

		await _dbContext.Favourites.AddAsync(new Favourite() { UserId = user.Id, ContactId = contact.Id });
		return await _dbContext.SaveChangesAsync() > 0;
	}
}
