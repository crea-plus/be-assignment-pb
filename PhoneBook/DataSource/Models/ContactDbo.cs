# nullable disable

namespace PhoneBook.DataSource.Models
{
    public class ContactDbo : BaseEntityDbo
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string AvatarImageUri { get; set; }
        public string Number { get; set; }

        public ICollection<UserContactDbo> UserContacts { get; set; } = new List<UserContactDbo>();
    }
}