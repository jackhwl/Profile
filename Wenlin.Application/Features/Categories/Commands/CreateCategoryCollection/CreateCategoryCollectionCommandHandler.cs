using AutoMapper;
using MediatR;
using System.Collections.Generic;
using Wenlin.Application.Contracts.Persistence;
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

        //var validator = new CreateCategoryCollectionCommandValidator();
        //var validationResult = await validator.ValidateAsync(request);

        //if (validationResult.Errors.Count > 0)
        //{
        //    createCategoryCommandResponse.Success = false;
        //    createCategoryCommandResponse.ValidationErrors = new List<string>();
        //    foreach (var error in validationResult.Errors)
        //    {
        //        createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
        //    }
        //}

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
