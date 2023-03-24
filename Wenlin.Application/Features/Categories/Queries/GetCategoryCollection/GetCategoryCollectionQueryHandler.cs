using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Categories.Queries.GetCategoryCollection;
public class GetCategoryCollectionQueryHandler : IRequestHandler<GetCategoryCollectionQuery, List<CategoryCollectionVm>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryCollectionQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryCollectionVm>> Handle(GetCategoryCollectionQuery request, CancellationToken cancellationToken)
    {
        var allCategories = (await _categoryRepository.GetCategoriesByIdAsync(request.CategoryIds)).OrderBy(c => c.Name);
        return _mapper.Map<List<CategoryCollectionVm>>(allCategories);
    }
}
