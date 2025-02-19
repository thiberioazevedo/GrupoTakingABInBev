using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : Repository<Sale>, ISaleRepository
{
    public SaleRepository(DefaultContext context) : base(context)
    {
    }
    public int GetLastNumber()
    {
        var model = dbSet.OrderByDescending(s => s.Number).FirstOrDefault();
        return model?.Number ?? 0;
    }
    public override Task<Sale> CreateAsync(Sale entity, CancellationToken cancellationToken = default)
    {
        entity.Number = GetLastNumber() + 1;
        entity.Calculate();
        return base.CreateAsync(entity, cancellationToken);
    }
}
