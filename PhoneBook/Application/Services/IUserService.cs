using PhoneBook.Application.Core;

namespace PhoneBook.Application.Services
{
    public interface IUserService
    {
        Task<UserResponse> RegisterUser(UserModel model);

        Task<bool> UserExists(string email);
    }
}
