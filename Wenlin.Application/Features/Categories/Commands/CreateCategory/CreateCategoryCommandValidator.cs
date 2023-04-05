using FluentValidation;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MinimumLength(150).WithMessage("{PropertyName} must exceed 150 characters.")
            .MaximumLength(80).WithMessage("{PropertyName} must not exceed 80 characters.");
    }
}