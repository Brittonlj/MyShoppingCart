namespace MyShoppingCart.Application.Data;

public static class OrderByClauses
{

    public static readonly IReadOnlyDictionary<string, IOrderBy> Products =
    new Dictionary<string, IOrderBy>
    {
        { nameof(Product.Name), new OrderBy<Product, string>(x => x.Name) },
        { nameof(Product.Price), new OrderBy<Product, decimal>(x => x.Price) },
    };

    public static readonly IReadOnlyDictionary<string, IOrderBy> Orders =
    new Dictionary<string, IOrderBy>
{
        { nameof(Order.OrderDateTimeUtc), new OrderBy<Order, DateTime>(x => x.OrderDateTimeUtc) }
};

    
}
