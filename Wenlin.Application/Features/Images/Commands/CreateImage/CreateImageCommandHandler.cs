using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Images.Commands.CreateImage;
public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, CreateImageCommandResponse>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public CreateImageCommandHandler(IImageRepository imageRepository, IMapper mapper)
	{
        _imageRepository= imageRepository;
        _mapper= mapper;
	}

    public async Task<CreateImageCommandResponse> Handle(CreateImageCommand request, CancellationToken cancellationToken)
    {
        var createImageCommandResponse = new CreateImageCommandResponse();

        // Automapper maps only the Title in our configuration
        var imageEntity = _mapper.Map<Image>(request);

        // Create an image from the passed-in bytes (Base64), and 
        // set the filename on the image

        // get this environment's web root path (the path
        // from which static content, like an image, is served)
        var webRootPath = request.WebRootPath;

        // create the filename
        string fileName = Guid.NewGuid().ToString() + ".jpg";

        // the full file path
        var filePath = Path.Combine($"{webRootPath}/images/{fileName}");

        // write bytes and auto-close stream
        await File.WriteAllBytesAsync(filePath, request.Bytes);

        // fill out the filename
        imageEntity.FileName = fileName;

        // ownerId should be set - can't save image in starter solution, will
        // be fixed during the course
        //imageEntity.OwnerId = ...;

        // set the ownerId on the imageEntity
        //var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        //if (ownerId == null)
        //{
        //    throw new Exception("User identifier is missing from token.");
        //}
        //imageEntity.OwnerId = ownerId;

        // add and save.  
        var image = await _imageRepository.AddAsync(imageEntity);

        createImageCommandResponse.CreateImageDto = _mapper.Map<CreateImageDto>(image);

        return createImageCommandResponse;
    }
}
