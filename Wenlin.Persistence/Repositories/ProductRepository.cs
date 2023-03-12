using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.Repositories;
public class ProductRepository : BaseRepository<Product>, IProductRepository
{
	public ProductRepository(WenlinDbContext dbContext) : base(dbContext)
	{

	}

    public Task<bool> IsProductNameAndDescriptionUnique(string name, string description)
    {
        var matches = _dbContext.Set<Product>().Any(p => p.Name.Equals(name) && (string.IsNullOrWhiteSpace(p.Description) ? string.IsNullOrWhiteSpace(description) : p.Description.Equals(description)));

        return Task.FromResult(matches);
    }
}
