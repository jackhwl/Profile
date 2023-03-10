using Microsoft.EntityFrameworkCore;

namespace Wenlin.SharedKernel.Configuration;
public interface IModelConfiguration
{
    void ConfigureModel(ModelBuilder modelBuilder);
}
