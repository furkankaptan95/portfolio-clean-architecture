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
            .HasColumnType("varchar(100)");  // PostgreSQL'de varchar kullanımı

        builder.Property(bp => bp.Content)
            .IsRequired()
            .HasColumnType("text");  // PostgreSQL'de metin verisi için text tipi

        builder.Property(bp => bp.PublishDate)
            .IsRequired()
            .HasColumnType("timestamp");  // datetime yerine PostgreSQL'de timestamp kullanılır

        builder.Property(bp => bp.UpdatedAt)
            .HasColumnType("timestamp");  // PostgreSQL'de datetime yerine timestamp

        builder.Property(bp => bp.IsVisible)
            .IsRequired()
            .HasColumnType("boolean");  // PostgreSQL'de bit yerine boolean kullanılır

        // Foreign key ilişkisi
        builder.HasMany(bp => bp.Comments)
            .WithOne(c => c.BlogPost)
            .HasForeignKey(c => c.BlogPostId)
            .OnDelete(DeleteBehavior.SetNull);
    }

}
