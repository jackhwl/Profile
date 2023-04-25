using MediatR;

namespace Wenlin.Application.Features.Images.Queries.GetImageDetail;
public class GetImageDetailQuery : IRequest<GetImageDetailQueryResponse>
{
    public Guid Id { get; set; }
}
