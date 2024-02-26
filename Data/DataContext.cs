using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace authorization_service.Data;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<AuthorizationDataContext> options) : base(options) {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityUser>(b =>
        {
            b.ToTable("BaseAuthorizationUsers");
        });

        modelBuilder.Entity<IdentityUserClaim<string>>(b =>
        {
            b.ToTable("BaseAuthorizationUserClaims");
        });

        modelBuilder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.ToTable("BaseAuthorizationUserLogins");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(b =>
        {
            b.ToTable("BaseAuthorizationUserTokens");
        });

        modelBuilder.Entity<IdentityRole>(b =>
        {
            b.ToTable("BaseAuthorizationRoles");
        });

        modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
        {
            b.ToTable("BaseAuthorizationRoleClaims");
        });

        modelBuilder.Entity<IdentityUserRole<string>>(b =>
        {
            b.ToTable("BaseAuthorizationUserRoles");
        });
    }
}