using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Categories.Queries.GetCategoriesWithProducts;
public class CategoryProductListVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<CategoryProductDto>? Products { get; set; } 
}
