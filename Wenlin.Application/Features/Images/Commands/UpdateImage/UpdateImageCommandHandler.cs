using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Images.Commands.UpdateImage;
public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, UpdateImageCommandResponse>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public UpdateImageCommandHandler(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<UpdateImageCommandResponse> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {
        var updateImageCommandResponse = new UpdateImageCommandResponse();

        var image = await _imageRepository.GetByIdAsync(request.Id);

        if (image == null)
        {
            updateImageCommandResponse.Success = false;
            updateImageCommandResponse.NotFound = true;

            return updateImageCommandResponse;
        }

        if (updateImageCommandResponse.Success)
        {
            _mapper.Map(request, image);
            await _imageRepository.UpdateAsync(image);
        }

        return updateImageCommandResponse;
    }
}
