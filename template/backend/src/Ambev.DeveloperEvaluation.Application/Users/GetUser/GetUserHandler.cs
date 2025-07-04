namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;

using FluentValidation;

using MediatR;

/// <summary>
/// Handler for processing GetUserCommand requests.
/// </summary>
public class GetUserHandler : IRequestHandler<GetUserCommand, GetUserResult>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserHandler"/> class.
    /// Initializes a new instance of GetUserHandler.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="validator">The validator for GetUserCommand.</param>
    public GetUserHandler(
        IUserRepository userRepository,
        IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the GetUserCommand request.
    /// </summary>
    /// <param name="request">The GetUser command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The user details if found.</returns>
    public async Task<GetUserResult> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        GetUserValidator validator = new GetUserValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        Domain.Entities.User? user = await this.userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {request.Id} not found");
        }

        return this.mapper.Map<GetUserResult>(user);
    }
}
