using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Images.Commands.CreateImage;
public class CreateImageCommandResponse : BaseResponse
{
	public CreateImageCommandResponse() : base()
	{

	}

    public CreateImageDto CreateImageDto { get; set; } = default!;
}
