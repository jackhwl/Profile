using Microsoft.EntityFrameworkCore;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain;
using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.Repositories;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
	public ProductRepository(WenlinContext dbContext) : base(dbContext)
	{

	}

    public async Task<Product> GetByIdAsync(Guid categoryId, Guid productId)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _dbContext.Set<Product>()
            .Where(p => p.CategoryId == categoryId && p.Id == productId).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<IReadOnlyList<Product>> GetProductsByCategory(Guid categoryId)
    {
        var products = await _dbContext.Set<Product>().Where(p => p.CategoryId == categoryId).ToListAsync();

        return products;
    }

    public Task<bool> IsProductNameAndDescriptionUnique(string name, string description)
    {
        var matches = _dbContext.Set<Product>().Any(p => p.Name.Equals(name) && (string.IsNullOrWhiteSpace(p.Description) ? string.IsNullOrWhiteSpace(description) : p.Description.Equals(description)));

        return Task.FromResult(matches);
    }
}
