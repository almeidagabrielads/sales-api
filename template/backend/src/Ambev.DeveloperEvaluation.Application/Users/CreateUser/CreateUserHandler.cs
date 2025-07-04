namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;

using FluentValidation;

using MediatR;

/// <summary>
/// Handler for processing CreateUserCommand requests.
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IPasswordHasher passwordHasher;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    /// <param name="validator">The validator for CreateUserCommand.</param>
    /// <param name="passwordHasher">The password hasher instance.</param>
    public CreateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Handles the CreateUserCommand request.
    /// </summary>
    /// <param name="command">The CreateUser command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created user details.</returns>
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        User? existingUser = await this.userRepository.GetByEmailAsync(command.Email, cancellationToken);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with email {command.Email} already exists");
        }

        User user = this.mapper.Map<User>(command);
        user.Password = this.passwordHasher.HashPassword(command.Password);

        User createdUser = await this.userRepository.CreateAsync(user, cancellationToken);
        CreateUserResult result = this.mapper.Map<CreateUserResult>(createdUser);
        return result;
    }
}
