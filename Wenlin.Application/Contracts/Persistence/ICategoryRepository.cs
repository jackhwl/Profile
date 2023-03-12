﻿using Wenlin.Domain.Entities;

namespace Wenlin.Application.Contracts.Persistence;
public interface ICategoryRepository : IAsyncRepository<Category>
{
    Task<List<Category>> GetCategoriesWithProducts(bool includeDisabledProducts);
}
