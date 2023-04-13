using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Configuration;
public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder
            .Property(p => p.DateOfBirth)  
            .IsRequired();
        builder
            .Property(p => p.MainCategory)
            .IsRequired()
            .HasMaxLength(50);
    }
}
