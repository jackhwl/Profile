using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

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
        var UpdateProductCommandResponse = new UpdateProductCommandResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category == null)
        {
            UpdateProductCommandResponse.Success = false;
            UpdateProductCommandResponse.NotFound = true;

            return UpdateProductCommandResponse;
        }

        var product = await _productRepository.GetByIdAsync(category.Id, request.Id);
        if (product == null)
        {
            // insert if product not found
            var productToAdd = _mapper.Map<Product>(request);
            productToAdd.Id = request.Id;

            await Utility.ValidateCommand(request, new UpdateProductCommandValidator(), UpdateProductCommandResponse);

            if (UpdateProductCommandResponse.Success)
            {
                var productAdded = await _productRepository.AddAsync(productToAdd);

                UpdateProductCommandResponse.IsAddProduct = true;
                UpdateProductCommandResponse.Product = _mapper.Map<ProductForInsert>(productAdded);

                // return not found if category is null
                // UpdateProductCommandResponse.Success = false;
                // UpdateProductCommandResponse.NotFound = true;
            }

            return UpdateProductCommandResponse;
        }

        await Utility.ValidateCommand(request, new UpdateProductCommandValidator(), UpdateProductCommandResponse);

        if (UpdateProductCommandResponse.Success)
        {
            _mapper.Map(request, product);
            await _productRepository.UpdateAsync(product);
        }

        return UpdateProductCommandResponse;
    }


}
