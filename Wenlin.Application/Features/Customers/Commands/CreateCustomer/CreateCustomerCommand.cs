﻿using MediatR;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomer;
public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTimeOffset DateOfBirth { get; set; }
    public string MainCategory { get; set; } = string.Empty;
    public string? Fields { get; set; }
}
