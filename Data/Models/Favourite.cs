namespace Data.Models;

public class Favourite
{
	/// <summary>
	/// Linked User
	/// </summary>
	public Guid UserId { get; set; }
	public virtual User User { get; set; }

	/// <summary>
	/// Favourite Contact for linked User
	/// </summary>
	public Guid ContactId { get; set; }
	public virtual Contact Contact { get; set; }
}
