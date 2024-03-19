using PhoneBook.Core;
using PhoneBook.DataSource.Models;
using PhoneBook.DataSource.Repositories;

namespace PhoneBook.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ContactResponse> AddContact(AddContactModel contact, Guid id)
        {
            var userDbo = await _unitOfWork.UserRepository.GetById(id);
            if (userDbo == null)
            {
                //log
                return null;
            }

            var contactDbo = new ContactDbo
            {
                Id = Guid.NewGuid(),
                AvatarImageUri = contact.AvatarImageUrl,
                Email = contact.Email,
                Name = contact.Name,
                Number = contact.Name,
                UserContacts = new List<UserContactDbo>
                {
                     new() {
                          Id = Guid.NewGuid(),
                          Favorite = false,
                          User = userDbo,
                          UserId = userDbo.Id,
                          }
                 }
            };

            await _unitOfWork.ContactRepository.Add(contactDbo);
            await _unitOfWork.SaveAsync();

            return new ContactResponse(contactDbo);
        }

        public async Task<Contact?> Get(Guid contactId)
        {
            var contactDbo = await _unitOfWork.ContactRepository.GetById(contactId);

            if (contactDbo == null)
            {
                //log or return 404 not found
                return null;
            }

            return new Contact(contactDbo);
        }

        public async Task<bool> MarkAsFavorite(Guid contactId, Guid? userId, bool favorite)
        {
            return await _unitOfWork.UserContactRepository.FavoriteContact(userId, contactId, favorite);
        }

        public async Task<bool> UpdateContact(Guid contactId, AddContactModel addContactModel)
        {
            var contact = await _unitOfWork.ContactRepository.GetById(contactId);
            if (contact == null)
            {
                return false;
            }

            contact.Number = addContactModel.PhoneNumber;
            contact.Name = addContactModel.Name;
            contact.Email = addContactModel.Email;
            contact.AvatarImageUri = addContactModel.AvatarImageUrl;

            _unitOfWork.ContactRepository.Update(contact);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}