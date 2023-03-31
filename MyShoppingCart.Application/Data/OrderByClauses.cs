namespace MyShoppingCart.Application.Data;

public static class OrderByClauses
{
    public static readonly IReadOnlyDictionary<string, IOrderBy> Customers =
    new Dictionary<string, IOrderBy>
    {
        { nameof(Customer.FirstName), new OrderBy<Customer, string>(x => x.FirstName) },
        { nameof(Customer.LastName), new OrderBy<Customer, string>(x => x.LastName) },
        { nameof(Customer.Email), new OrderBy<Customer, string>(x => x.Email) },
    };

    public static readonly IReadOnlyDictionary<string, IOrderBy> Products =
    new Dictionary<string, IOrderBy>
    {
        { nameof(Product.Name), new OrderBy<Product, string>(x => x.Name) },
        { nameof(Product.Price), new OrderBy<Product, decimal>(x => x.Price) },
    };


}
