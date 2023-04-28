using MyShoppingCart.Application.Authentication;
using MyShoppingCart.Application.Customers;
using MyShoppingCart.Application.Orders;
using MyShoppingCart.Application.Products;
using MyShoppingCart.Domain.Entities;
using MyShoppingCart.Domain.Models;

namespace MyShoppingCart.Shared.Tests.Helpers;

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
            "fred.flintsone",
            "SomePassword",
            address,
            address);
    }
    public static RegisterQuery GetRegisterQuery()
    {
        var address = new AddressModel(
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new RegisterQuery(
            "Fred",
            "Flintstone",
            "fred.flintstone@test.com",
            "fred.flintstone",
            "SomePassword123!",
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
            "fred.flintsone",
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

    public static CreateProductQuery GetCreateProductQuery()
    {
        return new CreateProductQuery(
            "New Product #123",
            "The latest and greatest in 123!",
            1000.00M,
            "http://somedomain.com/image.jpg",
            new HashSet<Category>
            {
                new Category(Guid.NewGuid(), "Test Category")
            });
    }

    public static GetProductsQuery GetGetProductsQuery()
    {
        return new GetProductsQuery(
            "test",
            1,
            20,
            "Name",
            true);
    }

    public static UpdateProductQuery GetUpdateProductQuery()
    {
        return new UpdateProductQuery(
            new Guid("7BC8AE1B-031A-4F3A-815C-2111288FF58C"),
            "New Nike Tennis Shoes",
            "New Nike Tennis Shoes Description",
            100.00M,
            null,
            new HashSet<Category>
            {
                new Category(Guid.NewGuid(), "Athletic")
            });
    }

    public static GetOrdersQuery GetGetOrdersQuery()
    {
        return new GetOrdersQuery(
            DataProvider.DefaultCustomerId,
            1,
            20,
            "OrderDateTimeUtc",
            true);
    }

    public static GetOrderQuery GetGetOrderQuery()
    {
        return new GetOrderQuery(
            DataProvider.DefaultCustomerId,
            DataProvider.DefaultOrderId);
    }

}
