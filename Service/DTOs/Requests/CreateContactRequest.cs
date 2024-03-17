using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Requests;

public class CreateContactRequest
{
    [StringLength(100)]
    public required string Name { get; set; }

    [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
    public required string PhoneNumber { get; set; }

    [Url(ErrorMessage = "Invalid URL format")]
    public string? ImageUrl { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }
}
