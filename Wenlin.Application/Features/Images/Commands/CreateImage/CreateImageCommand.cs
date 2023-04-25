using MediatR;

namespace Wenlin.Application.Features.Images.Commands.CreateImage;
public class CreateImageCommand : IRequest<CreateImageCommandResponse>
{
    public string Title { get; set; } = string.Empty;

    public byte[] Bytes { get; set; } = default!;
    public Guid OwnerId { get; set; } = default!;
    public string WebRootPath { get; set; } = string.Empty;
}
