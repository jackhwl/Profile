using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommand : IRequest<IActionResult>
{
    public ProductForUpdate ProductForUpdate { get; set; }
    public ControllerBase Controller { get; set; }

    public UpdateProductCommand(ProductForUpdate productForUpdate, ControllerBase controller)
    {
        ProductForUpdate = productForUpdate;
        Controller = controller;
    }
}
