#nullable disable

using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Core
{
    public class AddContactModel
    {

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string Name { get; set; }

        public string AvatarImageUrl { get; set; }

        [RegularExpression(@"^\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
