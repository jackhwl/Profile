using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Persistence.Configurations;
using Wenlin.Persistence.Repositories;

namespace Wenlin.Persistence;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IModelConfiguration, SqlModelConfiguration>();
        services.AddDbContext<WenlinDbContext>(options => {
            options.UseSqlServer(connectionString); //, sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly(typeof(PersistenceServiceRegistration).Assembly.FullName);
            //});
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        return services;
    }
}
