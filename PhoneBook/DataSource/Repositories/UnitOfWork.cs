namespace PhoneBook.DataSource.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhoneBookDbContext _dbContext;

        public UnitOfWork(PhoneBookDbContext dbContext)
        {
            _dbContext = dbContext;

            UserRepository = new UserRepository(_dbContext);
            ContactRepository = new ContactRepository(_dbContext);
            UserContactRepository = new UserContactRepository(_dbContext);
        }

        public IUserRepository UserRepository { get; }
        public ContactRepository ContactRepository { get; }
        public UserContactRepository UserContactRepository { get; }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}