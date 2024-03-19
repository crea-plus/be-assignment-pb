# nullable disable

namespace PhoneBook.DataSource.Models
{
    public class UserDbo : BaseEntityDbo
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public ICollection<UserContactDbo> Contacts { get; set; } = new List<UserContactDbo>();
    }
}