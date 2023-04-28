using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
public class GetCustomerDetailQueryResponse : BaseResponse
{
    public GetCustomerDetailQueryResponse() : base()
    {

    }
    public object CustomerVm { get; set; } = default!;
}
