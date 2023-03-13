using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Categories.Commands;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesList;
using Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;
using Wenlin.Domain.Entities;

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
    [HttpHead]
    public async Task<ActionResult<List<CategoryListVm>>> GetCategories()
    {
        var dtos = await _mediator.Send(new GetCategoriesListQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetCategoryById")]
    public async Task<ActionResult<CategoryDetailVm>> GetCategoryById(Guid id)
    {
        var vm = await _mediator.Send(new GetCategoryDetailQuery() { Id = id });

        if (vm == null)
        {
            return NotFound();
        }

        return Ok(vm);
    }


    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCategory(CreateCategoryCommand createCategoryCommand)
    {
        var response = await _mediator.Send(createCategoryCommand);

        if (!response.Success)
        {
            throw new ArgumentNullException(response.Message);
        }

        return CreatedAtRoute("GetCategoryById", new { id = response.Category.Id }, response.Category);
    }
}
