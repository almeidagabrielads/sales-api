using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public Guid BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
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
    
    public void UpdateDetails(
        string newSaleNumber,
        Guid newCustomerId,
        string newCustomerName,
        Guid newBranchId,
        string newBranchName,
        List<SaleItem> newItems)
    {
        this.SaleNumber = newSaleNumber;
        this.CustomerId = newCustomerId;
        this.CustomerName = newCustomerName;
        this.BranchId = newBranchId;
        this.BranchName = newBranchName;

        this.Items.Clear();
        this.Items.AddRange(newItems);
        this.Items.ForEach(i => i.ApplyDiscountRules());
        this.RecalculateTotal();
    }
}