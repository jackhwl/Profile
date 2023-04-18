using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Images.Queries.GetImagesList;
public class GetImagesListQueryResponse : BaseResponse
{
	public GetImagesListQueryResponse() : base()
	{

	}
    public List<ImageListDto> ImageListDto { get; set; } = default!;
}
