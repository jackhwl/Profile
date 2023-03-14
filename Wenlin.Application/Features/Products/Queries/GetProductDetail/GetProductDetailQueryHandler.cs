using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Queries.GetProductDetail;
public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, GetProductDetailQueryResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetProductDetailQueryHandler(IMapper mapper, IProductRepository productRepository, IAsyncRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<GetProductDetailQueryResponse> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var getProductDetailQueryResponse = new GetProductDetailQueryResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category == null) 
        {
            getProductDetailQueryResponse.Success= false;
            getProductDetailQueryResponse.NotFound= true;

            return getProductDetailQueryResponse;
        } 

        var product = await _productRepository.GetByIdAsync(category.Id, request.ProductId);

        if (product == null)
        {
            getProductDetailQueryResponse.Success = false;
            getProductDetailQueryResponse.NotFound = true;

            return getProductDetailQueryResponse;
        }

        var productDetailVm = _mapper.Map<ProductDetailVm>(product);
        productDetailVm.Category = _mapper.Map<CategoryDto>(category);
        getProductDetailQueryResponse.ProductDetailVm = productDetailVm;

        return getProductDetailQueryResponse;
    }
}
