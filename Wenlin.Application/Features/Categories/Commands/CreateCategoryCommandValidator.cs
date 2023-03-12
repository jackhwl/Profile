using FluentValidation;

namespace Wenlin.Application.Features.Categories.Commands;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
    }
}