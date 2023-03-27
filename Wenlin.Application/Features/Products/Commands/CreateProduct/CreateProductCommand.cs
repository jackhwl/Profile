using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Wenlin.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommand : IRequest<CreateProductCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
}
