using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomersList;
public class CustomersResourceParameters
{
    public string? MainCategory { get; set; }
    public string? SearchQuery { get; set; }
}
