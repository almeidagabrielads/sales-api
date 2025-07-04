using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;

using FluentAssertions;

using FluentValidation;

using NSubstitute;

using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class DeleteSaleHandlerTest
{
    private readonly ISaleRepository saleRepository;
    private readonly ISaleItemRepository saleItemRepository;
    private readonly DeleteSaleHandler handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteSaleHandlerTest"/> class.
    /// Initializes the handler with mocked dependencies.
    /// </summary>
    public DeleteSaleHandlerTest()
    {
        this.saleRepository = Substitute.For<ISaleRepository>();
        this.saleItemRepository = Substitute.For<ISaleItemRepository>();
        this.handler = new DeleteSaleHandler(this.saleRepository, this.saleItemRepository);
    }

    /// <summary>
    /// Should throw ValidationException when command is invalid.
    /// </summary>
    [Fact(DisplayName = "Should throw ValidationException when command is invalid")]
    public async Task Handle_InvalidCommand_ThrowsValidationException()
    {
        // Arrange
        var invalidCommand = new DeleteSaleCommand(Guid.Empty); // Missing Id

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
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();
        this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                      .Returns((Sale?)null);

        // Act
        Func<Task> act = async () => await this.handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
                 .WithMessage($"Sale with ID {command.Id} not found");
    }

    /// <summary>
    /// Should throw Exception when deletion of sale items fails.
    /// </summary>
    [Fact(DisplayName = "Should throw Exception when deleting sale items fails")]
    public async Task Handle_DeleteItemsFails_ThrowsException()
    {
        // Arrange
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();
        var existingSale = DeleteSaleHandlerTestData.GenerateExistingSale(command.Id);

        this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
                      .Returns(existingSale);

        this.saleItemRepository.DeleteRangeAsync(existingSale.Items, Arg.Any<CancellationToken>())
                          .Returns(false);

        // Act
        Func<Task> act = async () => await this.handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>()
                 .WithMessage("Failed to delete sale items");
    }

    /// <summary>
    /// Should delete sale and return success response.
    /// </summary>
    [Fact(DisplayName = "Should delete sale and return success")]
    public async Task Handle_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        var command = DeleteSaleHandlerTestData.GenerateValidCommand();
        var existingSale = DeleteSaleHandlerTestData.GenerateExistingSale(command.Id);

        this.saleRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(existingSale);

        this.saleItemRepository.DeleteRangeAsync(existingSale.Items, Arg.Any<CancellationToken>())
            .Returns(true);

        this.saleRepository.DeleteAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(true)); // ✅ fix aqui

        // Act
        var result = await this.handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
    }
}