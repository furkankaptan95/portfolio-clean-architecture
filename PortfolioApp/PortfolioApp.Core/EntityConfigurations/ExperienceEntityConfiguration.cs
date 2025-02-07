﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class ExperienceEntityConfiguration : IEntityTypeConfiguration<ExperienceEntity>
{
    public void Configure(EntityTypeBuilder<ExperienceEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Title).IsRequired().HasColumnType("nvarchar(100)");
        builder.Property(e => e.Company).IsRequired().HasColumnType("nvarchar(100)");
        builder.Property(e => e.Description).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(e => e.StartDate).IsRequired().HasColumnType("datetime");
        builder.Property(e => e.EndDate).HasColumnType("datetime");
        builder.Property(e => e.IsVisible).IsRequired().HasColumnType("bit");
    }
}
