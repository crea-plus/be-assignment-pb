using Data;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PhonebookContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(PhonebookContext))));

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.Load("Service"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// run migrations on startup
using IServiceScope scope = app.Services.CreateScope();
PhonebookContext dbContext = scope.ServiceProvider.GetRequiredService<PhonebookContext>();
dbContext.Database.Migrate();

app.Run();
