using Microsoft.EntityFrameworkCore;

namespace Wenlin.Persistence.Configurations;
public interface IModelConfiguration
{
    void ConfigureModel(ModelBuilder modelBuilder);
}