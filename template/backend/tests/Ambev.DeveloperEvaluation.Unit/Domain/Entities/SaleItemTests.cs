namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;
using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="SaleItem"/> entity class.
/// Tests cover cancellation behavior, discount rule application, and total calculation.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests that an active item can be cancelled, setting <see cref="SaleItem.IsCancelled"/> to true.
    /// </summary>
    [Fact(DisplayName = "Given active item When cancelled Then IsCancelled should be true")]
    public void Given_ActiveItem_When_Cancelled_Then_IsCancelledIsTrue()
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateValidSaleItem();
        item.IsCancelled = false;

        // Act
        item.Cancel();

        // Assert
        Assert.True(item.IsCancelled);
    }

    /// <summary>
    /// Tests that cancelling an already cancelled item throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    [Fact(DisplayName = "Given cancelled item When cancelled again Then throws InvalidOperationException")]
    public void Given_CancelledItem_When_CancelledAgain_Then_Throws()
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateValidSaleItem();
        item.IsCancelled = true;

        // Act
        Action act = () => item.Cancel();

        // Assert
        Assert.Throws<InvalidOperationException>(act);
    }

    /// <summary>
    /// Tests that applying discount rules to an item with quantity 10 or more sets a 20% discount.
    /// </summary>
    [Fact(DisplayName = "Given quantity >= 10 When ApplyDiscountRules Then discount is 20%")]
    public void Given_Quantity10OrMore_When_ApplyDiscountRules_Then_DiscountIs20Percent()
    {
        SaleItem item = SaleItemTestData.GenerateItemWithQuantity(10);
        item.ApplyDiscountRules();
        Assert.Equal(0.20m, item.Discount);
    }

    /// <summary>
    /// Tests that applying discount rules to an item with quantity between 4 and 9 sets a 10% discount.
    /// </summary>
    [Fact(DisplayName = "Given quantity between 4 and 9 When ApplyDiscountRules Then discount is 10%")]
    public void Given_QuantityBetween4And9_When_ApplyDiscountRules_Then_DiscountIs10Percent()
    {
        SaleItem item = SaleItemTestData.GenerateItemWithQuantity(5);
        item.ApplyDiscountRules();
        Assert.Equal(0.10m, item.Discount);
    }

    /// <summary>
    /// Tests that applying discount rules to an item with quantity less than 4 sets a 0% discount.
    /// </summary>
    [Fact(DisplayName = "Given quantity less than 4 When ApplyDiscountRules Then discount is 0%")]
    public void Given_QuantityLessThan4_When_ApplyDiscountRules_Then_DiscountIsZero()
    {
        SaleItem item = SaleItemTestData.GenerateItemWithQuantity(2);
        item.ApplyDiscountRules();
        Assert.Equal(0.00m, item.Discount);
    }
}