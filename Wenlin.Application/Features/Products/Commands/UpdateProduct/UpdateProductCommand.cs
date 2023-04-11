using MediatR;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommand : IRequest<UpdateProductCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
