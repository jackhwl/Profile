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

namespace Wenlin.Application.Features.Products.Queries.GetProductsList;
public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, List<ProductListVm>>
{
    private readonly IAsyncRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductsListQueryHandler(IMapper mapper, IAsyncRepository<Product> productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<List<ProductListVm>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
    {
        var allProducts = (await _productRepository.ListAllAsync()).OrderBy(p => p.Name);
        return _mapper.Map<List<ProductListVm>>(allProducts);
    }
}
