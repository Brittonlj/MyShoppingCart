namespace MyShoppingCart.Domain.Repositories;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity<Guid>
{
    Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken);

    Task<TEntity?> FindAsyncWithNoTracking(Guid id, CancellationToken cancellationToken);
}