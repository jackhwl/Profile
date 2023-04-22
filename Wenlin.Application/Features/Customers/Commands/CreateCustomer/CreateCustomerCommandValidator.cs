using FluentValidation;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomer;
public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
        RuleFor(p => p.MainCategory)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
        RuleFor(p => p.DateOfBirth)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();
    }
}
