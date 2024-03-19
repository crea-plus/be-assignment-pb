using Microsoft.EntityFrameworkCore;
using PhoneBook.DataSource.Models;

namespace PhoneBook.DataSource
{
    public class PhoneBookDbContext : DbContext
    {
        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDbo>()
                .ToTable("Users")
                .HasKey(e => e.Id);

            modelBuilder.Entity<ContactDbo>()
                .ToTable("Contacts")
                .HasKey(e => e.Id);

            modelBuilder.Entity<UserContactDbo>()
                .ToTable("UserContacts")
                .HasOne(i => i.User)
                .WithMany(i => i.Contacts)
                .HasForeignKey(i => i.UserId)
                .HasPrincipalKey(i => i.Id);

            modelBuilder.Entity<UserContactDbo>()
               .ToTable("UserContacts")
               .HasOne(i => i.Contact)
               .WithMany(i => i.UserContacts)
               .HasForeignKey(i => i.ContactId)
               .HasPrincipalKey(i => i.Id);


        }

        public DbSet<UserDbo> Users { get; set; }
        public DbSet<ContactDbo> Contacts { get; set; }

        public DbSet<UserContactDbo> UserContacts { get; set; }
    }
}
