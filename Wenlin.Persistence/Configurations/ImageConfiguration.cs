using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.Configurations;
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder
            .ToTable("CustomerImage")
            .HasOne(i => i.Customer)
            .WithMany(c => c.Images)
            .HasForeignKey(i => i.OwnerId)
            ;

        builder
            .Property(p => p.OwnerId)
            .HasColumnName("CustomerId")
            .HasColumnType("UniqueIdentifier")
            ;
    }
}
