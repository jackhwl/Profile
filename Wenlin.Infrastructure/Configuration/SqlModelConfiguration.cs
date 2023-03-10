using Microsoft.EntityFrameworkCore;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Infrastructure.Configuration;
internal class SqlModelConfiguration : IModelConfiguration
{
    public void ConfigureModel(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqlModelConfiguration).Assembly);
    }
}
