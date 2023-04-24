using Microsoft.AspNetCore.Mvc;
using Wenlin.API.Helpers;

namespace Wenlin.API.Controllers;
[Route("api")]
[ApiController]
public class RootController : ControllerBase
{
    [HttpGet(Name = nameof(GetRoot))]
    public IActionResult GetRoot()
    {
        var links = new List<LinkDto>();

        links.Add(new(Url.Link("GetRoot", new { }), "self", "GET"));
        links.Add(new(Url.Link("GetCustomers", new { }), "customers", "GET"));
        links.Add(new(Url.Link("CreateCustomer", new { }), "create_customer", "POST"));

        return Ok(links);
    }
}
