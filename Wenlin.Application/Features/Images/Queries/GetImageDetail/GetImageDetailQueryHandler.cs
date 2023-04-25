using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Images.Queries.GetImageDetail;
public class GetImageDetailQueryHandler : IRequestHandler<GetImageDetailQuery, GetImageDetailQueryResponse>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public GetImageDetailQueryHandler(IMapper mapper, IImageRepository imageRepository)
    {
        _mapper = mapper;
        _imageRepository = imageRepository;
    }

    public async Task<GetImageDetailQueryResponse> Handle(GetImageDetailQuery request, CancellationToken cancellationToken)
    {
        var getImageDetailQueryResponse = new GetImageDetailQueryResponse();

        var image = await _imageRepository.GetByIdAsync(request.Id);
        if (image == null)
        {
            getImageDetailQueryResponse.Success = false;
            getImageDetailQueryResponse.NotFound = true;

            return getImageDetailQueryResponse;
        }

        getImageDetailQueryResponse.ImageDetailDto = _mapper.Map<ImageDetailDto>(image);

        return getImageDetailQueryResponse;
    }
}
