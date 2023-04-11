using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Infrastructure;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Models.Mail;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Products.Commands.CreateProduct;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductCommandResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public CreateProductCommandHandler(ICategoryRepository categoryRepository, IProductRepository productRepository, IMapper mapper, IEmailService emailService)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _emailService= emailService;
    }

    public async Task<CreateProductCommandResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var createProductCommandResponse = new CreateProductCommandResponse();

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        if (category == null)
        {
            createProductCommandResponse.Success = false;
            createProductCommandResponse.NotFound = true;

            return createProductCommandResponse;
        }

        var validator = new CreateProductCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createProductCommandResponse.Success = false;
            createProductCommandResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            foreach (var error in validationResult.Errors)
            {
                if (createProductCommandResponse.ValidationErrors.ContainsKey(error.PropertyName))
                {
                    var errorMsgs = createProductCommandResponse.ValidationErrors[error.PropertyName].ToList();
                    errorMsgs.Add(error.ErrorMessage);
                    createProductCommandResponse.ValidationErrors[error.PropertyName] = errorMsgs;
                }
                else
                    createProductCommandResponse.ValidationErrors.Add(error.PropertyName, new List<string> { error.ErrorMessage });
            }
        }

        if (createProductCommandResponse.Success)
        {
            var product = _mapper.Map<Product>(request);
            product = await _productRepository.AddAsync(product);

            createProductCommandResponse.Product = _mapper.Map<CreateProductDto>(product);

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
        }

        return createProductCommandResponse;
    }
}
