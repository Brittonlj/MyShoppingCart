namespace MyShoppingCart.Application.Orders;

public static class OrderMapper
{
    public static Order ToShallowEntity(this CreateOrderQuery other, Customer customer)
    {
        return new Order
        {
            CustomerId = other.CustomerId,
            Customer = customer,
        };
    }

    public static OrderModel ToModel(this Order other) 
    {
        var products = other.Products.ToModels();
        return new OrderModel(
            other.Id,
            other.OrderDateTimeUtc,
            products);
    }

    public static List<OrderModel> ToModels(this IEnumerable<Order> others)
    {
        return others.Select(x => x.ToModel()).ToList();
    }
}
