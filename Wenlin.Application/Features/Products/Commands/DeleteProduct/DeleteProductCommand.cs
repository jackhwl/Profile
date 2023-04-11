using MediatR;

namespace Wenlin.Application.Features.Products.Commands.DeleteProduct;
public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
{
    public Guid CategoryId { get; set; }
    public Guid ProductId { get; set; }
}
