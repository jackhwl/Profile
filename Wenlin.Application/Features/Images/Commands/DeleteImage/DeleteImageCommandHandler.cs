using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Images.Commands.DeleteImage;
public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, DeleteImageCommandResponse>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public DeleteImageCommandHandler(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<DeleteImageCommandResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var deleteImageCommandResponse = new DeleteImageCommandResponse();

        var imageToDelete = await _imageRepository.GetByIdAsync(request.Id);
        if (imageToDelete == null)
        {
            deleteImageCommandResponse.Success = false;
            deleteImageCommandResponse.NotFound = true;
        }
        else
        {
            await _imageRepository.DeleteAsync(imageToDelete);
        }

        return deleteImageCommandResponse;
    }
}
