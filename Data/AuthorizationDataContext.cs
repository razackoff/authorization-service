using Microsoft.EntityFrameworkCore;

namespace authorization_service.Data;

public class AuthorizationDataContext : DbContext
{
    public AuthorizationDataContext(DbContextOptions<AuthorizationDataContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}