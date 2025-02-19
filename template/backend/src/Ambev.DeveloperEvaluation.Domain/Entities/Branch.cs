using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Domain.Entities;


public class Branch : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Sale> SaleCollection { get; set; }

    public override ValidationResult GetValidationResult()
    {
        return new BranchValidator().Validate(this);
    }
}