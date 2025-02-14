using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class BlogPostEntityConfiguration : IEntityTypeConfiguration<BlogPostEntity>
{
    public void Configure(EntityTypeBuilder<BlogPostEntity> builder)
    {
        builder.HasKey(bp => bp.Id);
        builder.Property(bp => bp.Id).ValueGeneratedOnAdd();

        // PostgreSQL'de nvarchar yerine text veya varchar kullanılabilir
        builder.Property(bp => bp.Title)
            .IsRequired()
            .HasColumnType("nvarchar(100)");  // PostgreSQL'de varchar kullanımı

        builder.Property(bp => bp.Content)
            .IsRequired()
            .HasColumnType("nvarchar(max)");  // PostgreSQL'de metin verisi için text tipi

        builder.Property(bp => bp.PublishDate)
            .IsRequired()
            .HasColumnType("datetime");  // datetime yerine PostgreSQL'de timestamp kullanılır

        builder.Property(bp => bp.UpdatedAt)
            .HasColumnType("datetime");  // PostgreSQL'de datetime yerine timestamp

        builder.Property(bp => bp.IsVisible)
            .IsRequired()
            .HasColumnType("bit");  // PostgreSQL'de bit yerine boolean kullanılır

        // Foreign key ilişkisi
        builder.HasMany(bp => bp.Comments)
            .WithOne(c => c.BlogPost)
            .HasForeignKey(c => c.BlogPostId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
