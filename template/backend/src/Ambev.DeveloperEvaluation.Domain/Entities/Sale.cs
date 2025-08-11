using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale, containing details such as sale number, creation date, customer and branch identifiers,
/// associated items, total amount, and cancellation status.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier for the sale.
    /// Must not be an empty Guid.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// Must not be empty and not be in the future.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets the external identifier of the customer associated with the sale.
    /// Must not be an empty Guid.
    /// </summary>
    public Guid CustomerExternalId { get; set; }

    /// <summary>
    /// Gets the external identifier of the branch where the sale occurred.
    /// Must not be an empty Guid.
    /// </summary>
    public Guid BranchExternalId { get; set; }

    /// <summary>
    /// Gets the list of items included in the sale.
    /// Must not be empty and must contain valid sale items.
    /// </summary>
    public virtual List<SaleItem> Items { get; set; } = new();

    /// <summary>
    /// Gets the total amount of the sale, calculated based on the items.
    /// Must be recalculated whenever items are added or modified.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Indicates whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; }

    /// <summary>
    /// Validates the sale using the <see cref="SaleValidator"/>.
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
    /// <list type="bullet">Sale Id information</list>
    /// <list type="bullet">Sale Number format</list>
    /// <list type="bullet">Sale Created date and time validation</list>
    /// <list type="bullet">Customer External Id information</list>
    /// <list type="bullet">Branch External Id information</list>
    /// <list type="bullet">Sale items validation</list>
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        SaleValidator validator = new SaleValidator();
        FluentValidation.Results.ValidationResult result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(e => (ValidationErrorDetail)e),
        };
    }

    /// <summary>
    /// Cancels the sale and all associated items. Throws an exception if the sale is already cancelled.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the sale is already cancelled.</exception>
    public void Cancel()
    {
        if (this.IsCancelled)
        {
            throw new InvalidOperationException("Sale is already cancelled.");
        }

        this.IsCancelled = true;
        this.Items.ForEach(i => i.Cancel());
    }

    /// <summary>
    /// Recalculates the total amount of the sale based on the associated items.
    /// </summary>
    public void RecalculateTotal()
    {
        this.TotalAmount = this.Items?.Sum(i => i.Total) ?? 0m;
    }

    /// <summary>
    /// Updates the details of the sale, including sale number, customer and branch identifiers, 
    /// and associated items. Recalculates discounts and the total amount.
    /// </summary>
    /// 
    /// <param name="newSaleNumber">The new sale number.</param>
    /// <param name="newCustomerId">The new customer identifier.</param>
    /// <param name="newBranchId">The new branch identifier.</param>
    /// <param name="newItems">The new list of items for the sale.</param>
    public void UpdateDetails(
        string newSaleNumber,
        Guid newCustomerId,
        Guid newBranchId,
        List<SaleItem> newItems)
    {
        this.SaleNumber = newSaleNumber;
        this.CustomerExternalId = newCustomerId;
        this.BranchExternalId = newBranchId;

        this.Items.Clear();
        this.Items.AddRange(newItems);
        this.Items.ForEach(i => i.ApplyDiscountRules());
        this.RecalculateTotal();
    }
}