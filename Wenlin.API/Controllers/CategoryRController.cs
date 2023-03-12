using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesList;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryRController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoryRController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryListVm>>> GetCategories()
    {
        var dtos = await _mediator.Send(new GetCategoriesListQuery());
        return Ok(dtos);
    }
}
