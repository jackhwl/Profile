using Microsoft.EntityFrameworkCore;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Persistence.Configurations;
public class SqlModelConfiguration : IModelConfiguration
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlModelConfiguration).Assembly);
    }
}
