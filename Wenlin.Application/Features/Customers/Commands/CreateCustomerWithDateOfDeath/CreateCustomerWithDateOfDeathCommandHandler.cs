using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomerWithDateOfDeath;
public class CreateCustomerWithDateOfDeathCommandHandler : IRequestHandler<CreateCustomerWithDateOfDeathCommand, CreateCustomerWithDateOfDeathCommandResponse>
{
    private readonly IAsyncRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerWithDateOfDeathCommandHandler(IMapper mapper, IAsyncRepository<Customer> customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<CreateCustomerWithDateOfDeathCommandResponse> Handle(CreateCustomerWithDateOfDeathCommand request, CancellationToken cancellationToken)
    {
        var createCustomerWithDateOfDeathCommandResponse = new CreateCustomerWithDateOfDeathCommandResponse();

        var validator = new CreateCustomerWithDateOfDeathCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createCustomerWithDateOfDeathCommandResponse.Success = false;
            createCustomerWithDateOfDeathCommandResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            foreach (var error in validationResult.Errors)
            {
                if (createCustomerWithDateOfDeathCommandResponse.ValidationErrors.ContainsKey(error.PropertyName))
                {
                    var errorMsgs = createCustomerWithDateOfDeathCommandResponse.ValidationErrors[error.PropertyName].ToList();
                    errorMsgs.Add(error.ErrorMessage);
                    createCustomerWithDateOfDeathCommandResponse.ValidationErrors[error.PropertyName] = errorMsgs;
                }
                else
                    createCustomerWithDateOfDeathCommandResponse.ValidationErrors.Add(error.PropertyName, new List<string> { error.ErrorMessage });
            }
        }

        if (createCustomerWithDateOfDeathCommandResponse.Success)
        {
            var customer = _mapper.Map<Customer>(request);
            customer = await _customerRepository.AddAsync(customer);
            var createCustomerDto = _mapper.Map<CreateCustomerWithDateOfDeathDto>(customer);
            createCustomerWithDateOfDeathCommandResponse.CreateCustomerWithDateOfDeathExpandoObject = createCustomerDto.ShapeData(request.Fields);
        }

        return createCustomerWithDateOfDeathCommandResponse;
    }
}
