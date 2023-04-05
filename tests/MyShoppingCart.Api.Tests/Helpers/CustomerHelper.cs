namespace MyShoppingCart.Api.Tests.Helpers;

public static class CustomerHelper
{
    public static IReadOnlyList<Customer> GetEmptyCustomersList()
    {
        return new List<Customer>();
    }

    public static IReadOnlyList<Customer> GetCustomerList()
    {
        var customers = new List<Customer>
        {
            new Customer
            {
                FirstName = "Bob",
                LastName = "Builder",
                Email = "bob.builder@test.com",
                BillingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
            new Customer
            {
                FirstName = "Fred",
                LastName = "Flintstone",
                Email = "fred.flintstone@test.com",
                BillingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
            new Customer
            {
                FirstName = "George",
                LastName = "Jetson",
                Email = "bob.builder@test.com",
                BillingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },

        };

        return customers;
    }

    public static Customer GetCustomer()
    {
        var customer = new Customer
        {
            FirstName = "George",
            LastName = "Jetson",
            Email = "bob.builder@test.com",
            BillingAddress = new Address
            {
                Street = "123 Test St",
                City = "Test Town",
                State = "MO",
                PostalCode = "12345"
            },
            ShippingAddress = new Address
            {
                Street = "123 Test St",
                City = "Test Town",
                State = "MO",
                PostalCode = "12345"
            }
        };

        return customer;

    }
}
