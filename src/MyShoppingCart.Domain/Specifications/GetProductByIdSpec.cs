namespace MyShoppingCart.Domain.Specifications;

public sealed class GetProductByIdSpec : BaseSpecification<Product>, ISingleResultSpecification
{
	public GetProductByIdSpec(Guid productId)
	{
		Query
			.Where(x => x.Id == productId)
			.Include(x => x.Categories)
			.AsSplitQuery();
	}
}
