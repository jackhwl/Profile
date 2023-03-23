﻿namespace Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoriesWithProducts;

public class CategoryProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}