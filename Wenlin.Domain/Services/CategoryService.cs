using Microsoft.EntityFrameworkCore;
using Wenlin.Domain.Entities;

namespace Wenlin.Domain.Services;
public class CategoryService : ICategoryService
{
    private readonly WenlinContext context;
	public CategoryService(WenlinContext context)
	{
		this.context = context;
	}

    public async Task<Category> GetCategoryAsync(Guid id)
	{
		if (id == Guid.Empty)
		{
			throw new ArgumentNullException(nameof(id));
		}

#pragma warning disable CS8603 // Possible null reference return.
        return await context.Set<Category>().FirstOrDefaultAsync(p => p.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
	{
		return await context.Set<Category>().ToListAsync();
	}

	public async Task AddCategoryAsync(Category category)
	{
		if (category == null)
		{
			throw new ArgumentNullException(nameof(category));
		}

		category.Id = Guid.NewGuid();

		await context.Set<Category>().AddAsync(category);
	}

    public async Task<bool> CategoryExistsAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(id));
        }

        return await context.Set<Category>().AnyAsync(c => c.Id == id);
    }

    public void DeleteCategory(Category category)
    {
        if (category == null)
        {
            throw new ArgumentNullException(nameof(category));
        }

        context.Set<Category>().Remove(category);
    }

    public async Task<bool> SaveAsync()
    {
        return (await context.SaveChangesAsync() >= 0);
    }
}
