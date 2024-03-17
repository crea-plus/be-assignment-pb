using AutoMapper;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.DTOs.Requests;
using Service.Interfaces;

namespace Service.Services;
public class ContactService : IContactService
{
	private readonly PhonebookContext _dbContext;
	private readonly IMapper _mapper;

	public ContactService(PhonebookContext dbContext, IMapper mapper)
    {
		_dbContext = dbContext;
		_mapper = mapper;
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

	public async Task<ContactDto> GetContactByIdAsync(Guid id)
	{
		Contact contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == id)
			?? throw new ArgumentException($"Contact with provided ID does not exist");

		return _mapper.Map<ContactDto>(contact);
	}

	public async Task<ContactDto> CreateContactAsync(CreateContactRequest request)
	{
		if (await IsContactUnique(request))
		{
			Contact contact = new Contact()
			{
				Name = request.Name,
				PhoneNumber = request.PhoneNumber,
				Email = request.Email,
				ImageUrl = request.ImageUrl
			};
			await _dbContext.Contacts.AddAsync(contact);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<ContactDto>(contact);
		}
		throw new ArgumentException("Contact with provided name/phone number already exist");
	}

	/// <summary>
	/// Check if Contact with same name/phone number already exist
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private async Task<bool> IsContactUnique(CreateContactRequest request)
	{
		return !await _dbContext.Contacts.AnyAsync(x => x.Name == request.Name || x.PhoneNumber == request.PhoneNumber);
	}

	public async Task<ContactDto> UpdateContactAsync(UpdateContactRequest request)
	{
		Contact? contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id)
			?? throw new ArgumentException("Contact with provided ID does not exist");

		if (await IsUpdateContactUnique(request))
		{
			contact.Name = request.Name;
			contact.PhoneNumber = request.PhoneNumber;
			contact.Email = request.Email;
			contact.ImageUrl = request.ImageUrl;

			await _dbContext.SaveChangesAsync();

			return _mapper.Map<ContactDto>(contact);
		}
		throw new ArgumentException("Contact with provided name/phone number already exist");
	}

	/// <summary>
	/// Check if Contact with same name/phone number already exist other then current
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	private async Task<bool> IsUpdateContactUnique(UpdateContactRequest request)
	{
		return !await _dbContext.Contacts.AnyAsync(x => (x.Name == request.Name || x.PhoneNumber == request.PhoneNumber) && x.Id != request.Id);
	}
}
