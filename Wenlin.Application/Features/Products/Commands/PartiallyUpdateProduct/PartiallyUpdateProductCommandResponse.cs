using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Application.Responses;

namespace Wenlin.Application.Features.Products.Commands.PartiallyUpdateProduct;
public class PartiallyUpdateProductCommandResponse : BaseResponse
{
	public PartiallyUpdateProductCommandResponse() : base()
	{

	}

    public bool IsAddProduct { get; set; } = false;
	public ProductForUpdateDto? Product { get; set; }
}
