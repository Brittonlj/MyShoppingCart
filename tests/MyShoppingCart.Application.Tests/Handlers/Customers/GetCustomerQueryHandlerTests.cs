using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public class GetCustomerQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserManagerFacade> _mockUserManager = new Mock<IUserManagerFacade>();
    private readonly Mock<IMapper> _mapper = new Mock<IMapper>();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenAllParametersAreValid()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var request = new GetCustomerQuery(Guid.NewGuid());
        var customerModel = DataProvider.GetCustomerModel();

        _mapper.Setup(x => x.Map<CustomerModel>(customer)).Returns(customerModel);

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);

        var handler = new GetCustomerQueryHandler(mockCustomerRepository.Object, _mapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.Be(customerModel);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var request = new GetCustomerQuery(Guid.NewGuid());

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithNullResponse(_cancellationToken);

        var handler = new GetCustomerQueryHandler(mockCustomerRepository.Object, _mapper.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

}
