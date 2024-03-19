using PhoneBook.Application.Config;
using PhoneBook.Application.Services;
using PhoneBook.DataSource;
using PhoneBook.DataSource.Repositories;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Services;
using PhoneBook.Common.ServiceExtensions;
using System.Net.Http;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // configuration
        var section = builder.Configuration.GetSection("PhoneBookConfig");
        var config = section.Get<PhoneBookConfig>();
        builder.Services.AddOptions<PhoneBookConfig>().Bind(section);

        // Add services to the container.

        builder.Services.AddTransient<IUserService, UserService>();
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<IQuoteService, QuoteService>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IContactService, ContactService>();
        builder.Services.AddTransient<ContactRepository, ContactRepository>();
        builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        builder.Services.AddHttpClient();

        builder.Services.AddDbContext<PhoneBookDbContext>(options =>
        {
            options.UseSqlServer(config!.DatabaseConnection.ConnectionString);
        }, ServiceLifetime.Transient);

        builder.Services.ConfigureJwtAuthentication(config!.Security.Authentication.SignKey);
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ContactPolicy", policy =>
            {
                policy.RequireRole("Contact");
            });
            options.AddPolicy("OnlyAllowAnonymous", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    return !context.User.Identity.IsAuthenticated;
                });
            });
        });

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

        app.Run();
    }
}