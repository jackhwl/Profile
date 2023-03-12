using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Categories.Queries.GetCategoriesWithProducts;
public class GetCategoriesListWithProductsQuery : IRequest<List<CategoryProductListVm>>
{
    public bool IncludeDisabled { get; set; }
}
