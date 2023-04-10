namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryAllProductsByProductIds : BaseSpecification<Product>
{
	public QueryAllProductsByProductIds(List<Guid> productIds)
	{
		Query
			.Where(x => productIds.Contains(x.Id));
	}
}
