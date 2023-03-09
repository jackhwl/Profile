using Microsoft.EntityFrameworkCore;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain;
public class WenlinContext : DbContext
{
	public WenlinContext(DbContextOptions<WenlinContext> options) : base(options)
    {

	}

	public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WenlinContext).Assembly);
    }
}
