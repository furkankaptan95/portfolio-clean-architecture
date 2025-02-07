using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.Entities;

namespace PortfolioApp.Core.EntityConfigurations;
public class ContactMessageEntityConfiguration : IEntityTypeConfiguration<ContactMessageEntity>
{
    public void Configure(EntityTypeBuilder<ContactMessageEntity> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Id).ValueGeneratedOnAdd();

        builder.Property(cm => cm.Name)
            .IsRequired()
            .HasColumnType("nvarchar(50)");  // nvarchar yerine varchar kullanıyoruz

        builder.Property(cm => cm.Email)
            .IsRequired()
            .HasColumnType("nvarchar(100)");  // nvarchar yerine varchar kullanıyoruz

        builder.Property(cm => cm.Subject)
            .HasColumnType("nvarchar(100)");  // nvarchar yerine varchar kullanıyoruz

        builder.Property(cm => cm.Message)
            .IsRequired()
            .HasColumnType("nvarchar(max)");  // nvarchar(max) yerine text kullanıyoruz

        builder.Property(c => c.SentDate)
            .IsRequired()
            .HasColumnType("datetime");  // datetime yerine timestamp kullanıyoruz

        builder.Property(c => c.IsRead)
            .IsRequired()
            .HasColumnType("bit");  // bit yerine boolean kullanıyoruz

        builder.Property(c => c.ReplyDate)
            .HasColumnType("datetime");  // datetime yerine timestamp kullanıyoruz
    }

}
