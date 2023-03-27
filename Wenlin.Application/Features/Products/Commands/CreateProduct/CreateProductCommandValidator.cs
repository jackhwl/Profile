using FluentValidation;
using Wenlin.Application.Contracts.Persistence;

namespace Wenlin.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandValidator(IProductRepository productRepository)
	{
		_productRepository = productRepository;

		RuleFor(p => p.Name)
			.NotEmpty().WithMessage("{PropertyName} is required.")
			.NotNull()
			.MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

		RuleFor(p => p)
			.MustAsync(ProductNameAndDescriptionUnique)
			.WithMessage("An Product with the same name and description already exists.");
	}

    private async Task<bool> ProductNameAndDescriptionUnique(CreateProductCommand product, CancellationToken token)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        return !await _productRepository.IsProductNameAndDescriptionUnique(product.Name, product.Description);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
