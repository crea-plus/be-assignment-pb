namespace Data.Models;

public abstract class BaseEntity
{
	public virtual Guid Id { get; protected set; }
}
