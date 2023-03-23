using MediatR;
using Wenlin.Application.Features.Products.Commands.CreateProduct;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;
public class CreateCategoryCollectionCommand : IRequest<CreateCategoryCollectionCommandResponse>
{
    public ICollection<CreateCategoryCommandDto> CategoryCollection { get; set; } = new List<CreateCategoryCommandDto>();
}
