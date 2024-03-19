#nullable disable

namespace PhoneBook.Application.Config
{
    public class PhoneBookConfig
    {
        public Security Security { get; set; }
        public DatabaseConnection DatabaseConnection { get; set; }
    }

    public class Security
    {
        public Authentication Authentication { get; set; }
    }

    public class Authentication
    {
        public string SignKey { get; set; }
        public string PasswordSalt { get; set; }
    }

    public class DatabaseConnection
    {
        public string ConnectionString { get; set; }
    }
}