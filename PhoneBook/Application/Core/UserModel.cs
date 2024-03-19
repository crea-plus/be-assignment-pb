#nullable disable

using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Application.Core
{
    public record UserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}