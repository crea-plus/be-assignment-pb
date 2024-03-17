using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.DTOs;
using Service.DTOs.Configuration;
using Service.DTOs.Requests;
using Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Services;

public class UserService : IUserService
{
	private readonly PhonebookContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly TokenValidation _tokenValidation;

	public UserService(PhonebookContext dbContext, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
		_dbContext = dbContext;
		_mapper = mapper;
		_httpContextAccessor = httpContextAccessor;

		_tokenValidation = configuration.GetSection(nameof(TokenValidation)).Get<TokenValidation>()
			?? throw new ArgumentException("TokenValidation configuration missing");
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

	public async Task<String> LoginAsync(LoginRequest request)
	{
		User? user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == HashPassword(request.Password))
			?? throw new UnauthorizedAccessException();

		return GenerateToken(user);
	}

	/// <summary>
	/// Generates JSON Web token
	/// </summary>
	/// <returns></returns>
	private string GenerateToken(User user)
	{
		List<Claim> claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim("Admin", user.IsAdmin.ToString()),
		};

		SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_tokenValidation.Key));
		SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

		JwtSecurityToken token = new JwtSecurityToken(
			issuer: _tokenValidation.Issuer,
			audience: _tokenValidation.Audience,
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddMinutes(30),
			signingCredentials: credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public async Task<UserDto> GetUserFromClaim()
	{
		Claim usernameClaim = _httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.Name);
		User user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == usernameClaim.Value)
			?? throw new UnauthorizedAccessException();

		return _mapper.Map<UserDto>(user);
	}
}
