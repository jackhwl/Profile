using FluentValidation;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
    }
}