using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Models.Mail;
using Wenlin.Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wenlin.Infrastructure.FileExport;

namespace Wenlin.Infrastructure;
public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration emailSettingsConfiguration)
    {
        services.Configure<EmailSettings>(emailSettingsConfiguration);

        services.AddTransient<ICsvExporter, CsvExporter>();
        services.AddTransient<IEmailService, EmailService>();

        //services.AddSingleton<IModelConfiguration, SqlModelConfiguration>();
        //services.AddScoped<IProductService, ProductService>();
        //services.AddScoped<ICategoryService, CategoryService>();

        //services.AddDbContext<WenlinContext>(options =>
        //{
        //    options.UseSqlServer(connectionString, sqlOptions =>
        //    {
        //        sqlOptions.MigrationsAssembly(typeof(InfrastructureServiceRegistration).Assembly.FullName);
        //    });
        //});

        return services;
    }
}
