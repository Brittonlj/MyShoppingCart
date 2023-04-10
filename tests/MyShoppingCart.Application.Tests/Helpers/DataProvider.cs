﻿using MyShoppingCart.Application.Configuration;
using System.Security.Claims;

namespace MyShoppingCart.Application.Tests.Helpers;

public static class DataProvider
{
    public static readonly Guid DefaultCustomerId = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5");
    public static readonly Guid DefaultAddressId = new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5");
    public static readonly Guid DefaultOrderId = new Guid("29D74756-8F33-4AE9-B534-F596252EB97B");

    public static Customer GetCustomer()
    {
        var address = new Address(
            DefaultAddressId,
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new Customer
        {
            Id = DefaultCustomerId,
            FirstName = "Fred",
            LastName = "Flintstone",
            Email = "fred.flintstone@test.com",
            ShippingAddress = address,
            ShippingAddressId = address.Id,
            BillingAddress = address,
            BillingAddressId = address.Id,
        };
    }

    public static List<Customer> GetCustomers()
    {
        return new List<Customer>
        {
            new Customer
            {
                Id = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                FirstName = "Fred",
                LastName = "Flintstone",
                Email = "fred.flintstone@test.com",
                BillingAddressId = new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
                BillingAddress = new Address
                {
                    Id = new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
                    Street = "123 Test St",
                    City = "Bedrock",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddressId = new Guid("6B760260-799C-4AF1-A173-0BF83A2A74D5"),
                ShippingAddress = new Address
                {
                    Id = new Guid("6B760260-799C-4AF1-A173-0BF83A2A74D5"),
                    Street = "123 Test St",
                    City = "Bedrock",
                    State = "MO",
                    PostalCode = "12345"
                },
            },
            new Customer
            {
                Id = new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                FirstName = "George",
                LastName = "Jetson",
                Email = "george.jetson@test.com",
                BillingAddressId = new Guid("B592FA04-541A-4BF2-967C-C07468AF2014"),
                BillingAddress = new Address
                {
                    Id = new Guid("B592FA04-541A-4BF2-967C-C07468AF2014"),
                    Street = "123 Test St",
                    City = "Space City",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddressId = new Guid("CCB9F54B-F5A0-4D42-927D-C65294E0F629"),
                ShippingAddress = new Address
                {
                    Id = new Guid("CCB9F54B-F5A0-4D42-927D-C65294E0F629"),
                    Street = "123 Test St",
                    City = "Space City",
                    State = "MO",
                    PostalCode = "12345"
                }
            }
        };
    }

    public static List<SecurityClaim> GetClaims()
    {
        return new List<SecurityClaim>
        {
            new SecurityClaim(
                new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                ClaimTypes.NameIdentifier,
                "4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
            new SecurityClaim(
                new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                ClaimTypes.Role,
                Roles.Customer),
            new SecurityClaim(
                new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                ClaimTypes.NameIdentifier,
                "79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
            new SecurityClaim(
                new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                ClaimTypes.Role,
                Roles.Admin)
        };
    }

    public static List<Product> GetProducts(int productCount = 10)
    {
        var products = new List<Product>
        {
            new Product
            {
                Id = new Guid("7BC8AE1B-031A-4F3A-815C-2111288FF58C"),
                Name = "Nike Tennis Shoes",
                Description = "These are some dope Nike Tennis Shoes!",
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("9955F4D7-3E40-4111-A76D-23406F93334B"),
                Name = "Fruit Stripe Gum",
                Description = "This is some tasty gum, but the flavor doesn't last!",
                Price = 1.99M
            },
            new Product
            {
                Id = new Guid("AD7D0CF7-CE00-477D-AE2A-5691F65EBA0E"),
                Name = "Cheerios",
                Description = "Cheerios are a healthy part of your breakfast!",
                Price = 6.00M
            },
            new Product
            {
                Id = new Guid("E226D6B2-324F-4508-B5E5-0DB77B345C69"),
                Name = "7Up",
                Description = "Crisp and clean with no caffeine!",
                Price = 1.50M
            },
            new Product
            {
                Id = new Guid("E3B2BCCE-A8F4-4F7E-9C9E-6AC93E03554A"),
                Name = "A Plaid Flannel Shirt",
                Description = "The 90s are calling and they want you back!",
                Price = 20.00M
            },
            new Product
            {
                Id = new Guid("1CAA7FB0-8C2E-4304-A1EC-747A89623131"),
                Name = "Garbage Pale Kids Stickers",
                Description = "The 80s are calling and they want you back!",
                Price = 4.00M
            },
            new Product
            {
                Id = new Guid("A9C15177-E1A4-4DC8-BCB0-5D78128FDEAE"),
                Name = "Pink Stuffed Dinosaur",
                Description = "Raaawwwrrrr!",
                Price = 15.99M
            },
            new Product
            {
                Id = new Guid("24EF70C3-0FC1-48D7-994F-380D4C533419"),
                Name = "Black Trenchcoat",
                Description = "Dark and mysterious!  Good for 2 kids who want to impersonate an adult.",
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("2DF1A80E-651A-417A-9028-B81D30A9A26E"),
                Name = "Monopoly",
                Description = "The game that destroys friendships and families!",
                Price = 100.00M
            },
            new Product
            {
                Id = new Guid("0553CA62-284D-4379-AFC5-C2D4903F7A4C"),
                Name = "A Dog Collar",
                Description = "Heavy duty!  Fits most dogs and some people.",
                Price = 100.00M
            }
        };

        return products.Take(productCount).ToList();
    }

    public static List<LineItemModel> GetLineItemModels()
    {
        return new List<LineItemModel>
        {
            new LineItemModel(new Guid("7BC8AE1B-031A-4F3A-815C-2111288FF58C"), 10),
            new LineItemModel(new Guid("9955F4D7-3E40-4111-A76D-23406F93334B"), 6),
            new LineItemModel(new Guid("E3B2BCCE-A8F4-4F7E-9C9E-6AC93E03554A"), 14)
        };
    }

    public static List<LineItem> GetLineItems(int take)
    {
        return GetLineItemModels().Take(take).Select(x => new LineItem(DefaultOrderId, x.ProductId, x.Quantity)).ToList();

    }

    public static Order GetOrder(int itemsCount = 3)
    {
        var order = new Order
        {
            Id = DefaultOrderId,
            CustomerId = DefaultCustomerId,
            Customer = GetCustomer(),
            OrderDateTimeUtc = MockProvider.DefaultUtcDateTime
        };
        order.AddUpdateLineItemRange(GetLineItems(itemsCount));

        return order;
    }

    public static List<Order> GetOrders()
    {
        var orders = new List<Order>();
        var orderToAdd = new Order
        {
            Id = DefaultOrderId,
            CustomerId = DefaultCustomerId,
            Customer = GetCustomer(),
            OrderDateTimeUtc = MockProvider.DefaultUtcDateTime
        };
        orderToAdd.AddUpdateLineItemRange(GetLineItems(3));
        orders.Add(orderToAdd);
        
        orderToAdd = new Order
        {
            Id = DefaultOrderId,
            CustomerId = DefaultCustomerId,
            Customer = GetCustomer(),
            OrderDateTimeUtc = MockProvider.DefaultUtcDateTime
        };
        orderToAdd.AddUpdateLineItemRange(GetLineItems(3));
        orders.Add(orderToAdd);

        return orders;
    }

    public static Product GetProduct()
    {
        return new Product
        {
            Id = new Guid("AA9F20AE-B5F2-44C4-8757-C6259CFDC794"),
            Name = "New Product #123",
            Description = "The latest and greatest in 123!",
            Price = 1000.00M,
            ImageUrl = "http://somedomain.com/image.jpg"
        };
    }
}