using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Products.Commands.DeleteProduct;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
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

    public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var deleteProductCommandResponse = new DeleteProductCommandResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if (category == null)
        {
            deleteProductCommandResponse.Success = false;
            deleteProductCommandResponse.NotFound = true;

            return deleteProductCommandResponse;
        }

        var product = await _productRepository.GetByIdAsync(request.CategoryId, request.ProductId);

        if (product == null)
        {
            deleteProductCommandResponse.Success = false;
            deleteProductCommandResponse.NotFound = true;
        }
        else
            await _productRepository.DeleteAsync(product);

        return deleteProductCommandResponse;
    }
}
