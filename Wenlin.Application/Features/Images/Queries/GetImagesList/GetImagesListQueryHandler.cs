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

        var categoryImages = (await _imageRepository.ListAllAsync()).OrderBy(p => p.FileName);

        getImagesListQueryResponse.ImageListDto = _mapper.Map<List<ImageListDto>>(categoryImages);

        return getImagesListQueryResponse;
    }
}
