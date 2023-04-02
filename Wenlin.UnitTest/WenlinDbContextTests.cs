using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.IntegrationTest;
public class WenlinDbContextTests
{
    //private readonly WenlinDbContext _wenlinDbContext;
    //private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
    private readonly string _loggedInUserId;

    public WenlinDbContextTests()
    {
        //var dbContextOptions = new DbContextOptionsBuilder<WenlinDbContext>()
        //    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _loggedInUserId = "00000000-0000-0000-0000-000000000000";
        //_loggedInUserServiceMock = new Mock<ILoggedInUserService>();
        //_loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

        //_wenlinDbContext = new WenlinDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
    }

    //[Fact]
    //public async void Save_SetCreatedByProperty()
    //{
    //    var product = new Product() { Id = Guid.NewGuid(), Name = "Test product" };
    //    _wenlinDbContext.Set<Product>().Add(product);
    //    await _wenlinDbContext.SaveChangesAsync();

    //    //product.Name.s.ShouldBe(_loggedInUserId);
    //}
}
