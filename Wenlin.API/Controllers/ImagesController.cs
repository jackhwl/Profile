using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Images.Queries.GetImagesList;

namespace Wenlin.API.Controllers;
public class ImagesController : BaseController
{
    public ImagesController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ImageListDto>>> GetImages()
    {
        var dtos = await _mediator.Send(new GetImagesListQuery());

        return Ok(dtos);
    }
}
