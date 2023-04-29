using System.Dynamic;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerWithoutLinks;
public class GetCustomerWithoutLinksQueryResponse : BaseResponse
{
    public GetCustomerWithoutLinksQueryResponse() : base()
    {

    }
    public ExpandoObject CustomerVm { get; set; } = default!;
}
