using AutoMapper;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoriesWithProducts;
public class GetCategoriesListWithProductsQueryHandler : IQueryHandler<GetCategoriesListWithProductsQuery, List<CategoryProductListVm>>
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
