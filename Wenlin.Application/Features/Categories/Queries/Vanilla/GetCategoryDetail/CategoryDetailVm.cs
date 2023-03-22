namespace Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoryDetail;

public class CategoryDetailVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}