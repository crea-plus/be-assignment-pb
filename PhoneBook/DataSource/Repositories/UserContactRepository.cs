using PhoneBook.DataSource.Models;

namespace PhoneBook.DataSource.Repositories
{
    public interface IUserContactRepository : IRepository<UserContactDbo>
    {
        Task<bool> FavoriteContact(Guid? userId, Guid contactId, bool isFavorite);
    }

    public class UserContactRepository : Repository<UserContactDbo>, IUserContactRepository
    {
        private readonly PhoneBookDbContext _dbContext;

        public UserContactRepository(PhoneBookDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> FavoriteContact(Guid? userId, Guid contactId, bool isFavorite)
        {
            var userContact = await _dbContext.UserContacts.FindAsync(userId, contactId);

            if (userContact == null)
            {
                return false;
            }
            userContact.Favorite = isFavorite;

            _dbContext.Update(userContact);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}