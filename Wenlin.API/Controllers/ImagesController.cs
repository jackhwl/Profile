using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Images.Commands.CreateImage;
using Wenlin.Application.Features.Images.Commands.DeleteImage;
using Wenlin.Application.Features.Images.Commands.UpdateImage;
using Wenlin.Application.Features.Images.Queries.GetImageDetail;
using Wenlin.Application.Features.Images.Queries.GetImagesList;

namespace Wenlin.API.Controllers;
[Route("api/images")]
[Authorize]
public class ImagesController : BaseController
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    public ImagesController(IMediator mediator, IWebHostEnvironment hostingEnvironment) : base(mediator) 
    { 
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ImageListDto>>> GetImages()
    {
        var ownerId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

        var response = await _mediator.Send(new GetImagesListQuery() { OwnerId = ownerId });

        return Ok(response.ImageListDto);
    }

    [HttpGet("{id}", Name = "GetImage")]
    [Authorize("MustOwnImage")]
    public async Task<ActionResult<ImageDetailDto>> GetImage(Guid id)
    {
        var response = await _mediator.Send(new GetImageDetailQuery() { Id = id});

        if (!response.Success) return HandleFail(response);

        return Ok(response.ImageDetailDto);
    }

    [HttpPost()]
    //[Authorize(Roles = "PayingUser")]
    [Authorize(Policy = "UserCanAddImage")]
    [Authorize(Policy = "ClientApplicationCanWrite")]
    public async Task<ActionResult<CreateImageDto>> CreateImage([FromBody] CreateImageCommand createImageCommand)
    {
        var inrole = User.IsInRole("PayingUser");
        createImageCommand.OwnerId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "sub")!.Value);
        createImageCommand.WebRootPath = @"C:\Jack\Projects\Profile\Wenlin.API";//_hostingEnvironment.WebRootPath;

        var response = await _mediator.Send(createImageCommand);
        
        if (!response.Success) return HandleFail(response);

        return CreatedAtRoute("GetImage", new { id = response.CreateImageDto.Id }, response.CreateImageDto);
    }

    [HttpDelete("{id}")]
    [Authorize("MustOwnImage")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var response = await _mediator.Send(new DeleteImageCommand() { Id = id });

        if (!response.Success) return HandleFail(response);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize("MustOwnImage")]
    public async Task<IActionResult> UpdateImage(Guid id, UpdateImageCommand updateImageCommand)
    {
        updateImageCommand.Id = id;
        var response = await _mediator.Send(updateImageCommand);

        if (!response.Success) return HandleFail(response);

        return NoContent();
    }
}
