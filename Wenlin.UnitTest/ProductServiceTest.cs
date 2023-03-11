using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using Wenlin.Domain;
using Wenlin.Domain.Entities;
using Wenlin.Domain.Services;

namespace Wenlin.UnitTest;

public class ProductServiceTest
{
    private readonly ProductService productService;
    public ProductServiceTest()
    {
        var options = new DbContextOptionsBuilder<WenlinContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new WenlinContext(options, new TestModelConfiguration());
        productService = new ProductService(context);
    }

    [Fact]
    public async Task CanCreateProduct()
    {
        Product product = await GivenProduct();
        product.Name.Should().Be("Test Product");
    }

    private async Task<Product> GivenProduct(
    string name = "Test Product",
    string description = "100 Test Street, Testertown, TS 99999"
)
    {
        var product = new Product() { Name = name, Description = description, ProductGuid = Guid.NewGuid() };
        await productService.AddProductAsync(product);
        return product;
    }
}