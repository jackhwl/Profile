using MediatR;

namespace Wenlin.Application.Features.Images.Commands.DeleteImage;
public class DeleteImageCommand : IRequest<DeleteImageCommandResponse>
{
    public Guid Id { get; set; }
}
