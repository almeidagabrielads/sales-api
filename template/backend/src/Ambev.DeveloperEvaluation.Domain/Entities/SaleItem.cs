using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale item, containing information about the product, quantity, unit price,
/// discount, total, cancellation status, and the associated sale.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets the external identifier of the product.
    /// Must not be an empty Guid.
    /// </summary>
    public Guid ProductExternalId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets the quantity of the product in the sale item.
    /// Must be between 1 and 20.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the unit price of the product.
    /// Must be greater than zero.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the discount percentage applied to the sale item.
    /// Must be 0.0 (no discount) or 0.10 (for more than 4 equal items) or 0.20 (for more than 10 equal items).
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets the total value of the sale item, calculated based on quantity, unit price, and discount.
    /// </summary>
    public decimal Total { get; private set; }

    /// <summary>
    /// Indicates whether the sale item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;

    /// <summary>
    /// Unique Identifier of the sale associated with the item.
    /// Must not be an empty Guid.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Sale associated with the sale item.
    /// </summary>
    public virtual Sale? Sale { get; set; }

    /// <summary>
    /// Validates the sale item using the <see cref="SaleItemValidator"/> rules.
    /// </summary>
    /// 
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// 
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Product External Id information</list>
    /// <list type="bullet">Quantity respecting the interval</list>
    /// <list type="bullet">Unit price value</list>
    /// <list type="bullet">Sale Id information</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        SaleItemValidator validator = new SaleItemValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }
    
    /// <summary>
    /// Applies discount rules based on the item quantity and recalculates the total.
    /// </summary>
    public void ApplyDiscountRules()
    {
        this.Discount = this.Quantity switch
        {
            >= 10 => 0.20m,
            >= 4 => 0.10m,
            _ => 0.0m,
        };

        this.Total = this.CalculateTotal();
    }

    /// <summary>
    /// Cancels the sale item. Throws an exception if the item is already cancelled.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the item is already cancelled.</exception>
    public void Cancel()
    {
        if (this.IsCancelled)
        {
            throw new InvalidOperationException("Item is already cancelled.");
        }

        this.IsCancelled = true;
    }

    /// <summary>
    /// Calculates the total value of the sales item based on quantity, unit price, and discount.
    /// </summary>
    /// <returns>The total value of the sale item.</returns>
    private decimal CalculateTotal()
    {
        decimal gross = this.Quantity * this.UnitPrice;
        decimal discountAmount = gross * this.Discount;
        return gross - discountAmount;
    }
}