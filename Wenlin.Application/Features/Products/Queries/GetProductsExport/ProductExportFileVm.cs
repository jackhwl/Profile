namespace Wenlin.Application.Features.Products.Queries.GetProductsExport;
public class ProductExportFileVm
{
    public string ProductExportFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] Data { get; set; } = default!;
}
