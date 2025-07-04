using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CustomerExternalId { get; set; }
    public Guid BranchExternalId { get; set; }
    public virtual List<SaleItem> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }

    public void Cancel()
    {
        if (this.IsCancelled)
        {
            throw new InvalidOperationException("Sale is already cancelled.");
        }

        this.IsCancelled = true;
        this.Items.ForEach(i => i.Cancel());
    }

    public void RecalculateTotal()
    {
        this.TotalAmount = this.Items?.Sum(i => i.Total) ?? 0m;
    }
    
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
}