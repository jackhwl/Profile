using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain;
using Wenlin.Persistence.Configurations;
using Wenlin.Persistence.Repositories;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Persistence;
public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IModelConfiguration, SqlModelConfiguration>();
        services.AddDbContext<WenlinContext>(options => {
            options.UseSqlServer(connectionString); //, sqlOptions =>
            //{
            //    sqlOptions.MigrationsAssembly(typeof(PersistenceServiceRegistration).Assembly.FullName);
            //});
        });

        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
