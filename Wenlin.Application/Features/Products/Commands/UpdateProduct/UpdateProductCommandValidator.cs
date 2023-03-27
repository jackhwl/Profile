using FluentValidation;

namespace Wenlin.Application.Features.Products.Commands.UpdateProduct;
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {

        RuleFor(p => p.ProductForUpdate.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

        RuleFor(p => p.ProductForUpdate.Description)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull()
           .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");
    }
}
