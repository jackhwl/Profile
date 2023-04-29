using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Wenlin.API.ActionConstraints;
using Wenlin.API.Helpers;
using Wenlin.Application.Features.Customers.Commands.CreateCustomer;
using Wenlin.Application.Features.Customers.Commands.CreateCustomerWithDateOfDeath;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;
using Wenlin.Application.Features.Customers.Queries.GetCustomerWithLinks;
using Wenlin.Application.Features.Customers.Queries.GetCustomerWithoutLinks;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : BaseController
{
    public CustomerController(IMediator mediator) : base(mediator) { }

    [HttpGet(Name = nameof(GetCustomers))]
    [HttpHead]
    public async Task<IActionResult> GetCustomers([FromQuery]CustomersResourceParameters customersResourceParameters)
    {
        var request = new GetCustomersListQuery() { CustomersResourceParameters = customersResourceParameters };
        var response = await _mediator.Send(request);

        if (!response.Success) return HandleFail(response);

        var customers = response.CustomerListDto;

        var paginationMetadata = new
        {
            totalCount = customers.TotalCount,
            pageSize = customers.PageSize,
            currentPage = customers.CurrentPage,
            totalPages = customers.TotalPages
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        // create links
        var links = CreateLinksForCustomers(customersResourceParameters, customers.HasNext, customers.HasPrevious);
        var shapedCustomersWithLinks = response.CustomerExpandoListDto.Select(customer =>
        {
            var customerAsDictionary = customer as IDictionary<string, object?>;
            var customerLinks = CreateLinksForCustomer((Guid)customerAsDictionary["Id"]!, null);
            customerAsDictionary.Add("links", customerLinks);
            return customerAsDictionary;
        });

        var linkedCollectionResource = new
        {
            value = shapedCustomersWithLinks,
            links
        };

        return Ok(linkedCollectionResource);
    }

    [RequestHeaderMatchesMediaType("Accept", "application/json", "application/vnd.wenlin.customer.friendly+json")]
    [Produces("application/json", "application/vnd.wenlin.customer.friendly+json")]
    [HttpGet("{id}", Name = "GetCustomer")]
    public async Task<ActionResult> GetCustomerWithoutLinks(Guid id, string? fields)
    {
        var response = await _mediator.Send(new GetCustomerWithoutLinksQuery() { Id = id, Fields = fields });

        if (!response.Success) return HandleFail(response);

        return Ok(response.CustomerVm);
    }

    [RequestHeaderMatchesMediaType("Accept", "application/vnd.wenlin.hateoas+json", "application/vnd.wenlin.customer.friendly.hateoas+json")]
    [Produces("application/vnd.wenlin.hateoas+json", "application/vnd.wenlin.customer.friendly.hateoas+json")]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCustomerWithLinks(Guid id, string? fields)
    {
        var response = await _mediator.Send(new GetCustomerWithoutLinksQuery() { Id = id, Fields = fields });

        if (!response.Success) return HandleFail(response);

        var links = CreateLinksForCustomer(id, fields);

        var linkedResourceToReturn = response.CustomerVm as IDictionary<string, object?>;

        linkedResourceToReturn!.Add("links", links);

        return Ok(linkedResourceToReturn);
    }

    [RequestHeaderMatchesMediaType("Accept", "application/vnd.wenlin.customer.full+json")]
    [Produces("application/vnd.wenlin.customer.full+json")]
    [HttpGet("{id}", Name = "GetCustomer")]
    public async Task<ActionResult> GetFullCustomerWithoutLinks(Guid id, string? fields)
    {
        var response = await _mediator.Send(new GetFullCustomerWithoutLinksQuery() { Id = id, Fields = fields });

        if (!response.Success) return HandleFail(response);

        return Ok(response.CustomerVm);
    }

    [RequestHeaderMatchesMediaType("Accept", "application/vnd.wenlin.customer.full.hateoas+json")]
    [Produces("application/vnd.wenlin.customer.full.hateoas+json")]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetFullCustomerWithLinks(Guid id, string? fields)
    {
        var response = await _mediator.Send(new GetFullCustomerWithoutLinksQuery() { Id = id, Fields = fields });

        if (!response.Success) return HandleFail(response);

        var links = CreateLinksForCustomer(id, fields);

        var linkedResourceToReturn = response.CustomerVm as IDictionary<string, object?>;

        linkedResourceToReturn!.Add("links", links);

        return Ok(linkedResourceToReturn);
    }

    //[Produces("application/json", "application/vnd.wenlin.hateoas+json"
    //   , "application/vnd.wenlin.hateoas+json"
    //   , "application/vnd.wenlin.customer.full+json"
    //   , "application/vnd.wenlin.customer.full.hateoas+json"
    //   , "application/vnd.wenlin.customer.friendly+json"
    //   , "application/vnd.wenlin.customer.friendly.hateoas+json"
    //   )]
    //[HttpGet("{id}", Name = nameof(GetCustomer))]
    //public async Task<ActionResult> GetCustomer(Guid id, string? fields, [FromHeader(Name = "Accept")] string? mediaType)
    //{
    //    string? mediaTypeValue = null;
    //    var includeLink = false;
    //    if (MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType))
    //    {
    //        includeLink = parsedMediaType!.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    //        mediaTypeValue = includeLink ? parsedMediaType.SubTypeWithoutSuffix.Substring(0, parsedMediaType.SubTypeWithoutSuffix.Length - 8) : parsedMediaType.SubTypeWithoutSuffix.Value;
    //    }

    //    var response = await _mediator.Send(new GetCustomerDetailQuery() { Id = id, Fields = fields, MediaType = mediaTypeValue, IncludeLink = includeLink });

    //    if (!response.Success) return HandleFail(response);

    //    if (includeLink)
    //    {
    //        // create links
    //        var links = CreateLinksForCustomer(id, fields);

    //        var linkedResourceToReturn = response.CustomerVm as IDictionary<string, object?>;

    //        linkedResourceToReturn!.Add("links", links);
    //        return Ok(linkedResourceToReturn);
    //    }

    //    return Ok(response.CustomerVm);
    //}


    [HttpPost(Name = nameof(CreateCustomerWithDateOfDeath))]
    [RequestHeaderMatchesMediaType("Content-Type", "application/vnd.wenlin.customerforcreationwithdateofdeath+json")]
    [Consumes("application/vnd.wenlin.customerforcreationwithdateofdeath+json")]
    public async Task<ActionResult> CreateCustomerWithDateOfDeath(CreateCustomerWithDateOfDeathCommand createCustomerCommand)
    {
        var response = await _mediator.Send(createCustomerCommand);

        if (!response.Success) return HandleFail(response);

        var linkedResourceToReturn = response.CreateCustomerWithDateOfDeathExpandoObject as IDictionary<string, object?>;
        var links = CreateLinksForCustomer((Guid)linkedResourceToReturn["Id"]!, null);
        linkedResourceToReturn.Add("links", links);

        return CreatedAtRoute("GetCustomer", new { id = linkedResourceToReturn["Id"] }, linkedResourceToReturn);
    }

    [HttpPost(Name = nameof(CreateCustomer))]
    [RequestHeaderMatchesMediaType("Content-Type", "application/json", "application/vnd.wenlin.customerforcreation+json")]
    [Consumes("application/json", "application/vnd.wenlin.customerforcreation+json")]
    public async Task<ActionResult> CreateCustomer(CreateCustomerCommand createCustomerCommand)
    {
        var response = await _mediator.Send(createCustomerCommand);

        if (!response.Success) return HandleFail(response);

        var linkedResourceToReturn = response.CreateCustomerExpandoObject as IDictionary<string, object?>;
        var links = CreateLinksForCustomer((Guid)linkedResourceToReturn["Id"]!, null);
        linkedResourceToReturn.Add("links", links);

        return CreatedAtRoute("GetCustomer", new { id = linkedResourceToReturn["Id"] }, linkedResourceToReturn);
    }

    private string? CreateCustomersResourceUri(CustomersResourceParameters customersResourceParameters, ResourceUriType type)
    {
        var pageNumber = customersResourceParameters.PageNumber;
        switch (type)
        {
            case ResourceUriType.PreviousPage:
                pageNumber--;
                break;
            case ResourceUriType.NextPage:
                pageNumber++;
                break;
            case ResourceUriType.Current:
            default:
                break;
        }

        return Url.Link(nameof(GetCustomers), new
        {
            fields = customersResourceParameters.Fields,
            orderBy = customersResourceParameters.OrderBy,
            pageNumber,
            pageSize = customersResourceParameters.PageSize,
            mainCustomer = customersResourceParameters.MainCategory,
            searchQuery = customersResourceParameters.SearchQuery
        });
    }

    private IEnumerable<LinkDto> CreateLinksForCustomers(CustomersResourceParameters customersResourceParameters, bool hasNext, bool hasPrevious)
    {
        var links = new List<LinkDto>();

        links.Add(new(CreateCustomersResourceUri(customersResourceParameters, ResourceUriType.Current), "self", "GET"));

        if (hasNext)
            links.Add(new(CreateCustomersResourceUri(customersResourceParameters, ResourceUriType.NextPage), "nextPage", "GET"));

        if (hasPrevious)
            links.Add(new(CreateCustomersResourceUri(customersResourceParameters, ResourceUriType.PreviousPage), "previousPage", "GET"));

        return links;
    }

    private IEnumerable<LinkDto> CreateLinksForCustomer(Guid id, string? fields)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(new(Url.Link("GetCustomer", new { id }), "self", "GET"));
        }
        else
        {
            links.Add(new(Url.Link("GetCustomer", new { id, fields }), "self", "GET"));
        }

        //links.Add(new(Url.Link("CreateCourseForCustomer", new { id }), "create_course_for_customer", "POST"));
        //links.Add(new(Url.Link("GetCoursesForCustomer", new { id }), "courses", "GET"));

        return links;
    }

    [HttpOptions]
    public IActionResult GetCustomerOptions()
    {
        Response.Headers.Add("Allow", "GET,HEAD,POST,OPTIONS");
        return Ok();
    }
}
