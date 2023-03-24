using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;

public class CreateCategoryCollectionCommandResponse : BaseResponse
{
    public CreateCategoryCollectionCommandResponse() : base()
    {

    }

    public IEnumerable<CreateCategoryDto> Categories { get; set; } = default!;
}