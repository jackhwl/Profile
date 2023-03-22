using AutoMapper;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoryDetail;
public class GetCategoryDetailQueryHandler : IQueryHandler<GetCategoryDetailQuery, CategoryDetailVm>
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryDetailQueryHandler(IAsyncRepository<Category> categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<CategoryDetailVm> Handle(GetCategoryDetailQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);
        var categoryVm = _mapper.Map<CategoryDetailVm>(category);

        return categoryVm;
    }
}
