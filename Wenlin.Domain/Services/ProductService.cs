using Microsoft.EntityFrameworkCore;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public class ProductService : IProductService
{
    private readonly WenlinContext context;
	public ProductService(WenlinContext context)
	{
		this.context = context;
	}

	public async Task<IEnumerable<Product>> GetProductsAsync()
	{
		return await context.Set<Product>().ToListAsync();
	}

	public async Task<Product> CreateProduct(string name, string description)
	{
		var product = new Product
		{
			Name = name,
			Description = description
		};

		await context.AddAsync(product);
		await context.SaveChangesAsync();

		return product;
	}
}
