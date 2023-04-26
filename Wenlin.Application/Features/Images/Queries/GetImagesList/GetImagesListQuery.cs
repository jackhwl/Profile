using MediatR;

namespace Wenlin.Application.Features.Images.Queries.GetImagesList;
public class GetImagesListQuery : IRequest<GetImagesListQueryResponse>
{
    public string? OwnerId { get; set; }
}
