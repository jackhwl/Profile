using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categorycollections")]
public class CategoryCollectionsController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoryCollectionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCategoryCollection(List<CreateCategoryCommandDto> categoryCollection)
    {
        var createCategoryCollectionCommand = new CreateCategoryCollectionCommand();
        createCategoryCollectionCommand.CategoryCollection = categoryCollection;
        var response = await _mediator.Send(createCategoryCollectionCommand);

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            throw new ArgumentNullException($"{response.Message};{response.ValidationErrorsString}");
        }

        return Ok();
        //return CreatedAtRoute("GetCategoryById", new { id = response.Category.Id }, response.Category);
    }

}
