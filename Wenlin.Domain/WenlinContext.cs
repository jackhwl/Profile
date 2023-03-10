using Microsoft.EntityFrameworkCore;
using Wenlin.Domain.Entities;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Domain;
public class WenlinContext : DbContext
{
    private readonly IModelConfiguration modelConfiguration;
	public WenlinContext(DbContextOptions<WenlinContext> options, IModelConfiguration modelConfiguration) : base(options)
    {
        this.modelConfiguration = modelConfiguration;
	}

	public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WenlinContext).Assembly);
        modelConfiguration.ConfigureModel(modelBuilder);
    }
}
