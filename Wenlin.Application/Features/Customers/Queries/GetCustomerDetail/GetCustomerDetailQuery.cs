﻿using MediatR;

namespace Wenlin.Application.Features.Customers.Queries.GetCustomerDetail;
public class GetCustomerDetailQuery : IRequest<GetCustomerDetailQueryResponse>
{
    public Guid Id { get; set; }
    public string? Fields { get; set; }
    public string? MediaType { get; set; }
    public bool IncludeLink { get; set; } = false;
}