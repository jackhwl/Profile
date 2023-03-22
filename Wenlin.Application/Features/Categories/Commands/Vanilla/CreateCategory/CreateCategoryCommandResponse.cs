using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Categories.Commands.Vanilla.CreateCategory;

public class CreateCategoryCommandResponse : BaseResponse
{
    public CreateCategoryCommandResponse() : base()
    {

    }

    public CreateCategoryDto Category { get; set; } = default!;
}