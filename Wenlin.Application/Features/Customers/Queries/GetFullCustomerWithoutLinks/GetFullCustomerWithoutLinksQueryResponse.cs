using System.Dynamic;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerWithLinks;
public class GetFullCustomerWithoutLinksQueryResponse : BaseResponse
{
    public GetFullCustomerWithoutLinksQueryResponse() : base()
    {

    }
    public ExpandoObject CustomerVm { get; set; } = default!;

}
