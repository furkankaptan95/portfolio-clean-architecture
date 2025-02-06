using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class PersonalInfoEntityConfiguration : IEntityTypeConfiguration<PersonalInfoEntity>
{
    public void Configure(EntityTypeBuilder<PersonalInfoEntity> builder)
    {
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Id).ValueGeneratedOnAdd();

        builder.Property(pi => pi.About).IsRequired().HasColumnType("varchar(300)");
        builder.Property(pi => pi.Name).IsRequired().HasColumnType("varchar(50)");
        builder.Property(pi => pi.Adress).IsRequired().HasColumnType("varchar(50)");
        builder.Property(pi => pi.Surname).IsRequired().HasColumnType("varchar(50)");
        builder.Property(c => c.BirthDate).IsRequired().HasColumnType("timestamp");
    }
}