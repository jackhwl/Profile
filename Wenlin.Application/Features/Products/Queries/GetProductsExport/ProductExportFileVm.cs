using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Products.Queries.GetProductsExport;
public class ProductExportFileVm
{
    public string ProductExportFileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[]? Data { get; set; }
}
