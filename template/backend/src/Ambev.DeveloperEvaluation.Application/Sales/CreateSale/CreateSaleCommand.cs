using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
	public Guid CustomerId { get; }
    public Guid BranchId { get; }
    public ICollection<SaleItem> SaleItemCollection { get; }

    public CreateSaleCommand(Guid customerId, Guid branchId, ICollection<SaleItem> saleItemCollection)
    {
        CustomerId = customerId;
        SaleItemCollection = saleItemCollection;
        BranchId = branchId;
    }


    public ValidationResultDetail Validate()
    {
        var validator = new CreateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}