using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandResponse : BaseResponse
{
	public UpdateProductCommandResponse() : base()
	{

	}
    public bool IsAddProduct { get; set; } = false;
    public ProductForInsert? Product { get; set; }
}
