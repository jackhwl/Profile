using AutoMapper;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoriesList;
public class GetCategoriesListQueryHandler : IQueryHandler<GetCategoriesListQuery, List<CategoryListVm>>
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesListQueryHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryListVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        var allCategories = (await _categoryRepository.ListAllAsync()).OrderBy(c => c.Name);
        return _mapper.Map<List<CategoryListVm>>(allCategories);
    }
}
