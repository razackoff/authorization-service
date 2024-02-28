using authorization_service.Models;
using Microsoft.EntityFrameworkCore;

namespace authorization_service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .ToTable("Users");
        
        // Настройка свойств сущности User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id); // Устанавливаем свойство Id как первичный ключ

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired(); // Требуем, чтобы Email был обязательным

        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .IsRequired(); // Требуем, чтобы Password был обязательным

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired(); // Требуем, чтобы Role был обязательным

        base.OnModelCreating(modelBuilder);
    }
}