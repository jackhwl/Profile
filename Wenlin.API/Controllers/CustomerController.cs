using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Wenlin.API.Helpers;
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
            mainCategory = customersResourceParameters.MainCategory,
            searchQuery = customersResourceParameters.SearchQuery
        });
    }
}
