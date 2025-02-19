﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public Guid CustomerId { get; set; }
    public Guid BranchId { get; set; }
    public ICollection<SaleItem> SaleItemCollection { get; set; }
}