namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;
using System;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

/// <summary>
/// Contains unit tests for the SaleItem entity class.
/// Tests cover cancellation behavior, discount rule application, and total calculation.
/// </summary>
public class SaleItemTests
{
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

    [Fact(DisplayName = "Given quantity >= 10 When ApplyDiscountRules Then discount is 20%")]
    public void Given_Quantity10OrMore_When_ApplyDiscountRules_Then_DiscountIs20Percent()
    {
        SaleItem item = SaleItemTestData.GenerateItemWithQuantity(10);
        item.ApplyDiscountRules();
        Assert.Equal(0.20m, item.Discount);
    }

    [Fact(DisplayName = "Given quantity between 4 and 9 When ApplyDiscountRules Then discount is 10%")]
    public void Given_QuantityBetween4And9_When_ApplyDiscountRules_Then_DiscountIs10Percent()
    {
        SaleItem item = SaleItemTestData.GenerateItemWithQuantity(5);
        item.ApplyDiscountRules();
        Assert.Equal(0.10m, item.Discount);
    }

    [Fact(DisplayName = "Given quantity less than 4 When ApplyDiscountRules Then discount is 0%")]
    public void Given_QuantityLessThan4_When_ApplyDiscountRules_Then_DiscountIsZero()
    {
        SaleItem item = SaleItemTestData.GenerateItemWithQuantity(2);
        item.ApplyDiscountRules();
        Assert.Equal(0.00m, item.Discount);
    }
}