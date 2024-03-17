using Data;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Interfaces;

namespace Service.Services;
public class ContactService : IContactService
{
	private readonly PhonebookContext _dbContext;

	public ContactService(PhonebookContext dbContext)
    {
		_dbContext = dbContext;
    }

    public async Task<List<ContactDto>> GetContactsAsync()
	{
		return await _dbContext.Contacts
			.Select(x => new ContactDto()
			{
				Id = x.Id,
				Name = x.Name,
				PhoneNumber = x.PhoneNumber,
				Email = x.Email,
				ImageUrl = x.ImageUrl
			})
			.ToListAsync();
	}
}
