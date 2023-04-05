using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Wenlin.API.Helpers;
using Wenlin.Application.Responses;

namespace Wenlin.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    public readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
    {
        var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

        return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
    }


    public ActionResult HandleFail(BaseResponse response)
    {
        if (response.NotFound) return NotFound();

        if (response.ValidationErrors != null)
        {
            var errors = response.ValidationErrors.ToDictionary(err => err.Key, err => err.Value.ToArray());
            var validationProblemDetails = Utils.GetValidationProblemDetails(errors, HttpContext.Request.Path);

            return new UnprocessableEntityObjectResult(validationProblemDetails)
            {
                ContentTypes = { "application/problem+json" }
            };
        }
        else
        {
            throw new ArgumentException($"{response.Message}");
        }
    }
}
