using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Queries.GetProductsList;
public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, GetProductsListQueryResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;

    private readonly IMapper _mapper;

    public GetProductsListQueryHandler(IMapper mapper, IProductRepository productRepository, IAsyncRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<GetProductsListQueryResponse> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var getProductsListQueryResponse = new GetProductsListQueryResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category == null)
        {
            getProductsListQueryResponse.Success = false;
            getProductsListQueryResponse.NotFound = true;

            return getProductsListQueryResponse;
        }

        var categoryProducts = (await _productRepository.GetProductsByCategory(category.Id)).OrderBy(p => p.Name);

        getProductsListQueryResponse.ProductListVm = _mapper.Map<List<ProductListVm>>(categoryProducts);

        return getProductsListQueryResponse;
    }
}
