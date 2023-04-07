using Ardalis.Specification.EntityFrameworkCore;
using MyShoppingCart.Domain.Repositories;

namespace MyShoppingCart.Infrastructure.Repositories;

public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> 
    where TEntity : class, IEntity<Guid>
{
    private readonly MyShoppingCartContext _context;
    public Repository(MyShoppingCartContext context) : base(context)
    {
        _context = context;
    }

    public void UpdateEntityProperties<T>(T entity, object request) where T : class, IEntity<Guid>
    {
        _context.Entry(entity).CurrentValues.SetValues(request);
    }
}
