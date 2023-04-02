namespace MyShoppingCart.Application.Orders;

public sealed record OrderModel(
    Guid Id,
    DateTime OrderDateTimeUtc,
    IReadOnlyList<ProductModel> Products)
{
}
