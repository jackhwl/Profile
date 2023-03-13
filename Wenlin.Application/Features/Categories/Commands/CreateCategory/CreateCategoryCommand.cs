using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}
