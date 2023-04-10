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
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), token))
            .ReturnsAsync(response);

        return mockCustomerRepository;
    }

    public static Mock<IRepository<Customer>> GetMockCustomerRepositoryWithNullResponse(CancellationToken token = default)
    {
        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), token))
            .ReturnsAsync(() => null);

        return mockCustomerRepository;
    }

    public static Mock<IRepository<Customer>> GetMockCustomerRepositoryWithManyResponses(List<Customer> customers, CancellationToken token = default)
    {
        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.ListAsync(It.IsAny<GetAllCustomersSpec>(), token))
            .ReturnsAsync(customers);
        return mockCustomerRepository;
    }

    public static Mock<IRepository<Product>> GetMockProductRepositoryWithSingleResponse(Product response, CancellationToken token = default)
    {
        var mockProductRepository = new Mock<IRepository<Product>>();
        mockProductRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetProductByIdSpec>(), token))
            .ReturnsAsync(response);

        return mockProductRepository;
    }

    public static Mock<IRepository<Product>> GetMockProductRepositoryWithNullResponse(CancellationToken token = default)
    {
        var mockProductRepository = new Mock<IRepository<Product>>();
        mockProductRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetProductByIdSpec>(), token))
            .ReturnsAsync(() => null);

        return mockProductRepository;
    }

    public static Mock<IRepository<Product>> GetMockProductRepositoryWithManyResponses(List<Product> products, CancellationToken token = default)
    {
        var mockProductRepository = new Mock<IRepository<Product>>();
        mockProductRepository
            .Setup(x => x.ListAsync(It.IsAny<GetAllProductsSpec>(), token))
            .ReturnsAsync(products);
        return mockProductRepository;
    }
}
