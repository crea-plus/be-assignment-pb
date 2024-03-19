using PhoneBook.DataSource.Models;

namespace PhoneBook.DataSource.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntityDbo
    {
        Task<TEntity?> GetById(Guid id);

        Task Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntityDbo
    {
        private readonly PhoneBookDbContext _dbContext;

        public Repository(PhoneBookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<TEntity?> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}