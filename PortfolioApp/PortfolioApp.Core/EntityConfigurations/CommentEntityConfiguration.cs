using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class CommentEntityConfiguration : IEntityTypeConfiguration<CommentEntity>
{
    public void Configure(EntityTypeBuilder<CommentEntity> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.Content)
            .IsRequired()
            .HasColumnType("nvarchar(300)");  // PostgreSQL'de nvarchar yerine varchar kullanıyoruz

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");  // datetime yerine timestamp kullanıyoruz

        builder.Property(c => c.IsApproved)
            .IsRequired()
            .HasColumnType("bit");  // bit yerine boolean kullanıyoruz

        builder.Property(c => c.UnsignedCommenterName)
            .HasColumnType("nvarchar(50)");  // nvarchar yerine varchar kullanıyoruz

        builder.HasOne(c => c.BlogPost)
            .WithMany(bp => bp.Comments)
            .HasForeignKey(c => c.BlogPostId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }

}
