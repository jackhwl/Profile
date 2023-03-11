using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public interface IProductService
{
    Task<Product> GetProductAsync(Guid categoryId, Guid productId);
    Task<IEnumerable<Product>> GetProductsAsync(Guid categoryId);
    Task AddProductAsync(Guid categoryId, Product product);
    Task<bool> CategoryExistsAsync(Guid categoryId);
    void DeleteProduct(Product product);
    Task<bool> SaveAsync();
}
