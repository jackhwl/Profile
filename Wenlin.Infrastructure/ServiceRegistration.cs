using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wenlin.Domain;

namespace Wenlin.Infrastructure;
public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
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
