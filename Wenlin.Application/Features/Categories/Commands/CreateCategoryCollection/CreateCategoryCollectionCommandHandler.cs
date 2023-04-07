using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Categories.Commands.CreateCategory;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;
public class CreateCategoryCollectionCommandHandler : IRequestHandler<CreateCategoryCollectionCommand, CreateCategoryCollectionCommandResponse>
{
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCollectionCommandHandler(IMapper mapper, IAsyncRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<CreateCategoryCollectionCommandResponse> Handle(CreateCategoryCollectionCommand request, CancellationToken cancellationToken)
    {
        var createCategoryCollectionCommandResponse = new CreateCategoryCollectionCommandResponse();

        foreach (var categoryCreateCommandDto in request.CategoryCollection)
        {
            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(_mapper.Map<CreateCategoryCommand>(categoryCreateCommandDto));

            if (validationResult.Errors.Count > 0)
            {
                if (createCategoryCollectionCommandResponse.Success)
                    createCategoryCollectionCommandResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>();
                createCategoryCollectionCommandResponse.Success = false;
                
                foreach (var error in validationResult.Errors)
                {
                    if (createCategoryCollectionCommandResponse.ValidationErrors!.ContainsKey(error.PropertyName))
                    {
                        var errorMsgs = createCategoryCollectionCommandResponse.ValidationErrors[error.PropertyName].ToList();
                        errorMsgs.Add(error.ErrorMessage);
                        createCategoryCollectionCommandResponse.ValidationErrors[error.PropertyName] = errorMsgs;
                    }
                    else
                        createCategoryCollectionCommandResponse.ValidationErrors.Add(error.PropertyName, new List<string> { error.ErrorMessage });
                }
            }
        }

        if (createCategoryCollectionCommandResponse.Success)
        {
            var categories = new List<CreateCategoryDto>();
            foreach ( var categoryCreateCommandDto in request.CategoryCollection)
            {
                var category = _mapper.Map<Category>(categoryCreateCommandDto);
                category = await _categoryRepository.AddAsync(category);
                categories.Add(_mapper.Map<CreateCategoryDto>(category));
                
            }
            createCategoryCollectionCommandResponse.Categories = categories;
        }

        return createCategoryCollectionCommandResponse;
    }
}
