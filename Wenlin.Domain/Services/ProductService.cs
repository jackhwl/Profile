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

    public async Task<Product> GetProductAsync(Guid categoryId, Guid productId)
    {
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }

        if (productId == Guid.Empty)
		{
			throw new ArgumentNullException(nameof(productId));
		}

#pragma warning disable CS8603 // Possible null reference return.
        return await context.Set<Product>()
            .Where(p => p.CategoryId == categoryId && p.Id == productId).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(Guid categoryId)
	{
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }

        return await context.Set<Product>()
            .Where(p => p.CategoryId == categoryId)
            .OrderBy(p => p.Name).ToListAsync();
	}

	public async Task AddProductAsync(Guid categoryId, Product product)
    {
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }

        if (product == null)
		{
			throw new ArgumentNullException(nameof(product));
		}

        product.CategoryId = categoryId;
		product.Id = Guid.NewGuid();

		await context.Set<Product>().AddAsync(product);
	}

    public async Task<bool> CategoryExistsAsync(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(categoryId));
        }

        return await context.Set<Category>().AnyAsync(c => c.Id == categoryId);
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
