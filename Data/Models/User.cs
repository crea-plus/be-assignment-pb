using System.ComponentModel.DataAnnotations;

namespace Data.Models;

/// <summary>
/// User entity
/// </summary>
public class User : BaseEntity
{
	[Required(ErrorMessage = "Username is required")]
	[StringLength(50)]
	public required string Username { get; set; }

	[Required(ErrorMessage = "Password is required")]
	[StringLength(250)]
	public required string Password { get; set; }

	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid Email Address")]
	[StringLength(100)]
	public required string Email { get; set; }

	public Boolean IsAdmin { get; set; } = false;

	/// <summary>
	/// Collection of linked Favourites
	/// </summary>
	public virtual ICollection<Favourite> Favourites { get; set; }
}
