using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var updateCategoryCommandResponse = new UpdateCategoryCommandResponse();

        var category = await _categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
        {            
            // insert if category not found
            var categoryToAdd = _mapper.Map<Category>(request);
            categoryToAdd.Id = request.Id;

            var categoryAdded = await _categoryRepository.AddAsync(categoryToAdd);
            updateCategoryCommandResponse.IsAddCategory = true;
            updateCategoryCommandResponse.Category = _mapper.Map<CategoryForInsert>(categoryAdded);

            // return not found if category is null
            // updateCategoryCommandResponse.Success = false;
            // updateCategoryCommandResponse.NotFound = true;

            return updateCategoryCommandResponse;
        }

        _mapper.Map(request, category);
        await _categoryRepository.UpdateAsync(category);

        return updateCategoryCommandResponse;
    }
}
