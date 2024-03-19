using Microsoft.EntityFrameworkCore;
using PhoneBook.DataSource.Models;

namespace PhoneBook.DataSource.Repositories
{
    public interface IUserRepository :IRepository<UserDbo>
    {
        Task<bool> UserExists(string email);
        Task<UserDbo?> Get(string email);
    }

    public class UserRepository : Repository<UserDbo>, IUserRepository
    {
        private readonly PhoneBookDbContext _dbContext;

        public UserRepository(PhoneBookDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDbo?> Get(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(i => i.Email == email);
        }

        public async Task<bool> UserExists(string email)
        {
            return await _dbContext.Users.AnyAsync(i => i.Email == email);
        }
    }
}