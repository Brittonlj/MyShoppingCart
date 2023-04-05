using MyShoppingCart.Domain.Data;

namespace MyShoppingCart.Application.Tests.UnitOfWork;

public class UnitOfWorkProvider
{
    public IUnitOfWork GetUnitOfWork()
    {
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        mockUnitOfWork.Setup(x => x.Orders);

        return mockUnitOfWork.Object;


    }

    public static Customer GetCustomer(Guid customerId)
    {
        return new Customer
        {
            Id = customerId,
            FirstName = "Bob",
            LastName = "Builder",
            Email = "bob.builder@test.com",
            BillingAddress = new Address
            {
                Id = new Guid("C42C546A-1AFE-4EE1-B9C2-8E805DDF1B88"),
                Street = "123 Test St",
                City = "Test Town",
                State = "MO",
                PostalCode = "12345"
            },
            ShippingAddress = new Address
            {
                Id = new Guid("AC6CF840-9CAB-40EC-BA86-837A3D19B22D"),
                Street = "123 Test St",
                City = "Test Town",
                State = "MO",
                PostalCode = "12345"
            }
        };
    }

    public static List<Customer> GetCustomers()
    {
        return new List<Customer>
        {
            new Customer
            {
                Id = new Guid("118B5F36-FB55-4EA1-B466-38140268D21F"),
                FirstName = "Bob",
                LastName = "Builder",
                Email = "bob.builder@test.com",
                BillingAddress = new Address
                {
                    Id = new Guid("C42C546A-1AFE-4EE1-B9C2-8E805DDF1B88"),
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Id = new Guid("AC6CF840-9CAB-40EC-BA86-837A3D19B22D"),
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
            new Customer
            {
                Id = new Guid("945E7453-CDFB-47E3-8592-CAD7B0B69C1A"),
                FirstName = "Fred",
                LastName = "Flintstone",
                Email = "fred.flintstone@test.com",
                BillingAddress = new Address
                {
                    Id = new Guid("0CCA5871-CC1F-4E21-8150-F3469F76C035"),
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {
                    Id = new Guid("C6DF804B-1513-44F8-9A56-1AFB399F664C"),
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
            new Customer
            {
                Id = new Guid("41D501FC-E2D0-4705-B030-AA1391A89508"),
                FirstName = "George",
                LastName = "Jetson",
                Email = "bob.builder@test.com",
                BillingAddress = new Address
                {
                    Id = new Guid("8EE0D76C-1B4A-4FCE-9019-2A314595A669"),
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                },
                ShippingAddress = new Address
                {   
                    Id = new Guid("27FC9212-CE92-4399-8AC3-888294E2080A"),
                    Street = "123 Test St",
                    City = "Test Town",
                    State = "MO",
                    PostalCode = "12345"
                }
            },
        };
    }
}
