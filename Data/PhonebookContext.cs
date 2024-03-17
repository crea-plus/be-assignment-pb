using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

/// <summary>
/// Database context
/// </summary>
public class PhonebookContext : DbContext
{
	public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<Contact> Contacts { get; set; }
	public DbSet<Favourite> Favourites { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(entity =>
		{
			entity.ToTable(nameof(User));
			entity.HasKey(x => x.Id);
			entity.HasIndex(x => x.Username).IsUnique();
		});

		modelBuilder.Entity<Contact>(entity =>
		{
			entity.ToTable(nameof(Contact));
			entity.HasKey(x => x.Id);
			entity.HasIndex(x => x.Name).IsUnique();
			entity.HasIndex(x => x.PhoneNumber).IsUnique();
		});

		modelBuilder.Entity<Favourite>(entity =>
		{
			entity.ToTable(nameof(Favourite));
			entity.HasKey(x => new { x.UserId, x.ContactId });

			entity.HasOne(x => x.User).WithMany(x => x.Favourites).HasForeignKey(x => x.UserId).HasConstraintName("FK_Favourite_UserId");
			entity.HasOne(x => x.Contact).WithMany(x => x.Favourites).HasForeignKey(x => x.ContactId).HasConstraintName("FK_Favourite_ContactId");
		});
	}
}
