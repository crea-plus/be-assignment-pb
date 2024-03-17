using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.DTOs.Configuration;
using Service.Interfaces;
using Service.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PhonebookContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(PhonebookContext))));

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.Load("Service"));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	// include xml document
	string xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
	c.IncludeXmlComments(xmlPath);
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer key')",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	// enable authorize option
	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<String>()
		}
	});
});

// Configure Bearer authentication
var tokenValidation = builder.Configuration.GetSection(nameof(TokenValidation)).Get<TokenValidation>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
		{
			ValidIssuer = tokenValidation.Issuer,
			ValidAudience = tokenValidation.Audience,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValidation.Key))
		};
	});

// Configure authorization policy
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("Admin", policy => policy.RequireClaim("Admin", "True")); // 'Admin' claim must be set to 'True'
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// run migrations on startup
using IServiceScope scope = app.Services.CreateScope();
PhonebookContext dbContext = scope.ServiceProvider.GetRequiredService<PhonebookContext>();
dbContext.Database.Migrate();

app.Run();
