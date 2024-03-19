#nullable disable

using PhoneBook.DataSource.Models;

namespace PhoneBook.Core
{
    public class Contact
    {
        public Contact(ContactDbo contactDbo)
        {
            ContactId = contactDbo.Id;
            Name = contactDbo.Name;
            Email = contactDbo.Email;
            AvatarImageUri = contactDbo.AvatarImageUri;
            Number = contactDbo.Number;
        }

        public Guid ContactId { get; }
        public string Name { get; }
        public string Email { get; }

        public string AvatarImageUri { get; }
        public string Number { get; }
    }
}