using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        var previousPageLink = customers.HasPrevious ? CreateCustomersResourceUri(customersResourceParameters, ResourceUriType.PreviousPage) : null;

        var nextPageLink = customers.HasNext ? CreateCustomersResourceUri(customersResourceParameters, ResourceUriType.NextPage) : null;

        var paginationMetadata = new
        {
            totalCount = customers.TotalCount,
            pageSize = customers.PageSize,
            currentPage = customers.CurrentPage,
            totalPages = customers.TotalPages,
            previousPageLink,
            nextPageLink
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

        return Ok(response.CustomerExpandoListDto);
    }

    [HttpGet("{id}", Name = "GetCustomer")]
    public async Task<ActionResult> GetCustomer(Guid id, string? fields)
    {
        var response = await _mediator.Send(new GetCustomerDetailQuery() { Id = id, Fields = fields });

        if (!response.Success) return HandleFail(response);

        // create links
        var links = CreateLinksForCustomer(id, fields);

        var linkedResourceToReturn = response.CustomerExpandoDetailVm as IDictionary<string, object?>;

        linkedResourceToReturn.Add("links", links);

        return Ok(linkedResourceToReturn);
    }

    [HttpPost]
    public async Task<ActionResult> CreateCustomer(CreateCustomerCommand createCustomerCommand)
    {
        var response = await _mediator.Send(createCustomerCommand);

        if (!response.Success) return HandleFail(response);

        return CreatedAtRoute("GetCustomer", new { id = response.Customer.Id }, response.Customer);
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

    private IEnumerable<LinkDto> CreateLinksForCustomer(Guid customerId, string? fields)
    {
        var links = new List<LinkDto>();

        if (string.IsNullOrWhiteSpace(fields))
        {
            links.Add(new(Url.Link("GetCustomer", new { customerId }), "self", "GET"));
        }
        else
        {
            links.Add(new(Url.Link("GetCustomer", new { customerId, fields }), "self", "GET"));
        }

        //links.Add(new(Url.Link("CreateCourseForCustomer", new { customerId }), "create_course_for_customer", "POST"));
        //links.Add(new(Url.Link("GetCoursesForCustomer", new { customerId }), "courses", "GET"));

        return links;
    }
}
