namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class SaleItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalAmount { get; set; }
}