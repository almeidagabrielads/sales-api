using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

using System;
using System.Linq;

using Ambev.DeveloperEvaluation.Domain.Entities;

using Xunit;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover cancellation, total calculation, and validation scenarios.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that a sale can be cancelled if it's not already cancelled.
    /// </summary>
    [Fact(DisplayName = "Given active sale When cancelled Then status should be set to cancelled and items cancelled")]
    public void Given_ActiveSale_When_Cancelled_Then_SaleAndItemsShouldBeCancelled()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.IsCancelled = false;

        // Act
        sale.Cancel();

        // Assert
        Assert.True(sale.IsCancelled);
        Assert.All(sale.Items, item => Assert.True(item.IsCancelled));
    }

    /// <summary>
    /// Tests that attempting to cancel an already cancelled sale throws an exception.
    /// </summary>
    [Fact(DisplayName = "Given cancelled sale When cancelled again Then throws InvalidOperationException")]
    public void Given_CancelledSale_When_CancelledAgain_Then_ShouldThrow()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.IsCancelled = true;

        // Act
        Action act = () => sale.Cancel();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    /// <summary>
    /// Tests that RecalculateTotal correctly sums up item totals.
    /// </summary>
    [Fact(DisplayName = "Given sale with items When recalculating total Then total should equal sum of item totals")]
    public void Given_SaleWithItems_When_RecalculateTotal_Then_TotalAmountShouldBeCorrect()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();

        decimal expectedTotal = sale.Items.Sum(i => i.Total);

        // Act
        sale.RecalculateTotal();

        // Assert
        Assert.Equal(expectedTotal, sale.TotalAmount);
    }

    /// <summary>
    /// Tests that validation passes for a valid sale.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When validated Then should be valid")]
    public void Given_ValidSale_When_Validated_Then_ShouldBeValid()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();

        // Act
        var result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails for an invalid sale.
    /// </summary>
    [Fact(DisplayName = "Given invalid sale When validated Then should return errors")]
    public void Given_InvalidSale_When_Validated_Then_ShouldReturnErrors()
    {
        // Arrange
        Sale sale = new Sale
        {
            SaleNumber = string.Empty,
            CustomerExternalId = Guid.Empty,
            BranchExternalId = Guid.Empty,
            Items = new(),
            CreatedAt = DateTime.MinValue
        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}