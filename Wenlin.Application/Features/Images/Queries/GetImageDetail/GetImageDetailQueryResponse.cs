using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Images.Queries.GetImageDetail;
public class GetImageDetailQueryResponse : BaseResponse
{
	public GetImageDetailQueryResponse() : base()
	{

	}
    public ImageDetailDto ImageDetailDto { get; set; } = default!;
}
