using Microsoft.EntityFrameworkCore;
using Wenlin.Domain;

namespace Wenlin.Application.UnitTest;
internal static class DbContextFactory
{
    public static WenlinContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<WenlinContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new WenlinContext(options, new TestModelConfiguration());

        return context;
    }
}
