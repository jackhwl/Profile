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

        return services;
    }
}
