namespace Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoriesWithProducts;
public class CategoryProductListVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<CategoryProductDto>? Products { get; set; } 
}
