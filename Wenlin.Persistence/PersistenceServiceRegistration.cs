using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Wenlin.Application.Contracts.Infrastructure;
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

        var repositoryTypes = Assembly.GetAssembly(typeof(BaseRepository<>))?.GetTypes()
            .Where(type => !type.IsAbstract && type.Name.EndsWith("Repository"));

        foreach (var repositoryType in repositoryTypes!)
        {
            var interfaceType = repositoryType.GetInterfaces()
                .FirstOrDefault(i => !i.IsGenericType);

            if (interfaceType == null)
            {
                throw new InvalidOperationException($"Repository {repositoryType.Name} must implement an interface other than IAsyncRepository.");
            }

            services.AddScoped(interfaceType, repositoryType);
        }

        return services;
    }
}
