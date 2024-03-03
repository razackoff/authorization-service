using authorization_service.Data;
using authorization_service.Repositories;
using authorization_service.Services;
using authorization_service.Services.JWT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Регистрация сервисов
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddSingleton<IJwtService>(provider => 
    new JwtService("your_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_hereyour_secret_key_here", 100));


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
