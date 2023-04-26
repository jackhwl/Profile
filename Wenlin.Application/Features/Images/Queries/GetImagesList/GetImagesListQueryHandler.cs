using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Images.Queries.GetImagesList;
public class GetImagesListQueryHandler : IRequestHandler<GetImagesListQuery, GetImagesListQueryResponse>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public GetImagesListQueryHandler(IMapper mapper, IImageRepository imageRepository)
    {
        _mapper = mapper;
        _imageRepository = imageRepository;
    }

    public async Task<GetImagesListQueryResponse> Handle(GetImagesListQuery request, CancellationToken cancellationToken)
    {
        var getImagesListQueryResponse = new GetImagesListQueryResponse();

        if (request.OwnerId == null)
        {
            getImagesListQueryResponse.Success = false;
            getImagesListQueryResponse.NotFound = true;

            return getImagesListQueryResponse;
        }
        var categoryImages = (await _imageRepository.ListAllAsync(Guid.Parse(request.OwnerId))).OrderBy(p => p.FileName);

        getImagesListQueryResponse.ImageListDto = _mapper.Map<List<ImageListDto>>(categoryImages);

        return getImagesListQueryResponse;
    }
}
