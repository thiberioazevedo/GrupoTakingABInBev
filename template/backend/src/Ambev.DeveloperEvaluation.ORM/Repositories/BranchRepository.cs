using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BranchRepository : Repository<Branch>, IBranchRepository
{
    public BranchRepository(DefaultContext context) : base(context)
    {
    }
}
