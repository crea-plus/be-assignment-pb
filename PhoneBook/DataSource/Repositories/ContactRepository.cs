using PhoneBook.Core;
using PhoneBook.DataSource.Models;

namespace PhoneBook.DataSource.Repositories
{
    public interface IContactRepository : IRepository<ContactDbo>
    {
    }

    public class ContactRepository : Repository<ContactDbo>, IContactRepository
    {
        private readonly PhoneBookDbContext _dbContext;

        public ContactRepository(PhoneBookDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}