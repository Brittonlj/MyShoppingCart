using MyShoppingCart.Application.Configuration;
using System.Security.Claims;

namespace MyShoppingCart.Application.Tests.Helpers;

public static class DataHelper
{
    public static Customer GetCustomer()
    {
        var address = new Address(
            new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new Customer
        {
            Id = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
            FirstName = "Fred",
            LastName = "Flintstone",
            Email = "fred.flintstone@test.com",
            ShippingAddress = address,
            BillingAddress = address
        };
    }

    public static List<Customer> GetCustomers()
    {
        var address = new Address(
            new Guid("786DE95E-2D4C-4524-AC64-6DDF11AD9EC5"),
            "123 Test Street",
            "Test Town",
            "MO",
            "12345");
        return new List<Customer>
        {
            new Customer
            {
                Id = new Guid("4A5EB696-7C8F-47D4-974B-C1DA72CEC2C5"),
                FirstName = "Fred",
                LastName = "Flintstone",
                Email = "fred.flintstone@test.com",
                ShippingAddress = address,
                BillingAddress = address
            },
            new Customer
            {
                Id = new Guid("79F42C77-83E5-403B-9EC1-6A3FF285C5AC"),
                FirstName = "George",
                LastName = "Jetson",
                Email = "george.jetson@test.com",
                ShippingAddress = address,
                BillingAddress = address
            }
        };
    }

    public static List<SecurityClaim> GetClaims(Guid customerId)
    {
        return new List<SecurityClaim>
        {
            new SecurityClaim(new Guid("B27DE938-6647-42E3-8DB2-145AB037F295"), customerId, ClaimTypes.Name, customerId.ToString()),
            new SecurityClaim(new Guid("1E51A7C1-D3C2-4DC0-96BE-A2A056A2331A"), customerId, ClaimTypes.Role, Roles.Customer)
        };
    }

    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = new Guid("452D3B0A-5FB2-43E5-A7BE-F4E49B930B1D"),
                Name = "Tennis Shoes",
                Description = "Some Tennis Shoes",
                Price = 10.00M,
                ImageUrl = null
            },
            new Product
            {
                Id = new Guid("516874DD-6CE7-4A5A-A2C0-E6FBA73DB4FC"),
                Name = "Running Shorts",
                Description = "Some Running Shorts",
                Price = 25.00M,
                ImageUrl = null
            },
            new Product
            {
                Id = new Guid("739FCB33-4A09-43D7-8CCD-949AA41053F1"),
                Name = "Baseball Cap",
                Description = "A Baseball Cap",
                Price = 15.00M,
                ImageUrl = null
            }
        };
    }

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

    public static GetCustomersQuery GetGetCustomersQuery()
    {
        return new GetCustomersQuery(
            "Fred",
            "test.com",
            1,
            20,
            "LastName");

    }

}
