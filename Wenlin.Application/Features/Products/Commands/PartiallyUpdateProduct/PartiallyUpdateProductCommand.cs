using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
public class PartiallyUpdateProductCommand : IRequest<PartiallyUpdateProductCommandResponse>
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public JsonPatchDocument<ProductForUpdateDto> PatchDocument { get; set; } = default!;
}
