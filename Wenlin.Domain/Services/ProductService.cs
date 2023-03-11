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

    public async Task<Product> GetProductAsync(Guid productGuid)
	{
		if (productGuid == Guid.Empty)
		{
			throw new ArgumentNullException(nameof(productGuid));
		}

#pragma warning disable CS8603 // Possible null reference return.
        return await context.Set<Product>().FirstOrDefaultAsync(p => p.ProductGuid == productGuid);
#pragma warning restore CS8603 // Possible null reference return.
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
