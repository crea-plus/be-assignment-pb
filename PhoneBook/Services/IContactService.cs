using PhoneBook.Core;

namespace PhoneBook.Services
{
    public interface IContactService
    {
        Task<ContactResponse> AddContact (AddContactModel contact, Guid id);
        Task<Contact?> Get(Guid contactId);
        Task<bool> MarkAsFavorite(Guid contactId, Guid? userId, bool favorite);
        Task<bool>UpdateContact(Guid contactId, AddContactModel addContactModel);
    }
}
