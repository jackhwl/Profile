using MediatR;

namespace Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;
public class GetCategoryDetailQuery : IRequest<CategoryDetailVm>
{
    public Guid Id { get; set; }
}
