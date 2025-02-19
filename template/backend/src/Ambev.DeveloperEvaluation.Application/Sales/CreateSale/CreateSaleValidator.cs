using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.SaleItemCollection).NotNull();
        RuleFor(sale => sale.SaleItemCollection).Must(x => x.Count > 0).WithMessage("At least a item is required");
        RuleForEach(sale => sale.SaleItemCollection).ChildRules(child => child.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero"));
        RuleForEach(sale => sale.SaleItemCollection).ChildRules(child => child.RuleFor(x => x.Quantity).LessThanOrEqualTo(20).WithMessage("Maximum limit: 20 items per product"));
    }
}