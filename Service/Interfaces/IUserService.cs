using Service.DTOs.Requests;
using Service.DTOs;

namespace Service.Interfaces;

public interface IUserService
{
	/// <summary>
	/// Register new user
	/// </summary>
	/// <param name="request"></param>
	/// <returns>New user</returns>
	public Task<UserDto> RegisterAsync(RegisterRequest request);
}
