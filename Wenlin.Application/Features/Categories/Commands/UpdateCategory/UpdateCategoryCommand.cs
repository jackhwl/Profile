using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Application.Features.Products.Commands.UpdateProduct;

namespace Wenlin.Application.Features.Categories.Commands.UpdateCategory;
public class UpdateCategoryCommand : IRequest<UpdateCategoryCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
