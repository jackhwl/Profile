using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
public class PartiallyUpdateProductCommandHandler : IRequestHandler<PartiallyUpdateProductCommand, PartiallyUpdateProductCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public PartiallyUpdateProductCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PartiallyUpdateProductCommandResponse> Handle(PartiallyUpdateProductCommand request, CancellationToken cancellationToken)
    {
        var partiallyUpdateProductCommandResponse = new PartiallyUpdateProductCommandResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            partiallyUpdateProductCommandResponse.Success = false;
            partiallyUpdateProductCommandResponse.NotFound = true;

            return partiallyUpdateProductCommandResponse;
        }

        var product = await _productRepository.GetByIdAsync(category.Id, request.Id);
        if (product == null)
        {
            var productDto = new ProductForUpdateDto();
            request.PatchDocument.ApplyTo(productDto);

            //if (!TryValidateModel(productDto))
            //{
            //    return ValidationProblem(ModelState);
            //}

            var productToAdd = _mapper.Map<Product>(productDto);
            productToAdd.Id = request.Id;

            var productAdded = await _productRepository.AddAsync(productToAdd);
            partiallyUpdateProductCommandResponse.IsAddProduct= true;
            partiallyUpdateProductCommandResponse.Product = _mapper.Map<ProductForUpdateDto>(productAdded);

            return partiallyUpdateProductCommandResponse;
        }


        //var validator = new UpdateProductCommandValidator();
        //var validationResult = await validator.ValidateAsync(request);

        //if (validationResult.Errors.Count > 0)
        //{
        //    partiallyUpdateProductCommandResponse.Success = false;
        //    partiallyUpdateProductCommandResponse.ValidationErrors = new List<string>();
        //    foreach (var error in validationResult.Errors)
        //    {
        //        partiallyUpdateProductCommandResponse.ValidationErrors.Add(error.ErrorMessage);
        //    }
        //}

        var productToPatch = _mapper.Map<ProductForUpdateDto>(product);
        request.PatchDocument.ApplyTo(productToPatch); //, ModelState);

        //if (!TryValidateModel(courseToPatch))
        //{
        //    return ValidationProblem(ModelState);
        //}

        _mapper.Map(productToPatch, product);
        await _productRepository.UpdateAsync(product);

        return partiallyUpdateProductCommandResponse;

    }
}
