using Ambev.DeveloperEvaluation.Domain.Validation;

using FluentValidation.TestHelper;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

/// <summary>
/// Contains unit tests for the <see cref="SaleValidator"/> class,
/// validating the business rules for the <see cref="Sale"/> entity.
/// </summary>
public class SaleValidatorTests
{
    private readonly SaleValidator validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="SaleValidatorTests"/> class.
    /// </summary>
    public SaleValidatorTests()
    {
        this.validator = new SaleValidator();
    }

    /// <summary>
    /// Tests that a valid <see cref="Sale"/> passes validation.
    /// </summary>
    [Fact(DisplayName = "Given valid sale When validated Then should return valid")]
    public void Given_ValidSale_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();

        // Act
        ValidationResultDetail result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that invalid sale numbers return a validation error.
    /// </summary>
    /// <param name="saleNumber">The sale number to test.</param>
    [Theory(DisplayName = "Given invalid sale number When validated Then should return validation error")]
    [InlineData("")]
    [InlineData("ThisSaleNumberIsWayTooLongAndExceedsFiftyCharacters1234567890")]
    public void Given_InvalidSaleNumber_When_Validated_Then_ShouldReturnError(string saleNumber)
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.SaleNumber = saleNumber;

        // Act
        TestValidationResult<Sale> result = this.validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SaleNumber);
    }

    /// <summary>
    /// Tests that a future <see cref="Sale.CreatedAt"/> date returns a validation error.
    /// </summary>
    [Fact(DisplayName = "Given future created date When validated Then should return validation error")]
    public void Given_FutureCreatedAt_When_Validated_Then_ShouldReturnError()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.CreatedAt = DateTime.UtcNow.AddMinutes(1);

        // Act
        TestValidationResult<Sale> result = this.validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CreatedAt);
    }

    /// <summary>
    /// Tests that an empty <see cref="Sale.CustomerExternalId"/> returns a validation error.
    /// </summary>
    [Fact(DisplayName = "Given empty customer ID When validated Then should return validation error")]
    public void Given_EmptyCustomerId_When_Validated_Then_ShouldReturnError()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.CustomerExternalId = Guid.Empty;

        // Act
        TestValidationResult<Sale> result = this.validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerExternalId);
    }

    /// <summary>
    /// Tests that an empty <see cref="Sale.BranchExternalId"/> returns a validation error.
    /// </summary>
    [Fact(DisplayName = "Given empty branch ID When validated Then should return validation error")]
    public void Given_EmptyBranchId_When_Validated_Then_ShouldReturnError()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.BranchExternalId = Guid.Empty;

        // Act
        TestValidationResult<Sale> result = this.validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.BranchExternalId);
    }

    /// <summary>
    /// Tests that a <see cref="Sale"/> with no items returns a validation error.
    /// </summary>
    [Fact(DisplayName = "Given no sale items When validated Then should return validation error")]
    public void Given_EmptyItems_When_Validated_Then_ShouldReturnError()
    {
        // Arrange
        Sale sale = SaleTestData.GenerateValidSale();
        sale.Items.Clear();

        // Act
        TestValidationResult<Sale> result = this.validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Items);
    }
}