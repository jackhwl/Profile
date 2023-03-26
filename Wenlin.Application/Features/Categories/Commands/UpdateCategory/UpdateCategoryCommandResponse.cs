using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Application.Features.Products.Commands.UpdateProduct;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommandResponse : BaseResponse
{
    public UpdateCategoryCommandResponse() : base()
    {

    }
    public bool IsAddCategory { get; set; } = false;
    public CategoryForInsert? Category { get; set; }
}
