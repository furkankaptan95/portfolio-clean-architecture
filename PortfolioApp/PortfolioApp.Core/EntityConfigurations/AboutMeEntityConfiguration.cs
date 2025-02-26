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

        builder.Property(a => a.Introduction).IsRequired().HasColumnType("nvarchar(100)");
        builder.Property(a => a.FullName).IsRequired().HasColumnType("nvarchar(50)");
        builder.Property(a => a.Field).IsRequired().HasColumnType("nvarchar(50)");
        builder.Property(a => a.AboutMeImageUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.HeroImageUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.LinkedInUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.MediumUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.InstagramUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.TwitterUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.GithubUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.GithubUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(a => a.Email).IsRequired().HasColumnType("nvarchar(100)");
    }
}
