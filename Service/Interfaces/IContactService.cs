using Service.DTOs;
using Service.DTOs.Requests;

namespace Service.Interfaces;

public interface IContactService
{
	/// <summary>
	/// Get collection of all Contacts
	/// </summary>
	/// <returns></returns>
	public Task<List<ContactDto>> GetContactsAsync();

	/// <summary>
	/// Create new Contact
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	public Task<ContactDto> CreateContactAsync(CreateContactRequest request);

	/// <summary>
	/// Update specific Contact
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	public Task<ContactDto> UpdateContactAsync(UpdateContactRequest request);
}
