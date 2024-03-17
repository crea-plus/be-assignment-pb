using Service.DTOs;

namespace Service.Interfaces;

public interface IContactService
{
	/// <summary>
	/// Get collection of all Contacts
	/// </summary>
	/// <returns></returns>
	public Task<List<ContactDto>> GetContactsAsync();
}
