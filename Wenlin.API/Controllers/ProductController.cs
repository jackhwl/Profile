using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wenlin.API.Models;
using Wenlin.Domain.Entities;
using Wenlin.Domain.Services;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categories/{categoryId}/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;


    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{productId}", Name ="GetProduct")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid categoryId, Guid productId)
    {
        if (!await _productService.CategoryExistsAsync(categoryId))
        {
            return NotFound();
        }

        var product = await _productService.GetProductAsync(categoryId, productId);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(Guid categoryId)
    {
        if (!await _productService.CategoryExistsAsync(categoryId))
        {
            return NotFound();
        }

        var products = await _productService.GetProductsAsync(categoryId);

        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProductForCategory(Guid categoryId, ProductForCreationDto product)
    {
        if (!await _productService.CategoryExistsAsync(categoryId))
        {
            return NotFound();
        }

        var productEntity = _mapper.Map<Product>(product);
        await _productService.AddProductAsync(categoryId, productEntity);
        await _productService.SaveAsync();

        var productToReturn = _mapper.Map<ProductDto>(productEntity);

        return CreatedAtRoute("GetProduct", new { categoryId, productId = productToReturn.Id }, productToReturn);
    }

    [HttpDelete("{productId}")]
    public async Task<ActionResult> DeleteProduct(Guid categoryId, Guid productId)
    {
        if (!await _productService.CategoryExistsAsync(categoryId))
        {
            return NotFound();
        }

        var product = await _productService.GetProductAsync(categoryId, productId);

        if (product == null)
        {
            return NotFound();
        }

        _productService.DeleteProduct(product);
        await _productService.SaveAsync();

        return NoContent();
    }
}
