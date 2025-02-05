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
            .HasColumnType("varchar(50)");  // nvarchar yerine varchar kullanıyoruz

        builder.Property(cm => cm.Email)
            .IsRequired()
            .HasColumnType("varchar(100)");  // nvarchar yerine varchar kullanıyoruz

        builder.Property(cm => cm.Subject)
            .HasColumnType("varchar(100)");  // nvarchar yerine varchar kullanıyoruz

        builder.Property(cm => cm.Message)
            .IsRequired()
            .HasColumnType("text");  // nvarchar(max) yerine text kullanıyoruz

        builder.Property(c => c.SentDate)
            .IsRequired()
            .HasColumnType("timestamp");  // datetime yerine timestamp kullanıyoruz

        builder.Property(c => c.IsRead)
            .IsRequired()
            .HasColumnType("boolean");  // bit yerine boolean kullanıyoruz

        builder.Property(c => c.ReplyDate)
            .HasColumnType("timestamp");  // datetime yerine timestamp kullanıyoruz
    }

}
