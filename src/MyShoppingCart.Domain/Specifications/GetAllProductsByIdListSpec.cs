namespace MyShoppingCart.Domain.Specifications;

public sealed class GetAllProductsByIdListSpec : BaseSpecification<Product>
{
	public GetAllProductsByIdListSpec(List<Guid> productIds)
	{
		Query
			.Where(x => productIds.Contains(x.Id));
	}
}
