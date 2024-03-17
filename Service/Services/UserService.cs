using AutoMapper;
using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.DTOs.Requests;
using Service.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services;

public class UserService : IUserService
{
	private readonly PhonebookContext _dbContext;
	private readonly IMapper _mapper;

	public UserService(PhonebookContext dbContext, IMapper mapper)
    {
		_dbContext = dbContext;
		_mapper = mapper;
    }

    public async Task<UserDto> RegisterAsync(RegisterRequest request)
	{
		if (await IsUserUniqueAsync(request.Username, request.Email))
		{
			// add user to db
			User user = new()
			{
				Username = request.Username,
				Email = request.Email,
				Password = HashPassword(request.Password),
				IsAdmin = request.IsAdmin
			};
			await _dbContext.Users.AddAsync(user);
			await _dbContext.SaveChangesAsync();

			return _mapper.Map<UserDto>(user);
		}
		throw new ArgumentException($"User with provided username/email already exist");
	}

	/// <summary>
	/// Check if user already exists in DB
	/// </summary>
	/// <param name="username"></param>
	/// <param name="email"></param>
	/// <returns></returns>
	private async Task<bool> IsUserUniqueAsync(string username, string email)
	{
		return !await _dbContext.Users.AnyAsync(x => x.Username == username || x.Email == email);
	}

	/// <summary>
	/// Hash password with SHA512 algorithm
	/// </summary>
	/// <param name="password"></param>
	/// <returns></returns>
	private static string HashPassword(string password)
	{
		byte[] hashBytes;
		using (SHA512 algorithm = SHA512.Create())
		{
			hashBytes = algorithm.ComputeHash(new UTF8Encoding().GetBytes(password));
		}
		return Convert.ToBase64String(hashBytes);
	}
}
