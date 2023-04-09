namespace MyShoppingCart.Application.Tests.Helpers;

public static class QueryProvider
{
    public static CreateCustomerQuery GetCreateCustomerQuery()
    {
        var address = new AddressModel(
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new CreateCustomerQuery(
            "Fred",
            "Flintstone",
            "fred.flintstone@test.com",
            address,
            address);
    }

    public static UpdateCustomerQuery GetUpdateCustomerQuery()
    {
        return new UpdateCustomerQuery(
            DataProvider.DefaultCustomerId,
            "Fred",
            "Flintstone",
            "fred.flintstone@test.com",
            new Address
            {
                Id = new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
                Street = "123 Test St",
                City = "Bedrock",
                State = "MO",
                PostalCode = "12345"
            },
            new Address
            {
                Id = new Guid("6B760260-799C-4AF1-A173-0BF83A2A74D5"),
                Street = "123 Test St",
                City = "Bedrock",
                State = "MO",
                PostalCode = "12345"
            });
    }

    public static GetCustomersQuery GetGetCustomersQuery()
    {
        return new GetCustomersQuery(
            "Fred",
            "test.com",
            1,
            20,
            "LastName");

    }

    public static CreateOrderQuery GetCreateOrderQuery()
    {
        return new CreateOrderQuery(
            DataProvider.DefaultCustomerId,
            DataProvider.GetLineItemModels());
    }

    public static UpdateOrderQuery GetUpdateOrderQuery(int itemsCount = 3)
    {
        return new UpdateOrderQuery(
            DataProvider.DefaultCustomerId,
            DataProvider.DefaultOrderId,
            DataProvider.GetLineItems(itemsCount));
    }

}
