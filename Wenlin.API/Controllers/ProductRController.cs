using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Wenlin.Application.Features.Products.Commands.CreateProduct;
using Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
using Wenlin.Application.Features.Products.Commands.UpdateProduct;
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

            throw new ArgumentException($"{response.Message};{response.ValidationErrorsString}");
        }

        return Ok(response.ProductListVm);
    }

    [HttpPost]
    public async Task<ActionResult<CreateProductDto>> CreateProductForCategory(Guid categoryId, CreateProductCommand createProductCommand)
    {
        createProductCommand.CategoryId = categoryId;
        var response = await _mediator.Send(createProductCommand);

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            throw new ArgumentException($"{response.Message};{response.ValidationErrorsString}");
        }

        return CreatedAtRoute("GetProductById", new { categoryId = response.Product.CategoryId, id=response.Product.Id }, response.Product);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductForCategory(Guid categoryId, Guid productId, ProductForUpdate productForUpdate)
    {
        productForUpdate.Id = productId;
        productForUpdate.CategoryId = categoryId;
        var updateProductCommand = new UpdateProductCommand(productForUpdate, this);

        return await _mediator.Send(updateProductCommand);
    }

    [HttpPatch("{productId}")]
    public async Task<IActionResult> PartiallyUpdateProductForCategory(Guid categoryId, Guid productId, JsonPatchDocument<ProductForUpdateDto> patchDocument)
    {
        var partiallyUpdateProductCommand = new PartiallyUpdateProductCommand() 
            { 
                CategoryId = categoryId, 
                Id = productId, 
                PatchDocument = patchDocument 
            };
        var response = await _mediator.Send(partiallyUpdateProductCommand);

        if (!response.Success)
        {
            if (response.NotFound) return NotFound();

            throw new ArgumentException($"{response.Message};{response.ValidationErrorsString}");
        }

        if (response.IsAddProduct)
        {
            return CreatedAtRoute("GetProductById", new { categoryId, id = productId }, response.Product);
        }

        return NoContent();
    }

    public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
    {
        var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

        return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
    }
}
