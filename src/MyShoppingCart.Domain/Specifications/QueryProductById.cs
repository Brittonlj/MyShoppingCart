using MyShoppingCart.Domain.Entities;

namespace MyShoppingCart.Domain.Specifications;

public sealed class QueryProductById : BaseSpecification<Product>, ISingleResultSpecification
{
	public QueryProductById(Guid productId)
	{
		Query
			.Where(x => x.Id == productId);
	}
}
