namespace PhoneBook.DataSource.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        ContactRepository ContactRepository { get; }
        UserContactRepository UserContactRepository { get; }

        Task SaveAsync();
    }
}