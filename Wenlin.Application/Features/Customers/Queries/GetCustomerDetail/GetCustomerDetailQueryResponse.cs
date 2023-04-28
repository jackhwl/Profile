using System.Dynamic;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
public class GetCustomerDetailQueryResponse : BaseResponse
{
    public GetCustomerDetailQueryResponse() : base()
    {

    }
    public ExpandoObject CustomerExpandoDetailVm { get; set; } = default!;
    public CustomerDetailVm CustomerDetailVm { get; set; } = default!;
    public bool HasHateoas { get; set; } = false;
}
