using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public Sale()
    {
        this.CreatedAt = DateTime.UtcNow;
        this.Items = new();
        this.Customer = new();
        this.Branch = new();
    }

    public string SaleNumber { get; set; } = string.Empty;

    public Customer Customer { get; set; }

    public Branch Branch { get; set; }

    public List<SaleItem> Items { get; set; }

    public bool IsCancelled { get; set; }

    public DateTime CreatedAt { get; set; }

    public decimal TotalAmount { get; set; }

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
}
