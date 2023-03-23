using FluentValidation;

namespace Wenlin.Application.Features.Categories.Commands.CreateCategoryCollection;

internal class CreateCategoryCollectionCommandValidator : AbstractValidator<CreateCategoryCollectionCommand>
{
    public CreateCategoryCollectionCommandValidator()
    {
    }
}