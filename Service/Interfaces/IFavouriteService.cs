using Service.DTOs;

namespace Service.Interfaces;

public interface IFavouriteService
{
	/// <summary>
	/// Get all favourite contacts for logged user
	/// </summary>
	public Task<FavouriteDto> GetFavouriteContacts();

	/// <summary>
	/// Add favourite contact to logged user
	/// </summary>
	/// <param name="contactId"></param>
	public Task<bool> AddFavouriteContact(Guid contactId);
}
