namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryEntityById<TEntity> : BaseSpecification<TEntity>, ISingleResultSpecification
    where TEntity : class, IEntity<Guid>
{
    public QueryEntityById(Guid id)
    {
        Query
            .Where(x => x.Id == id);
    }
}