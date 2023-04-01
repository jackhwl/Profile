using Microsoft.EntityFrameworkCore;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Persistence.Configurations;
internal class SqlModelConfiguration : IModelConfiguration
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlModelConfiguration).Assembly);
    }
}
