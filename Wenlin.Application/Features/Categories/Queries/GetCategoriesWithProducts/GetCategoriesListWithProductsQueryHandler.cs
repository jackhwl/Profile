using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Categories.Queries.GetCategoriesWithProducts;
public class GetCategoriesListWithProductsQueryHandler : IRequestHandler<GetCategoriesListWithProductsQuery, List<CategoryProductListVm>>
{
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    public GetCategoriesListWithProductsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryProductListVm>> Handle(GetCategoriesListWithProductsQuery request, CancellationToken cancellationToken)
    {
        var list = await _categoryRepository.GetCategoriesWithProducts(request.IncludeDisabled);
        return _mapper.Map<List<CategoryProductListVm>>(list);
    }
}
