using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Wenlin.API.Helpers;
using Wenlin.Application.Features.Categories.Commands.CreateCategory;
using Wenlin.Application.Features.Categories.Commands.DeleteCategory;
using Wenlin.Application.Features.Categories.Commands.UpdateCategory;
using Wenlin.Application.Features.Categories.Queries.GetCategoriesList;
using Wenlin.Application.Features.Categories.Queries.GetCategoryDetail;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoryController(IMediator mediator)
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
            if (response.NotFound) return NotFound();

            if (response.ValidationErrors != null)
            {
                var validationProblemDetails = Utils.GetValidationProblemDetails(response.ValidationErrors.ToDictionary(err => err.Key, err => err.Value.ToArray()), HttpContext.Request.Path);
                return new UnprocessableEntityObjectResult(validationProblemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            }
        }

        return CreatedAtRoute("GetCategoryById", new { id = response.Category.Id }, response.Category);
    }



    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        var response = await _mediator.Send(new DeleteCategoryCommand() { Id = id });

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            throw new ArgumentException($"{response.Message};{response.ValidationErrorsString}");
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, UpdateCategoryCommand updateCategoryCommand)
    {
        updateCategoryCommand.Id = id;
        var response = await _mediator.Send(updateCategoryCommand);

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            throw new ArgumentException($"{response.Message};{response.ValidationErrorsString}");
        }

        // insert if id not found 
        if (response.IsAddCategory)
        {
            return CreatedAtRoute("GetCategoryById", new { id }, response.Category);
        }

        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetCategoriesOptions()
    {
        Response.Headers.Add("Allow", "GET,HEAD,POST,DELETE,OPTIONS");
        return Ok();
    }
}
