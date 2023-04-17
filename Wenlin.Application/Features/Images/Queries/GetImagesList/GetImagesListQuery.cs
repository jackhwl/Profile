using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wenlin.Application.Features.Customers.Queries.GetCustomersList;

namespace Wenlin.Application.Features.Images.Queries.GetImagesList;
public class GetImagesListQuery : IRequest<GetCustomersListQueryResponse>
{
}
