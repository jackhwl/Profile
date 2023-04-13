using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;

namespace Wenlin.API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : BaseController
{
    public CustomerController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<IEnumerable<CustomerListDto>>> GetCustomers([FromQuery]CustomersResourceParameters customersResourceParameters)
    {
        var request = new GetCustomersListQuery() { CustomersResourceParameters = customersResourceParameters };
        var response = await _mediator.Send(request);

        return Ok(response.CustomerListDto);
    }
}
