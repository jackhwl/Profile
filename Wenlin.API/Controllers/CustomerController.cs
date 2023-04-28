using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using Wenlin.API.Helpers;
using Wenlin.Application.Features.Customers.Commands.CreateCustomer;
using Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;

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

    [HttpGet("{id}", Name = nameof(GetCustomer))]
    public async Task<ActionResult> GetCustomer(Guid id, string? fields, [FromHeader(Name = "Accept")] string? mediaType)
    {
        var mediaTypeValue = MediaTypeHeaderValue.TryParse(mediaType, out var parsedMediaType) ? parsedMediaType.MediaType.ToString() : null;

        var response = await _mediator.Send(new GetCustomerDetailQuery() { Id = id, Fields = fields, MediaType = mediaTypeValue });

        if (!response.Success) return HandleFail(response);

        if (response.HasHateoas)
        {
            // create links
            var links = CreateLinksForCustomer(id, fields);

            var linkedResourceToReturn = response.CustomerExpandoDetailVm as IDictionary<string, object?>;

            linkedResourceToReturn.Add("links", links);
            return Ok(linkedResourceToReturn);
        }

        return Ok(response.CustomerDetailVm);
    }

    [HttpPost(Name = nameof(CreateCustomer))]
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
}
