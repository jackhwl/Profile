using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class ProductForInsert
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
