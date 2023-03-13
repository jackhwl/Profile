using MediatR;

namespace Wenlin.Application.Features.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
{
    public Guid Id { get; set; }
}
