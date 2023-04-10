﻿namespace MyShoppingCart.Application.Tests.Handlers.Customers;

public class GetCustomerSecurityQueryHandlerTests
{
    private readonly CancellationToken _cancellationToken = new CancellationToken();

    #region Happy Path

    [Fact]
    public async Task Handle_ShouldReturnSecurityClaims_WhenAllParametersAreValid()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var claims = DataProvider.GetClaims();
        var request = new GetCustomerSecurityQuery(customer.Id);

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);

        var mockSecurityClaimsRepository = new Mock<IRepository<SecurityClaim>>();
        mockSecurityClaimsRepository
            .Setup(x => x.ListAsync(It.IsAny<GetSecurityClaimsByCustomerIdSpec>(), _cancellationToken))
            .ReturnsAsync(claims);

        var handler = new GetCustomerSecurityQueryHandler(mockCustomerRepository.Object, mockSecurityClaimsRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(claims);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), _cancellationToken), Times.Once);
        mockSecurityClaimsRepository
            .Verify(x => x.ListAsync(It.IsAny<GetSecurityClaimsByCustomerIdSpec>(), _cancellationToken), Times.Once);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenCustomerIsNotFound()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var request = new GetCustomerSecurityQuery(Guid.NewGuid());

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithNullResponse(_cancellationToken);

        var mockSecurityClaimsRepository = new Mock<IRepository<SecurityClaim>>();

        var handler = new GetCustomerSecurityQueryHandler(mockCustomerRepository.Object, mockSecurityClaimsRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.NotFound.Should().NotBeNull();
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), _cancellationToken), Times.Once);
        mockSecurityClaimsRepository
            .Verify(x => x.ListAsync(It.IsAny<GetSecurityClaimsByCustomerIdSpec>(), _cancellationToken), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptySecurityClaims_WhenNoSecurityClaimsFound()
    {
        //Arrange
        var customer = DataProvider.GetCustomer();
        var claims = new List<SecurityClaim>();
        var request = new GetCustomerSecurityQuery(customer.Id);

        var mockCustomerRepository = MockProvider.GetMockCustomerRepositoryWithSingleResponse(customer, _cancellationToken);

        var mockSecurityClaimsRepository = new Mock<IRepository<SecurityClaim>>();
        mockSecurityClaimsRepository
            .Setup(x => x.ListAsync(It.IsAny<GetSecurityClaimsByCustomerIdSpec>(), _cancellationToken))
            .ReturnsAsync(claims);

        var handler = new GetCustomerSecurityQueryHandler(mockCustomerRepository.Object, mockSecurityClaimsRepository.Object);

        //Act
        var results = await handler.Handle(request, _cancellationToken);

        //Assert
        results.Success.Should().NotBeNull().And.BeEquivalentTo(claims);
        mockCustomerRepository
            .Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerByIdSpec>(), _cancellationToken), Times.Once);
        mockSecurityClaimsRepository
         .Verify(x => x.ListAsync(It.IsAny<GetSecurityClaimsByCustomerIdSpec>(), _cancellationToken), Times.Once);
    }


    #endregion

}
