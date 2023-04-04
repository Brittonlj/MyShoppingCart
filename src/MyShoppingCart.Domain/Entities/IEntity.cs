namespace MyShoppingCart.Domain.Entities;

public interface IEntity<TEntity> : IEquatable<TEntity> where TEntity : class, IEntity<TEntity>
{
    Guid Id { get; init; }
}
