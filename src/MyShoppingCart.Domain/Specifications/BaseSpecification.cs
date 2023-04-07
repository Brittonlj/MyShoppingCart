namespace MyShoppingCart.Domain.Specifications;

public abstract class BaseSpecification<TEntity> : Specification<TEntity> where TEntity : class, IEntity<Guid>
{
    public BaseSpecification<TEntity> WithNoTracking()
    {
        Query.AsNoTracking();

        return this;
    }

}
