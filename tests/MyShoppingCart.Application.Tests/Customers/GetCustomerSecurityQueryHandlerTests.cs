namespace MyShoppingCart.Application.Tests.Customers;

public class GetCustomerSecurityQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSecurityClaims_WhenAllParametersAreValid()
    {
        //Arrange
        var customer = DataHelper.GetCustomer();
        var claims = DataHelper.GetClaims(customer.Id);
        var request = new GetCustomerSecurityQuery(customer.Id);

        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken))
            .ReturnsAsync(customer);
        
        var mockSecurityClaimsRepository = new Mock<IRepository<SecurityClaim>>();
        mockSecurityClaimsRepository
            .Setup(x => x.ListAsync(It.IsAny<QuerySecurityClaims>(), _cancellationToken))
            .ReturnsAsync(claims);
        
        var handler = new GetCustomerSecurityQueryHandler(mockCustomerRepository.Object, mockSecurityClaimsRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(claims);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockSecurityClaimsRepository
            .Verify(x => x.ListAsync(It.IsAny<QuerySecurityClaims>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var customer = DataHelper.GetCustomer();
        var claims = new List<SecurityClaim>();
        var request = new GetCustomerSecurityQuery(Guid.NewGuid());
        
        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken))
            .ReturnsAsync(() => null);

        var mockSecurityClaimsRepository = new Mock<IRepository<SecurityClaim>>();
        mockSecurityClaimsRepository
            .Setup(x => x.ListAsync(It.IsAny<QuerySecurityClaims>(), _cancellationToken))
            .ReturnsAsync(claims);

        var handler = new GetCustomerSecurityQueryHandler(mockCustomerRepository.Object, mockSecurityClaimsRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockSecurityClaimsRepository
            .Verify(x => x.ListAsync(It.IsAny<QuerySecurityClaims>(), _cancellationToken), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptySecurityClaims_WhenNoSecurityClaimsFound()
    {
        //Arrange
        var customer = DataHelper.GetCustomer();
        var claims = new List<SecurityClaim>();
        var request = new GetCustomerSecurityQuery(customer.Id);

        var mockCustomerRepository = new Mock<IRepository<Customer>>();
        mockCustomerRepository
            .Setup(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken))
            .ReturnsAsync(customer);

        var mockSecurityClaimsRepository = new Mock<IRepository<SecurityClaim>>();
        mockSecurityClaimsRepository
            .Setup(x => x.ListAsync(It.IsAny<QuerySecurityClaims>(), _cancellationToken))
            .ReturnsAsync(claims);

        var handler = new GetCustomerSecurityQueryHandler(mockCustomerRepository.Object, mockSecurityClaimsRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(claims);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<QueryCustomerById>(), _cancellationToken), Times.Once);
        mockSecurityClaimsRepository
         .Verify(x => x.ListAsync(It.IsAny<QuerySecurityClaims>(), _cancellationToken), Times.Once);
    }


    #endregion

}
