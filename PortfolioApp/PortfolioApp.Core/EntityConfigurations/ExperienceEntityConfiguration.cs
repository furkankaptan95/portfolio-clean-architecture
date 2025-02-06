using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class ExperienceEntityConfiguration : IEntityTypeConfiguration<ExperienceEntity>
{
    public void Configure(EntityTypeBuilder<ExperienceEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Title).IsRequired().HasColumnType("varchar(100)");
        builder.Property(e => e.Company).IsRequired().HasColumnType("varchar(100)");
        builder.Property(e => e.Description).IsRequired().HasColumnType("text");
        builder.Property(e => e.StartDate).IsRequired().HasColumnType("timestamp");
        builder.Property(e => e.EndDate).HasColumnType("timestamp");
        builder.Property(e => e.IsVisible).IsRequired().HasColumnType("boolean");
    }
}
