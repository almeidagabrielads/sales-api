// <copyright file="DeleteUserHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Application.Users.DeleteUser;

using Ambev.DeveloperEvaluation.Domain.Repositories;

using FluentValidation;

using MediatR;

/// <summary>
/// Handler for processing DeleteUserCommand requests.
/// </summary>
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly IUserRepository userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteUserHandler"/> class.
    /// Initializes a new instance of DeleteUserHandler.
    /// </summary>
    /// <param name="userRepository">The user repository.</param>
    /// <param name="validator">The validator for DeleteUserCommand.</param>
    public DeleteUserHandler(
        IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    /// <summary>
    /// Handles the DeleteUserCommand request.
    /// </summary>
    /// <param name="request">The DeleteUser command.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The result of the delete operation.</returns>
    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        DeleteUserValidator validator = new DeleteUserValidator();
        FluentValidation.Results.ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        bool success = await this.userRepository.DeleteAsync(request.Id, cancellationToken);
        if (!success)
        {
            throw new KeyNotFoundException($"User with ID {request.Id} not found");
        }

        return new DeleteUserResponse { Success = true };
    }
}
