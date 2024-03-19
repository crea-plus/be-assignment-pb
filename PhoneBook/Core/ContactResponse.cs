using PhoneBook.DataSource.Models;

namespace PhoneBook.Core
{
    public class ContactResponse
    {
        public ContactResponse(ContactDbo contactDbo)
        {
            Id = contactDbo.Id;
            Email = contactDbo.Email;
            Name = contactDbo.Name;
            PhoneNumber = contactDbo.Number;
            AvatarUri = contactDbo.AvatarImageUri;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string PhoneNumber { get; }
        public string AvatarUri { get; }
        public string Email { get; }
    }
}