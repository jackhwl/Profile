using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public interface IProductService
{
    Task<Product> GetProductAsync(Guid productGuid);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task AddProductAsync(Product product);
    Task<bool> ProductExistsAsync(Guid productGuid);
    void DeleteProduct(Product product);
    Task<bool> SaveAsync();
}
