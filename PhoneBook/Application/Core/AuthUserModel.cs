#nullable disable

using PhoneBook.DataSource.Models;

namespace PhoneBook.Application.Core
{
    public class AuthUserModel
    {
        public AuthUserModel(UserDbo userDbo)
        {
            Id = userDbo.Id;
            Email = userDbo.Email;
            Name = userDbo.UserName;
            PasswordHash = userDbo.PasswordHash;
            PasswordSalt = userDbo.PasswordSalt;
            Role = RoleEnum.User;
        }

        public Guid Id { get; }
        public string Email { get; }
        public string Name { get; }
        public string PasswordHash { get; }
        public string PasswordSalt { get; }
        public RoleEnum Role { get; }
    }
}
