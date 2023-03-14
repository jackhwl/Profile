using MediatR;

namespace Wenlin.Application.Features.Products.Queries.GetProductDetail;
public class GetProductDetailQuery : IRequest<GetProductDetailQueryResponse>
{
    public Guid CategoryId { get; set; }
    public Guid ProductId { get; set; }
}
