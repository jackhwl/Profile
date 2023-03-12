using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Queries.GetProductDetail;
public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailVm>
{
    private readonly IAsyncRepository<Product> _productRepository;
    private readonly IAsyncRepository<Category> _categoryRepository;
    private readonly IMapper _mapper;

    public GetProductDetailQueryHandler(IMapper mapper, IAsyncRepository<Product> productRepository, IAsyncRepository<Category> categoryRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<ProductDetailVm> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        var productDetailDto = _mapper.Map<ProductDetailVm>(product);

        var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
        productDetailDto.Category = _mapper.Map<CategoryDto>(category);

        return productDetailDto;
    }
}
