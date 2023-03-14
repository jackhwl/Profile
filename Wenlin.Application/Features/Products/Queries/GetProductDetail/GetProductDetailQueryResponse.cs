using Wenlin.Application.Features.Categories.Commands.CreateCategory;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Products.Queries.GetProductDetail;

public class GetProductDetailQueryResponse : BaseResponse
{
	public GetProductDetailQueryResponse() : base()
	{

	}
    public ProductDetailVm ProductDetailVm { get; set; } = default!;
    
}