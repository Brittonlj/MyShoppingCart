namespace MyShoppingCart.Domain.Specifications;

public sealed class GetProductsByIdListSpec : BaseSpecification<Product>
{
	public GetProductsByIdListSpec(List<Guid> productIds)
	{
		Query
			.Where(x => productIds.Contains(x.Id));
	}
}
