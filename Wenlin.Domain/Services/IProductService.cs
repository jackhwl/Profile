using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public interface IProductService
{
    Task<Product> GetProductAsync(Guid productGuid);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> CreateProduct(string name, string description);
}
