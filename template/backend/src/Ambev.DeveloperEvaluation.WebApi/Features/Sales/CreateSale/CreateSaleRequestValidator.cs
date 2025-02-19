using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.SaleItemCollection).NotNull().WithMessage("At least a item is required");
        RuleFor(sale => sale.SaleItemCollection).Must(x => x.Count > 0).WithMessage("At least a item is required");
        RuleForEach(sale => sale.SaleItemCollection).ChildRules(child => child.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero"));
        RuleForEach(sale => sale.SaleItemCollection).Must((sale, item) => sale.SaleItemCollection.GroupBy(i => i.ProductId)
                                                                                                 .Sum(i => i.Sum(s => s.Quantity)) <= 20)
                                                                                                 .WithMessage("Cannot sell more than 20 identical items.");
    }
}