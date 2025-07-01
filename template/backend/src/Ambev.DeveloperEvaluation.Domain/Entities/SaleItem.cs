using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public SaleItem()
    {
        this.Product = new();
    }

    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal Total => this.CalculateTotal();

    public bool IsCancelled { get; set; }

    public void ApplyDiscountRules()
    {
        this.Discount = this.Quantity switch
        {
            >= 10 => 0.20m,
            >= 4 => 0.10m,
            _ => 0.0m,
        };
    }

    public void Cancel()
    {
        if (this.IsCancelled)
        {
            throw new InvalidOperationException("Item is already cancelled.");
        }

        this.IsCancelled = true;
    }

    private decimal CalculateTotal()
    {
        decimal gross = this.Quantity * this.UnitPrice;
        decimal discountAmount = gross * this.Discount;
        return gross - discountAmount;
    }
}