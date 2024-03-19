using CoachAssist.Application.Helpers;
using Microsoft.Extensions.Options;
using PhoneBook.Application.Config;
using PhoneBook.Application.Core;
using PhoneBook.DataSource.Models;
using PhoneBook.DataSource.Repositories;

namespace PhoneBook.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<PhoneBookConfig> _config;

        public UserService(IUnitOfWork unitOfWork, IOptions<PhoneBookConfig> config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<UserResponse> RegisterUser(UserModel model)
        {
            var (Hash, Salt) = PasswordHashing.HashWithSalt(model.Password);

            var userDbo = new UserDbo
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                UserName = model.UserName,
                PasswordHash = Hash,
                PasswordSalt = Salt
            };

            await _unitOfWork.UserRepository.Add(userDbo);
            await _unitOfWork.SaveAsync();

            return new UserResponse(userDbo);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _unitOfWork.UserRepository.UserExists(email);
        }
    }
}