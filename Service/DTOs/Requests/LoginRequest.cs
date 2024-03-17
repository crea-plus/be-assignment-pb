using System.ComponentModel.DataAnnotations;
namespace Service.DTOs.Requests;

public class LoginRequest
{
	[Required(ErrorMessage = "Username is required")]
	[StringLength(50)]
	public required string Username { get; set; }

	[Required(ErrorMessage = "Password is required")]
	[StringLength(50)]
	public required string Password { get; set; }
}
