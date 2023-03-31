using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Wenlin.Application.Features.Products.Commands.DeleteProduct;
public class DeleteProductCommand : IRequest<IActionResult>
{
    public Guid CategoryId { get; set; }
    public Guid ProductId { get; set; }
    public ControllerBase Controller { get; set; }
    public DeleteProductCommand(Guid categoryId, Guid productId, ControllerBase controller)
    {
        CategoryId= categoryId;
        ProductId= productId;
        Controller = controller;
    }
}
