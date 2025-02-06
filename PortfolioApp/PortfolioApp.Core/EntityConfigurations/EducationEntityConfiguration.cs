using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class EducationEntityConfiguration : IEntityTypeConfiguration<EducationEntity>
{
    public void Configure(EntityTypeBuilder<EducationEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Degree).IsRequired().HasColumnType("varchar(50)");
        builder.Property(e => e.School).IsRequired().HasColumnType("varchar(100)");
        builder.Property(e => e.StartDate).IsRequired().HasColumnType("timestamp");
        builder.Property(e => e.EndDate).HasColumnType("timestamp");
        builder.Property(e => e.IsVisible).IsRequired().HasColumnType("boolean");
    }
}
