using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Configuration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(80);
        builder
            .Property(p => p.Description)
            .HasMaxLength(400);
    }
}
