﻿namespace Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
public class CustomerDetailVm
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string MainCategory { get; set; } = string.Empty;
}
