using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class UserVerificationEntityConfiguration : IEntityTypeConfiguration<UserVerificationEntity>
{
    public void Configure(EntityTypeBuilder<UserVerificationEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.HasIndex(u => u.Token).IsUnique();

        builder.Property(c => c.ExpireDate).IsRequired().HasColumnType("timestamp");
        builder.Property(c => c.CreatedAt).IsRequired().HasColumnType("timestamp");

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.UserVerifications)
            .HasForeignKey(rt => rt.UserId)
            .IsRequired();
    }
}
