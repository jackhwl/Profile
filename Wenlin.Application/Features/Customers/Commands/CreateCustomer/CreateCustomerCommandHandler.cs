using AutoMapper;
using MediatR;
using Wenlin.Application.Contracts.Persistence;
using Wenlin.Application.Helpers;
using Wenlin.Domain.Entities;

namespace Wenlin.Application.Features.Customers.Commands.CreateCustomer;
public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerCommandResponse>
{
    private readonly IAsyncRepository<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IMapper mapper, IAsyncRepository<Customer> customerRepository)
    {
        _mapper = mapper;
        _customerRepository = customerRepository;
    }

    public async Task<CreateCustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var createCustomerCommandResponse = new CreateCustomerCommandResponse();

        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Count > 0)
        {
            createCustomerCommandResponse.Success = false;
            createCustomerCommandResponse.ValidationErrors = new Dictionary<string, IEnumerable<string>>();
            foreach (var error in validationResult.Errors)
            {
                if (createCustomerCommandResponse.ValidationErrors.ContainsKey(error.PropertyName))
                {
                    var errorMsgs = createCustomerCommandResponse.ValidationErrors[error.PropertyName].ToList();
                    errorMsgs.Add(error.ErrorMessage);
                    createCustomerCommandResponse.ValidationErrors[error.PropertyName] = errorMsgs;
                }
                else
                    createCustomerCommandResponse.ValidationErrors.Add(error.PropertyName, new List<string> { error.ErrorMessage });
            }
        }

        if (createCustomerCommandResponse.Success)
        {
            var customer = _mapper.Map<Customer>(request);
            customer = await _customerRepository.AddAsync(customer);
            var createCustomerDto = _mapper.Map<CreateCustomerDto>(customer);
            createCustomerCommandResponse.CreateCustomerExpandoObject = createCustomerDto.ShapeData(request.Fields);
        }

        return createCustomerCommandResponse;
    }
}
