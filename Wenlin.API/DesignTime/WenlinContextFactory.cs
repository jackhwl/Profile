using Wenlin.Domain;
using Wenlin.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Wenlin.API.DesignTime;
public class WenlinContextFactory : IDesignTimeDbContextFactory<WenlinContext>
{
    private const string AdminConnectionString = "WENLIN_ADMIN_CONNECTION_STRING";

    public WenlinContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable(AdminConnectionString);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ApplicationException($"Please set the environment avriable {AdminConnectionString}");
        }

        var options = new DbContextOptionsBuilder<WenlinContext>()
            .UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(ServiceRegistration).Assembly.FullName);
            })
            .Options;
        return new WenlinContext(options, new DesignTimeModelConfiguration());
    }
}
