using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> CreateProduct(string name, string description);
}
