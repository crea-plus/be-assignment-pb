using CoachAssist.Application.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneBook.Application.Config;
using PhoneBook.Application.Core;
using PhoneBook.DataSource.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhoneBook.Application.Services
{
    public interface IAuthService
    {
        Task<AuthUserModel?> Login(LoginModel model);

        string GenerateJwtToken(AuthUserModel user);
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<PhoneBookConfig> _config;

        public AuthService(IUnitOfWork unitOfWork, IOptions<PhoneBookConfig> config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<AuthUserModel?> Login(LoginModel model)
        {
            var userDbo = await _unitOfWork.UserRepository.Get(model.Email);

            if (userDbo != null)
            {
                var passwordHash = PasswordHashing.Hash(model.Password, userDbo.PasswordSalt);
                if (passwordHash == userDbo.PasswordHash)
                {
                    return new AuthUserModel(userDbo);
                }
            }

            return null;
        }

        public string GenerateJwtToken(AuthUserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Value.Security.Authentication.SignKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role,  user.Role.ToString()),
                    new Claim(CustomClaims.UserId, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}