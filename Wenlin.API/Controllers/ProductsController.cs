using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wenlin.API.Models;
using Wenlin.Domain.Entities;
using Wenlin.Domain.Services;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;


    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("{productGuid}", Name ="GetProduct")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid productGuid)
    {
        var product = await _productService.GetProductAsync(productGuid);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<ProductDto>(product));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetProductsAsync();

        return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductForCreationDto product)
    {
        var productEntity = _mapper.Map<Product>(product);

        if (productEntity == null)
        {
            throw new ArgumentNullException(nameof(productEntity));
        }

        await _productService.AddProductAsync(productEntity);
        await _productService.SaveAsync();

        var productToReturn = _mapper.Map<ProductDto>(productEntity);

        return CreatedAtRoute("GetProduct", new { productGuid = productToReturn.ProductGuid }, productToReturn);
    }

    [HttpDelete("{productGuid}")]
    public async Task<ActionResult> DeleteProduct(Guid productGuid)
    {
        var product = await _productService.GetProductAsync(productGuid);

        if (product == null)
        {
            return NotFound();
        }

        _productService.DeleteProduct(product);
        await _productService.SaveAsync();

        return NoContent();
    }
}
