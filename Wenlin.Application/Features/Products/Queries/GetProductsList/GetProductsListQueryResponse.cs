using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Products.Queries.GetProductsList;

public class GetProductsListQueryResponse : BaseResponse
{
	public GetProductsListQueryResponse() : base()
	{

	}

    public List<ProductListVm> ProductListVm { get; set; } = default!;
}