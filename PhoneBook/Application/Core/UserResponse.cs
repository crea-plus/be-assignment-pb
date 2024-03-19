#nullable disable

using PhoneBook.DataSource.Models;

namespace PhoneBook.Application.Core
{
    public class UserResponse
    {
        public UserResponse(UserDbo userDbo)
        {
            Id = userDbo.Id;
            Email = userDbo.Email;
            UserName = userDbo.UserName;
        }

        public Guid Id { get; }
        public string UserName { get; }
        public string Email { get; }
    }
}