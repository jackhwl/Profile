using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IActionResult>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IActionResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            return request.Controller.NotFound();
        }

        var product = await _productRepository.GetByIdAsync(category.Id, request.ProductId);
        if (product == null)
        {
            return request.Controller.NotFound();
        }

        await _productRepository.DeleteAsync(product);

        return request.Controller.NoContent();
    }
}
