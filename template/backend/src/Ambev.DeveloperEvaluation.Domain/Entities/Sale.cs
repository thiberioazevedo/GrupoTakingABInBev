using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


public class Sale : BaseEntity
{
	public int Number { get; set; }
    public DateTime Date { get; set; }
    public virtual Guid CustomerId { get; set; }
    public virtual Guid BranchId { get; set; }
    public bool Cancelled { get; set; }
    public decimal Discount { get; internal set; }
    public decimal PercentageDiscount { get; internal set; }
    public decimal GrossTotal { get; internal set; }
    public decimal Total { get; internal set; }
    public virtual Branch Branch { get; set; }
    public virtual Customer Customer { get; set; }

    public ICollection<SaleItem> SaleItemCollection { get; set; }

    void SetGrossTotal() {
        GrossTotal = SaleItemCollection.Sum(i => i.Quantity * i.UnitPrice);
    }

    void SetPercentageDiscount()
    {
        switch (SaleItemCollection.GroupBy(i => i.ProductId).OrderByDescending(i => i.Sum(s => s.Quantity)).FirstOrDefault().Sum(i => i.Quantity))
        {
            case int n when (n <= 4):
                PercentageDiscount = 0;
                return;

            case int n when (n < 10):
                PercentageDiscount = 10;
                return;

            default:
                PercentageDiscount = 20;
                return;

        }
    }

    void SetDiscount()
    {
        Discount = Math.Round(GrossTotal * (PercentageDiscount / (decimal)100), 2);
    }

    void SetTotal()
    {
        Total = GrossTotal - Discount;
    }

    public void Calculate()
    {
        SetGrossTotal();
        SetPercentageDiscount();
        SetDiscount();
        SetTotal();
    }

    public override ValidationResult GetValidationResult()
    {
        return new SaleValidator().Validate(this);
    }
}