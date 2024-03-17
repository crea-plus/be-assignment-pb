using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Requests;

public class RegisterRequest
{
	[Required(ErrorMessage = "Username is required")]
	[StringLength(50)]
	public required string Username { get; set; }

	[Required(ErrorMessage = "Password is required")]
	[StringLength(50)]
	public required string Password { get; set; }

	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid Email Address")]
	[StringLength(100)]
	public required string Email { get; set; }

	public required bool IsAdmin { get; set; } = false;
}
