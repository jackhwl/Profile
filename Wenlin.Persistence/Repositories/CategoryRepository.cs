﻿using Microsoft.EntityFrameworkCore;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Persistence.Repositories;
public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(WenlinDbContext dbContext) : base(dbContext)
    {

    }
    public async Task<List<Category>> GetCategoriesWithProducts(bool includeDisabledProducts)
    {
        // test includeDisabledProducts
        var allCategories = await _dbContext.Set<Category>().Include(c => c.Products).ToListAsync();
        
        return allCategories;
    }
}
