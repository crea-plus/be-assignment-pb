using Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Service.Services;

namespace UnitTests.Services;
public class ContactServiceTests
{
	private DbContextOptions<PhonebookContext> _contextOptions;

	[SetUp]
	public void Setup()
	{
		SqliteConnection connection = new SqliteConnection("Filename=:memory:");
		connection.Open();

		_contextOptions = new DbContextOptionsBuilder<PhonebookContext>()
		.UseSqlite(connection)
			.Options;

		using PhonebookContext? context = new(_contextOptions);
		context.Database.EnsureCreated();
	}

	/// <summary>
	/// Mock DB context
	/// </summary>
	/// <returns></returns>
	private async Task<PhonebookContext> GetDbContext()
	{
		PhonebookContext dbContext = new(_contextOptions);

		var contact = new Data.Models.Contact()
		{
			Name = "ContactName",
			PhoneNumber = "+38612345678"
		};
		await dbContext.Contacts.AddAsync(contact);

		await dbContext.SaveChangesAsync();
		return dbContext;
	}

	private ContactService GetService(PhonebookContext dbContext)
	{
		return new ContactService(dbContext);
	}

	[Test]
	public async Task GetContactsAsync_ReturnCollection()
	{
		PhonebookContext dbcontext = await GetDbContext();
		ContactService service = GetService(dbcontext);

		List<Service.DTOs.ContactDto> result = await service.GetContactsAsync();

		Assert.That(result, Is.Not.Null);
		Assert.That(result, Is.Not.Empty);
		Assert.That(result.Count(), Is.EqualTo(dbcontext.Contacts.Count()));
	}
}
