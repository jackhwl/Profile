namespace Wenlin.Application.Features.Categories.Queries.GetCategoryCollection;

public class CategoryCollectionVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set;}
}