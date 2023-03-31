using Wenlin.Application.Features.Products.Queries.GetProductsExport;

namespace Wenlin.Application.Contracts.Infrastructure;
public interface ICsvExporter
{
    byte[] ExportProductsToCsv(List<ProductExportDto> productExportDtos);
}
