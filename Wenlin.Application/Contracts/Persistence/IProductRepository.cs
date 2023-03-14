using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface IProductRepository : IAsyncRepository<Product>
{
    Task<Product> GetByIdAsync(Guid categoryId, Guid productId);
    Task<bool> IsProductNameAndDescriptionUnique(string name, string description);
}
