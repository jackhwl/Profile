using MediatR;

namespace Wenlin.Application.Features.Products.Queries.GetProductsList;
public class GetProductsListQuery : IRequest<GetProductsListQueryResponse>
{
    public Guid CategoryId { get; set; }
}
