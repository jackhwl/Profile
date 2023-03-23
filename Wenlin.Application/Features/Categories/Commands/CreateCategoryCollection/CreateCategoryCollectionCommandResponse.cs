using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;

public class CreateCategoryCollectionCommandResponse : BaseResponse
{
    public CreateCategoryCollectionCommandResponse() : base()
    {

    }

    public CreateCategoryDto Category { get; set; } = default!;
}