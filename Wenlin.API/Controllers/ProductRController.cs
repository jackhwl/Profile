using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Products.Commands.CreateProduct;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;
using Wenlin.Application.Features.Products.Queries.GetProductsList;
using Wenlin.Application.Responses;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/categories/{categoryId}/products")]
public class ProductRController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductRController(IMediator mediator)
    {
        _mediator= mediator;
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<ActionResult<ProductDetailVm>> GetProductById(Guid categoryId, Guid id)
    {
        var request = new GetProductDetailQuery() { CategoryId = categoryId, ProductId = id };
        var response = await _mediator.Send(request);

        if (!response.Success && response.NotFound)
        { 
           return NotFound();
        }

        return Ok(response.ProductDetailVm);
    }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<List<ProductListVm>>> GetProducts(Guid categoryId)
    {
        var request = new GetProductsListQuery() { CategoryId = categoryId };
        var response = await _mediator.Send(request);

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            var validationErrors = response.ValidationErrors == null ? "" : string.Join(";", response.ValidationErrors.ToArray());

            throw new ArgumentNullException($"{response.Message};{validationErrors}");
        }

        return Ok(response.ProductListVm);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductDto>> CreateProductForCategory(CreateProductCommand createProductCommand)
    {
        var response = await _mediator.Send(createProductCommand);

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            var validationErrors = response.ValidationErrors == null ? "" : string.Join(";", response.ValidationErrors.ToArray());

            throw new ArgumentNullException($"{response.Message};{validationErrors}");
        }

        return CreatedAtRoute("GetProductById", new { categoryId = response.Product.CategoryId, id=response.Product.Id }, response.Product);
    }

}
