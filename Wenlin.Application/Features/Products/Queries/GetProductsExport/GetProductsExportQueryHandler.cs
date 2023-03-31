using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Products.Queries.GetProductsExport;
public class GetProductsExportQueryHandler : IRequestHandler<GetProductsExportQuery, ProductExportFileVm>
{
    private readonly IProductRepository _productRepository;
    private readonly ICsvExporter _csvExporter;
    private readonly IMapper _mapper;
    public GetProductsExportQueryHandler(IMapper mapper, IProductRepository productRepository, ICsvExporter csvExporter)
    {
        _mapper= mapper;
        _productRepository= productRepository;
        _csvExporter= csvExporter;
    }

    public async Task<ProductExportFileVm> Handle(GetProductsExportQuery request, CancellationToken cancellationToken)
    {
        var allProducts = _mapper.Map<List<ProductExportDto>>((await _productRepository.ListAllAsync()).OrderBy(p => p.Name));

        var fileData = _csvExporter.ExportProductsToCsv(allProducts);

        var productExportFileDto = new ProductExportFileVm() { ContentType = "text/csv", Data= fileData, ProductExportFileName = $"{Guid.NewGuid()}.csv" };

        return productExportFileDto;
    }
}
