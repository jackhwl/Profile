﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Images.Queries.GetImageDetail;
using Wenlin.Application.Features.Images.Queries.GetImagesList;

namespace Wenlin.API.Controllers;
[Route("api/images")]
public class ImagesController : BaseController
{
    public ImagesController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<ImageListDto>>> GetImages()
    {
        var response = await _mediator.Send(new GetImagesListQuery());

        return Ok(response.ImageListDto);
    }

    [HttpGet("{id}", Name = "GetImage")]
    public async Task<ActionResult<ImageDetailDto>> GetImage(Guid id)
    {
        var response = await _mediator.Send(new GetImageDetailQuery() { Id = id});

        if (!response.Success) return HandleFail(response);

        return Ok(response.ImageDetailDto);
    }
}
