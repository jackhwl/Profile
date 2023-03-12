using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Models.Mail;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IEmailService emailService)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _emailService= emailService;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        var validator = new CreateProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Count > 0) 
            throw new Exceptions.ValidationException(validationResult);

        product = await _productRepository.AddAsync(product);

        // Sending email notification to addmin address
        var email = new Email() { To = "admin@google.com", Body = $"A new product was created: {request}", Subject="A new product was created" };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch
        {
            //_logger.LogError($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
        }

        return product.Id;
    }
}
