namespace PhoneBook.DataSource.Models
{
    public class UserContactDbo : BaseEntityDbo
    {
        public Guid UserId { get; set; }
        public UserDbo User { get; set; }

        public Guid? ContactId { get; set; }
        public ContactDbo Contact { get; set; }

        public bool Favorite { get; set; }
    }
}