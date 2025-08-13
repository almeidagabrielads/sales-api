using Ambev.DeveloperEvaluation.Unit.Domain;

namespace Ambev.DeveloperEvaluation.Unit.Application;

using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using AutoMapper;

using FluentAssertions;

using FluentValidation;

using NSubstitute;

using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="GetSaleHandler"/> class.
/// </summary>
public class GetSaleHandlerTest
{
    private readonly ISaleRepository saleRepository;
    private readonly IMapper mapper;
    private readonly GetSaleHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleHandlerTest"/> class.
    /// Initializes the handler with mocked dependencies.
    /// </summary>
    public GetSaleHandlerTest()
    {
        this.saleRepository = Substitute.For<ISaleRepository>();
        this.mapper = Substitute.For<IMapper>();
        this.handler = new GetSaleHandler(this.saleRepository, this.mapper);
    }

    /// <summary>
    /// Should throw a ValidationException when the command is invalid.
    /// </summary>
    [Fact(DisplayName = "Should throw ValidationException when command is invalid")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var invalidCommand = new GetSaleCommand(Guid.Empty);

        // Act
        Func<Task> act = async () => await this.handler.Handle(invalidCommand, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    /// <summary>
    /// Should throw KeyNotFoundException when sale is not found.
    /// </summary>
    [Fact(DisplayName = "Should throw KeyNotFoundException when sale is not found")]
    public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                      .Returns((Sale?)null);

        // Act
        Func<Task> act = async () => await this.handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
                 .WithMessage($"Sale with ID {command.Id} not found");
    }

    /// <summary>
    /// Should return mapped result when sale is found.
    /// </summary>
    [Fact(DisplayName = "Should return GetSaleResult when sale is found")]
    public async Task Handle_ValidCommand_ReturnsResult()
    {
        // Arrange
        var command = GetSaleHandlerTestData.GenerateValidCommand();
        var existingSale = GetSaleHandlerTestData.GenerateExistingSale(command.Id);
        var expectedResult = GetSaleHandlerTestData.GenerateResult(command.Id);

        this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                      .Returns(existingSale);

        this.mapper.Map<GetSaleResult>(existingSale)
              .Returns(expectedResult);

        // Act
        var result = await this.handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(command.Id);
    }
}