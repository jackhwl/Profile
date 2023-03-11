using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public interface ICategoryService
{
    Task<Category> GetCategoryAsync(Guid id);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task AddCategoryAsync(Category category);
    Task<bool> CategoryExistsAsync(Guid id);
    void DeleteCategory(Category category);
    Task<bool> SaveAsync();
}
