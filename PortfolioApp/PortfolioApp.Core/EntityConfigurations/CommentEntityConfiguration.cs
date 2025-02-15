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
            .HasColumnType("nvarchar(300)");

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("datetime");

        builder.Property(c => c.IsApproved)
            .IsRequired()
            .HasColumnType("bit");

        builder.Property(c => c.UnsignedCommenterName)
            .HasColumnType("nvarchar(30)");

        builder.Property(c => c.SignedCommenterName)
            .HasColumnType("nvarchar(50)");

        builder.Property(c => c.UserId).HasColumnType("int");

        builder.HasOne(c => c.BlogPost)
            .WithMany(bp => bp.Comments)
            .HasForeignKey(c => c.BlogPostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }

}
