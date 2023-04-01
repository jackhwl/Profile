using Microsoft.EntityFrameworkCore;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Persistence;
public class WenlinDbContext : DbContext
{
    private readonly IModelConfiguration modelConfiguration;
	public WenlinDbContext(DbContextOptions<WenlinDbContext> options, IModelConfiguration modelConfiguration) : base(options)
    {
        this.modelConfiguration = modelConfiguration;
	}

	//public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WenlinDbContext).Assembly);
        modelConfiguration.ConfigureModel(modelBuilder);
    }
}
