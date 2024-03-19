namespace PhoneBook.Application.Core
{
    public class UserData
    {
        public UserData(Guid id, RoleEnum role)
        {
            Id = id;
            Role = role;
        }

        public Guid Id { get; }
        public RoleEnum Role { get; }
    }
}
