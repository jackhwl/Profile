using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.API.Helpers;
using Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;
using Wenlin.Application.Features.Categories.Queries.GetCategoryCollection;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categorycollections")]
public class CategoryCollectionsController : BaseController
{
    public CategoryCollectionsController(IMediator mediator) : base(mediator) { }

    [HttpGet("({categoryIds})", Name ="GetCategoryCollection")]
    public async Task<ActionResult<List<CategoryCollectionVm>>> GetCategoryCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute] IEnumerable<Guid> categoryIds)
    {
        var categoryCollectionVm = await _mediator.Send(new GetCategoryCollectionQuery() { CategoryIds = categoryIds});
        return Ok(categoryCollectionVm);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCategoryCollection(List<CreateCategoryCommandDto> categoryCollection)
    {
        var createCategoryCollectionCommand = new CreateCategoryCollectionCommand();
        createCategoryCollectionCommand.CategoryCollection = categoryCollection;
        var response = await _mediator.Send(createCategoryCollectionCommand);

        if (!response.Success) return HandleFail(response);

        var categoryIdsAsString = string.Join(",", response.Categories.Select(c => c.Id));

        return CreatedAtRoute("GetCategoryCollection", new {  categoryIds = categoryIdsAsString }, response.Categories);
    }

}
