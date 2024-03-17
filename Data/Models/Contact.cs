using System.ComponentModel.DataAnnotations;

namespace Data.Models;

/// <summary>
/// Contact entity
/// </summary>
public class Contact : BaseEntity
{
	[Required(ErrorMessage = "Name is required")]
	[StringLength(100)]
	public required string Name { get; set; }

	[Required(ErrorMessage = "Phone number is required")]
	[RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format")]
	[StringLength(17)]
	public required string PhoneNumber { get; set; }

	[Url(ErrorMessage = "Invalid URL format")]
	public string? ImageUrl { get; set; }

	[EmailAddress(ErrorMessage = "Invalid Email Address")]
	[StringLength(100)]
	public string? Email { get; set; }

	/// <summary>
	/// Collection of linked Favourites
	/// </summary>
	public virtual ICollection<Favourite> Favourites { get; set; }
}
