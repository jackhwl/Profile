using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Features.Categories.Commands.Vanilla.CreateCategory;
using Wenlin.Application.Features.Categories.Commands.Vanilla.DeleteCategory;
using Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoriesList;
using Wenlin.Application.Features.Categories.Queries.Vanilla.GetCategoryDetail;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/cqrscategories")]
public class CategoryCQController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    public CategoryCQController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher= queryDispatcher;
    }
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<List<CategoryListVm>>> GetCategories()
    {
        var dtos = await _queryDispatcher.Dispatch<GetCategoriesListQuery, List<CategoryListVm>>(new GetCategoriesListQuery());
        return Ok(dtos);
    }

    [HttpGet("{id}", Name = "GetCQCategoryById")]
    public async Task<ActionResult<CategoryDetailVm>> GetCategoryById(Guid id)
    {
        var vm = await _queryDispatcher.Dispatch<GetCategoryDetailQuery, CategoryDetailVm>(new GetCategoryDetailQuery() { Id = id });

        if (vm == null)
        {
            return NotFound();
        }

        return Ok(vm);
    }


    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCategory(CreateCategoryCommand createCategoryCommand)
    {
        var response = await _commandDispatcher.Dispatch<CreateCategoryCommand, CreateCategoryCommandResponse>(createCategoryCommand);

        if (!response.Success)
        {
            throw new ArgumentNullException(response.Message);
        }

        return CreatedAtRoute("GetCQCategoryById", new { id = response.Category.Id }, response.Category);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        var response = await _commandDispatcher.Dispatch<DeleteCategoryCommand, DeleteCategoryCommandResponse>(new DeleteCategoryCommand() { Id = id });

        if (!response.Success)
        {
            if (response.NotFound)
                return NotFound();
            else
                throw new ArgumentNullException(response.Message);
        }

        return NoContent();
    }
}
