namespace MyShoppingCart.Domain.Repositories;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity<Guid>
{    void UpdateEntityProperties<T>(T entity, object request) where T : class, IEntity<Guid>;
}