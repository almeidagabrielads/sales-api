// <copyright file="CreateUserHandlerTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ambev.DeveloperEvaluation.Unit.Application;

using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain;

using AutoMapper;

using FluentAssertions;

using NSubstitute;

using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="CreateUserHandler"/> class.
/// </summary>
public class CreateUserHandlerTests
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IPasswordHasher passwordHasher;
    private readonly CreateUserHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateUserHandlerTests"/> class.
    /// Sets up the test dependencies and creates fake data generators.
    /// </summary>
    public CreateUserHandlerTests()
    {
        this.userRepository = Substitute.For<IUserRepository>();
        this.mapper = Substitute.For<IMapper>();
        this.passwordHasher = Substitute.For<IPasswordHasher>();
        this.handler = new CreateUserHandler(this.userRepository, this.mapper, this.passwordHasher);
    }

    /// <summary>
    /// Tests that a valid user creation request is handled successfully.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact(DisplayName = "Given valid user data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        CreateUserCommand command = CreateUserHandlerTestData.GenerateValidCommand();
        User user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role,
        };

        CreateUserResult result = new CreateUserResult
        {
            Id = user.Id,
        };
        this.mapper.Map<User>(command).Returns(user);
        this.mapper.Map<CreateUserResult>(user).Returns(result);

        this.userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(user);
        this.passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        CreateUserResult createUserResult = await this.handler.Handle(command, CancellationToken.None);

        // Then
        createUserResult.Should().NotBeNull();
        createUserResult.Id.Should().Be(user.Id);
        await this.userRepository.Received(1).CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid user creation request throws a validation exception.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact(DisplayName = "Given invalid user data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        CreateUserCommand command = new CreateUserCommand(); // Empty command will fail validation

        // When
        Func<Task<CreateUserResult>> act = () => this.handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<FluentValidation.ValidationException>();
    }

    /// <summary>
    /// Tests that the password is hashed before saving the user.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact(DisplayName = "Given user creation request When handling Then password is hashed")]
    public async Task Handle_ValidRequest_HashesPassword()
    {
        // Given
        CreateUserCommand command = CreateUserHandlerTestData.GenerateValidCommand();
        string originalPassword = command.Password;
        const string hashedPassword = "h@shedPassw0rd";
        User user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role,
        };

        this.mapper.Map<User>(command).Returns(user);
        this.userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(user);
        this.passwordHasher.HashPassword(originalPassword).Returns(hashedPassword);

        // When
        await this.handler.Handle(command, CancellationToken.None);

        // Then
        this.passwordHasher.Received(1).HashPassword(originalPassword);
        await this.userRepository.Received(1).CreateAsync(
            Arg.Is<User>(u => u.Password == hashedPassword),
            Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that the mapper is called with the correct command.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToUser()
    {
        // Given
        CreateUserCommand command = CreateUserHandlerTestData.GenerateValidCommand();
        User user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password,
            Email = command.Email,
            Phone = command.Phone,
            Status = command.Status,
            Role = command.Role,
        };

        this.mapper.Map<User>(command).Returns(user);
        this.userRepository.CreateAsync(Arg.Any<User>(), Arg.Any<CancellationToken>())
            .Returns(user);
        this.passwordHasher.HashPassword(Arg.Any<string>()).Returns("hashedPassword");

        // When
        await this.handler.Handle(command, CancellationToken.None);

        // Then
        this.mapper.Received(1).Map<User>(Arg.Is<CreateUserCommand>(c =>
            c.Username == command.Username &&
            c.Email == command.Email &&
            c.Phone == command.Phone &&
            c.Status == command.Status &&
            c.Role == command.Role));
    }
}
