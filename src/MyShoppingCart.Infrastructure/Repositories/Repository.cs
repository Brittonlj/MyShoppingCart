using Ardalis.Specification.EntityFrameworkCore;
using MyShoppingCart.Domain.Repositories;
using MyShoppingCart.Domain.Specifications;

namespace MyShoppingCart.Infrastructure.Repositories;

public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    private readonly MyShoppingCartContext _context;
    public Repository(MyShoppingCartContext context) : base(context)
    {
        _context = context;
    }

    public async Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new QueryEntityById<TEntity>(id);
        return await FirstOrDefaultAsync(query, cancellationToken);
    }

    public async Task<TEntity?> FindAsyncWithNoTracking(Guid id, CancellationToken cancellationToken = default)
    {
        var query = new QueryEntityById<TEntity>(id).WithNoTracking();
        return await FirstOrDefaultAsync(query, cancellationToken);
    }
}
