using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updateProductCommandResponse = new UpdateProductCommandResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            updateProductCommandResponse.Success = false;
            updateProductCommandResponse.NotFound = true;

            return updateProductCommandResponse;
        }

        var product = await _productRepository.GetByIdAsync(category.Id, request.Id);
        if (product == null)
        {
            updateProductCommandResponse.Success = false;
            updateProductCommandResponse.NotFound = true;

            return updateProductCommandResponse;
        }

        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            updateProductCommandResponse.Success = false;
            updateProductCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                updateProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (updateProductCommandResponse.Success)
        {
            _mapper.Map(request, product);
            await _productRepository.UpdateAsync(product);
        }

        return updateProductCommandResponse;
    }
}
