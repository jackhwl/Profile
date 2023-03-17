using System.Collections.Generic;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface IProductRepository : IAsyncRepository<Product>
{
    Task<Product> GetByIdAsync(Guid categoryId, Guid productId);
    Task<IReadOnlyList<Product>> GetProductsByCategory(Guid categoryId);
    Task<bool> IsProductNameAndDescriptionUnique(string name, string description);
}
