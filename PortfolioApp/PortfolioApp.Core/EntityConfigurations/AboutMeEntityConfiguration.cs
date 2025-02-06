using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class AboutMeEntityConfiguration : IEntityTypeConfiguration<AboutMeEntity>
{
    public void Configure(EntityTypeBuilder<AboutMeEntity> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Introduction).IsRequired().HasColumnType("varchar(100)");
        builder.Property(a => a.FullName).IsRequired().HasColumnType("varchar(50)");
        builder.Property(a => a.Field).IsRequired().HasColumnType("varchar(50)");
        builder.Property(a => a.AboutMeImageUrl).IsRequired().HasColumnType("varchar(255)");
        builder.Property(a => a.LinkedInUrl).IsRequired().HasColumnType("varchar(255)");
        builder.Property(a => a.GithubUrl).IsRequired().HasColumnType("varchar(255)");
        builder.Property(a => a.GithubUrl).IsRequired().HasColumnType("varchar(255)");
        builder.Property(a => a.Email).IsRequired().HasColumnType("varchar(100)");
    }
}
