using FluentValidation;

namespace Wenlin.Application.Features.Categories.Commands.Vanilla.CreateCategory;

internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
    }
}