using Data;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services;
public class FavouriteService : IFavouriteService
{
	private readonly PhonebookContext _dbContext;
	private readonly IUserService _userService;

	public FavouriteService(PhonebookContext dbContext, IUserService userService)
	{
		_dbContext = dbContext;
		_userService = userService;
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
}
