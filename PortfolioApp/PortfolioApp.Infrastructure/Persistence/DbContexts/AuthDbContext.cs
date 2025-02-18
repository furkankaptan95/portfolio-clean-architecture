using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Helpers;

namespace PortfolioApp.Infrastructure.Persistence.DbContexts;
public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    public DbSet<UserVerificationEntity> UserVerifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        base.OnModelCreating(modelBuilder);

        byte[] passwordHash, passwordSalt;


        HashingHelper.CreatePasswordHash("11229900", out passwordHash, out passwordSalt);

        modelBuilder.Entity<UserEntity>().HasData(

            new UserEntity
            {
                Id = 1,
                Username = "FurkanKaptan",
                Firstname = "Furkan",
                Lastname = "Kaptan",
                Email = "furkan.kaptan.work@gmail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
                Role = "Admin",
            }
        );
    }
}
