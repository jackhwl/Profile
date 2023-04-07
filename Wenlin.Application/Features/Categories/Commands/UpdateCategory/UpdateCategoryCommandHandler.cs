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

            await ValidateUpdateCommand(request, updateCategoryCommandResponse);

            if (updateCategoryCommandResponse.Success)
            {
                var categoryAdded = await _categoryRepository.AddAsync(categoryToAdd);
            
                updateCategoryCommandResponse.IsAddCategory = true;
                updateCategoryCommandResponse.Category = _mapper.Map<CategoryForInsert>(categoryAdded);

                // return not found if category is null
                // updateCategoryCommandResponse.Success = false;
                // updateCategoryCommandResponse.NotFound = true;
            }

            return updateCategoryCommandResponse;
        }

        await ValidateUpdateCommand(request, updateCategoryCommandResponse);

        if (updateCategoryCommandResponse.Success)
        {
            _mapper.Map(request, category);
            await _categoryRepository.UpdateAsync(category);
        }

        return updateCategoryCommandResponse;
    }

    private static async Task ValidateUpdateCommand(UpdateCategoryCommand request, UpdateCategoryCommandResponse updateCategoryCommandResponse)
    {
        var validator = new UpdateCategoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            updateCategoryCommandResponse.Success = false;
            updateCategoryCommandResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            foreach (var error in validationResult.Errors)
            {
                if (updateCategoryCommandResponse.ValidationErrors.ContainsKey(error.PropertyName))
                {
                    var errorMsgs = updateCategoryCommandResponse.ValidationErrors[error.PropertyName].ToList();
                    errorMsgs.Add(error.ErrorMessage);
                    updateCategoryCommandResponse.ValidationErrors[error.PropertyName] = errorMsgs;
                }
                else
                    updateCategoryCommandResponse.ValidationErrors.Add(error.PropertyName, new List<string> { error.ErrorMessage });
            }
        }
    }
}
