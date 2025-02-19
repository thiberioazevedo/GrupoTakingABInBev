using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public DateTime Date { get; set; }
    public virtual Guid CustomerId { get; set; }
    public virtual Guid BranchId { get; set; }
    public bool Cancelled { get; set; }
    public decimal Discount { get; internal set; }
    public decimal PercentageDiscount { get; internal set; }
    public decimal GrossTotal { get; internal set; }
    public decimal Total { get; internal set; }

    public ICollection<SaleItem> SaleItemCollection { get; set; }
}
