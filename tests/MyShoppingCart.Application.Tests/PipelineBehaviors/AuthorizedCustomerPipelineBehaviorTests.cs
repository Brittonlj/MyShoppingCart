using MediatR;
using MyShoppingCart.Application.Configuration;
using MyShoppingCart.Application.PipelineBehaviors;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.PipelineBehaviors;

public class AuthorizedCustomerPipelineBehaviorTests
{
	private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserSecurityService> _mockUserSecurityService;
    private readonly AuthorizedCustomerPipelineBehavior<GetCustomerQuery, Customer> _unitUnderTest;

	public AuthorizedCustomerPipelineBehaviorTests()
	{
		_mockUserSecurityService = new Mock<IUserSecurityService>();
		_mockUserSecurityService.Setup(x => x.IsInRole(Roles.Admin)).Returns(false);
		_mockUserSecurityService.Setup(x => x.GetCustomerId()).Returns(DataProvider.DefaultCustomerId);

		_unitUnderTest = new AuthorizedCustomerPipelineBehavior<GetCustomerQuery, Customer>(_mockUserSecurityService.Object);
	}

	#region Happy Path

	[Fact]
	public async Task Handle_ShouldProcessNext_WhenSecurityStateIsNormal()
	{
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
		var next = new RequestHandlerDelegate<Response<Customer>>(Next);

		//Act
		var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

		//Assert
		result.Success.Should().NotBeNull().And.Be(DataProvider.GetCustomer());
        _mockUserSecurityService.Verify(x => x.IsInRole(Roles.Admin), Times.Once);
        _mockUserSecurityService.Verify(x => x.GetCustomerId(), Times.Once);
    }

    #endregion

    #region Admin

    [Fact]
    public async Task Handle_ShouldProcessNextWithoutCheckingCustomerId_WhenUserIsAdmin()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<Customer>>(Next);
        _mockUserSecurityService.Setup(x => x.IsInRole(Roles.Admin)).Returns(true);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Success.Should().NotBeNull().And.Be(DataProvider.GetCustomer());
        _mockUserSecurityService.Verify(x => x.IsInRole(Roles.Admin), Times.Once);
        _mockUserSecurityService.Verify(x => x.GetCustomerId(), Times.Never);
    }

    #endregion

    #region CustomerId

    [Fact]
    public async Task Handle_ShouldReturnUnauthorized_WhenUserIdIsNotFound()
    {
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
        var next = new RequestHandlerDelegate<Response<Customer>>(Next);
        _mockUserSecurityService.Setup(x => x.GetCustomerId()).Returns(() => null);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Unauthorized.Should().NotBeNull();
        _mockUserSecurityService.Verify(x => x.IsInRole(Roles.Admin), Times.Once);
        _mockUserSecurityService.Verify(x => x.GetCustomerId(), Times.Once);
    }

    #endregion

    #region Private Helpers

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task<Response<Customer>> Next()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
	{
		return Response<Customer>.FromSuccess(DataProvider.GetCustomer());
	}

	#endregion
}
