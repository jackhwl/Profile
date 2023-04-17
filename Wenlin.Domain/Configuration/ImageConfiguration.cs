using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Configuration;
public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(150);
        builder
            .Property(p => p.FileName)
            .IsRequired()
            .HasMaxLength(200);
        builder
            .Property(p => p.OwnerId)
            .IsRequired()
            .HasMaxLength(50);
    }
}
