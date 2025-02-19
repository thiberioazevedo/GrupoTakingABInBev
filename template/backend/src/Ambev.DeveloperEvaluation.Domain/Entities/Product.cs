using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public virtual ICollection<SaleItem> SaleItemCollection { get; set; }

    public override ValidationResult GetValidationResult()
    {
        return new ProductValidator().Validate(this);
    }
}