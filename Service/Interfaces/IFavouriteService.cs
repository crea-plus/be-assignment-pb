using Service.DTOs;

namespace Service.Interfaces;

public interface IFavouriteService
{
	/// <summary>
	/// Get all favourite contacts for logged user
	/// </summary>
	public Task<FavouriteDto> GetFavouriteContacts();

}
