namespace PhoneBook.Services
{
    public interface IQuoteService
    {
        Task<string?> GetRandomQuote();
    }
}