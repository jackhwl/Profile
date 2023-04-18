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
        ;

        builder
            .Property(p => p.OwnerId)
            .HasColumnName("CustomerId")
            .HasColumnType("UniqueIdentifier")
            ;

    }
}
