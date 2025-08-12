using Ambev.DeveloperEvaluation.Domain.Validation;

using FluentValidation.TestHelper;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

/// <summary>
/// Contains validation tests for the <see cref="SaleItem"/> entity using the domain validation mechanism.
/// </summary>
public class SaleItemValidatorTests
{
    private readonly SaleItemValidator validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleItemValidatorTests"/> class.
    /// </summary>
    public SaleItemValidatorTests()
    {
        this.validator = new SaleItemValidator();
    }

    /// <summary>
    /// Tests that a valid <see cref="SaleItem"/> passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid SaleItem When validated Then result is valid")]
    public void Given_ValidSaleItem_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateValidSaleItem();

        // Act
        var result = item.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that an invalid <see cref="SaleItem"/> fails validation.
    /// </summary>
    [Fact(DisplayName = "Given invalid SaleItem When validated Then result is invalid")]
    public void Given_InvalidSaleItem_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateInvalidSaleItem();

        // Act
        var result = item.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that invalid quantities for <see cref="SaleItem"/> return a validation error.
    /// </summary>
    /// <param name="quantity">The quantity to test.</param>
    [Theory(DisplayName = "Given invalid quantities When validated Then should return validation error")]
    [InlineData(0)]
    [InlineData(-5)]
    [InlineData(21)] // out of range InclusiveBetween(1, 20)
    public void Given_InvalidQuantity_When_Validated_Then_ShouldReturnError(int quantity)
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateValidSaleItem();
        item.Quantity = quantity;

        // Act
        TestValidationResult<SaleItem> result = this.validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }

    /// <summary>
    /// Tests that invalid unit prices for <see cref="SaleItem"/> return a validation error.
    /// </summary>
    /// <param name="unitPrice">The unit price to test.</param>
    [Theory(DisplayName = "Given invalid unit price When validated Then should return validation error")]
    [InlineData(0)]
    [InlineData(-10)]
    public void Given_InvalidUnitPrice_When_Validated_Then_ShouldReturnError(decimal unitPrice)
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateValidSaleItem();
        item.UnitPrice = unitPrice;

        // Act
        TestValidationResult<SaleItem> result = this.validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice);
    }

    /// <summary>
    /// Tests that an empty product ID for <see cref="SaleItem"/> returns a validation error.
    /// </summary>
    [Fact(DisplayName = "Given empty product ID When validated Then should return validation error")]
    public void Given_EmptyProductId_When_Validated_Then_ShouldReturnError()
    {
        // Arrange
        SaleItem item = SaleItemTestData.GenerateValidSaleItem();
        item.ProductExternalId = Guid.Empty;

        // Act
        TestValidationResult<SaleItem> result = this.validator.TestValidate(item);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductExternalId);
    }
}