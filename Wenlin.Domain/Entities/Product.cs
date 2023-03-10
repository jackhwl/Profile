using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Domain.Entities;
public class Product
{
    public int ProductId { get; set; }
    public Guid ProductGuid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
