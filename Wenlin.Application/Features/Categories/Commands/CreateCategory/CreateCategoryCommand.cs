using MediatR;
using Wenlin.Application.Features.Products.Commands.CreateProduct;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<CreateProductCommand> Products { get; set; } = new List<CreateProductCommand>();
}
