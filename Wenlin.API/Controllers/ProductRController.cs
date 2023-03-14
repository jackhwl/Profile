using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Products.Queries.GetProductDetail;

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
}
