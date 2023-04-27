using MediatR;

namespace Wenlin.Application.Features.Images.Commands.UpdateImage;
public class UpdateImageCommand : IRequest<UpdateImageCommandResponse>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
