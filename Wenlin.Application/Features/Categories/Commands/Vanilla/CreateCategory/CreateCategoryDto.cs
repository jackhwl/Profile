namespace Wenlin.Application.Features.Categories.Commands.Vanilla.CreateCategory;

public class CreateCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}