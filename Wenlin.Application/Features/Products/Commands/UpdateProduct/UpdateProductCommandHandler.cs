using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IActionResult>
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

    public async Task<IActionResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.ProductForUpdate.CategoryId);
        if (category == null)
        {
            return request.Controller.NotFound();
        }

        var product = await _productRepository.GetByIdAsync(category.Id, request.ProductForUpdate.Id);
        if (product == null)
        {
            // insert if product not found
            var productToAdd = _mapper.Map<Product>(request);
            productToAdd.Id = request.ProductForUpdate.Id;

            if (!request.Controller.TryValidateModel(productToAdd))
            {
                return request.Controller.ValidationProblem(request.Controller.ModelState);
            }

            var productAdded = await _productRepository.AddAsync(productToAdd);

            return request.Controller.CreatedAtRoute("GetProductById", 
                new { categoryId = request.ProductForUpdate.CategoryId, id = request.ProductForUpdate.Id }, 
                _mapper.Map<ProductForInsert>(productAdded));
        }

        var validator = new UpdateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        foreach (var error in validationResult.Errors)
        {
            request.Controller.ModelState.AddModelError(error.ErrorCode, error.ErrorMessage);
        }

        _mapper.Map(request, product);

            if (!request.Controller.TryValidateModel(product))
            {
                return request.Controller.ValidationProblem(request.Controller.ModelState);
            }

            await _productRepository.UpdateAsync(product);

        return request.Controller.NoContent();
    }
}
