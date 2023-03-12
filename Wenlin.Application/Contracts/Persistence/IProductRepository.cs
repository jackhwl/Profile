using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface IProductRepository : IAsyncRepository<Product>
{
    Task<bool> IsProductNameAndDescriptionUnique(string name, string description);
}
