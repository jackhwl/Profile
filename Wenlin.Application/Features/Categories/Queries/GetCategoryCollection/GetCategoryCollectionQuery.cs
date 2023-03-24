using MediatR;

namespace Wenlin.Application.Features.Categories.Queries.GetCategoryCollection;
public class GetCategoryCollectionQuery : IRequest<List<CategoryCollectionVm>>
{
    public IEnumerable<Guid> CategoryIds { get; set; } = new List<Guid>();
}
