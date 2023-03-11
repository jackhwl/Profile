using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wenlin.API.Models;
using Wenlin.Domain.Entities;
using Wenlin.Domain.Services;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;


    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{id}", Name ="GetCategory")]
    public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
    {
        var category = await _categoryService.GetCategoryAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<CategoryDto>(category));
    }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var categorys = await _categoryService.GetCategoriesAsync();

        return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categorys));
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryForCreationDto category)
    {
        var categoryEntity = _mapper.Map<Category>(category);

        if (categoryEntity == null)
        {
            throw new ArgumentNullException(nameof(categoryEntity));
        }

        await _categoryService.AddCategoryAsync(categoryEntity);
        await _categoryService.SaveAsync();

        var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

        return CreatedAtRoute("GetCategory", new { id = categoryToReturn.Id }, categoryToReturn);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        var category = await _categoryService.GetCategoryAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        _categoryService.DeleteCategory(category);
        await _categoryService.SaveAsync();

        return NoContent();
    }
}
