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

	public async Task AddProductAsync(Product product)
	{
		if (product == null)
		{
			throw new ArgumentNullException(nameof(product));
		}

		product.ProductGuid = Guid.NewGuid();

		await context.Set<Product>().AddAsync(product);
	}

    public async Task<bool> ProductExistsAsync(Guid productGuid)
    {
        if (productGuid == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(productGuid));
        }

        return await context.Set<Product>().AnyAsync(p => p.ProductGuid == productGuid);
    }

    public void DeleteProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        context.Set<Product>().Remove(product);
    }

    public async Task<bool> SaveAsync()
    {
        return (await context.SaveChangesAsync() >= 0);
    }
}
