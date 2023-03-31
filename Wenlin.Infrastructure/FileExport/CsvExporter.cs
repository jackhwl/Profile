using CsvHelper;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Features.Products.Queries.GetProductsExport;

namespace Wenlin.Infrastructure.FileExport;
public class CsvExporter : ICsvExporter
{
    public byte[] ExportProductsToCsv(List<ProductExportDto> productExportDtos)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter);
            csvWriter.WriteRecords(productExportDtos);
        }

        return memoryStream.ToArray();
    }
}
