﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class ProjectEntityConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Title).IsRequired().HasColumnType("nvarchar(100)");
        builder.Property(p => p.Description).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(p => p.ImageUrl).IsRequired().HasColumnType("nvarchar(255)");
        builder.Property(e => e.IsVisible).IsRequired().HasColumnType("bit");
    }
}
