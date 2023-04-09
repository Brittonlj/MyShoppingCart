using MyShoppingCart.Domain.Utilities;

namespace MyShoppingCart.Application.Tests.Helpers;

public static class MockProvider
{
    public static readonly DateTime DefaultUtcDateTime = new DateTime(2023, 1, 1, 1, 1, 1, DateTimeKind.Utc);

    public static IUtcDateTimeProvider GetUtcDateTimeProvider()
    {
        var mockDateTimeProvider = new Mock<IUtcDateTimeProvider>();
        mockDateTimeProvider.Setup(x => x.GetUtcDateTime()).Returns(DefaultUtcDateTime);
        return mockDateTimeProvider.Object;
    }

    public static Mock<IRepository<Customer>> GetMockCustomerRepositoryWithSingleResponse(Customer response, CancellationToken token = default)
    {
        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), token))
            .ReturnsAsync(response);

        return mockCustomerRepository;
    }

    public static Mock<IRepository<Customer>> GetMockCustomerRepositoryWithNullResponse(CancellationToken token = default)
    {
        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), token))
            .ReturnsAsync(() => null);

        return mockCustomerRepository;
    }

    public static Mock<IRepository<Customer>> GetMockCustomerRepositoryWithManyResponses(List<Customer> customers, CancellationToken token = default)
    {
        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.ListAsync(It.IsAny<QueryAllCustomers>(), token))
            .ReturnsAsync(customers);

        return mockCustomerRepository;
    }
}
