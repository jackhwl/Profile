using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Products.Queries.GetProductsExport;
public class GetProductsExportQuery : IRequest<ProductExportFileVm>
{
}
