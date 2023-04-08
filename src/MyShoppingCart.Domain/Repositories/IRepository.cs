namespace MyShoppingCart.Domain.Repositories;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity<Guid>
{ 
}