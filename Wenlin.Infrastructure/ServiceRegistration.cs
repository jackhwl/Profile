using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wenlin.Domain;
using Wenlin.Domain.Services;
using Wenlin.Infrastructure.Configuration;
using Wenlin.SharedKernel.Configuration;

namespace Wenlin.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IModelConfiguration, SqlModelConfiguration>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddDbContext<WenlinContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ServiceRegistration).Assembly.FullName);
            });
        });

        return services;
    }
}
