using MediatR;
using MyShoppingCart.Application.Configuration;
using MyShoppingCart.Application.PipelineBehaviors;
using MyShoppingCart.Application.Services;

namespace MyShoppingCart.Application.Tests.PipelineBehaviors;

public class AuthorizedCustomerPipelineBehaviorTests
{
	private readonly CancellationToken _cancellationToken = new CancellationToken();
    private readonly Mock<IUserSecurityService> _mockUserSecurityService;
    private readonly AuthorizedCustomerPipelineBehavior<GetCustomerQuery, CustomerModel> _unitUnderTest;

	public AuthorizedCustomerPipelineBehaviorTests()
	{
		_mockUserSecurityService = new Mock<IUserSecurityService>();
		_mockUserSecurityService.Setup(x => x.IsInRole(Roles.Admin)).Returns(false);
		_mockUserSecurityService.Setup(x => x.GetCustomerId()).Returns(DataProvider.DefaultCustomerId);

		_unitUnderTest = new AuthorizedCustomerPipelineBehavior<GetCustomerQuery, CustomerModel>(_mockUserSecurityService.Object);
	}

	#region Happy Path

	[Fact]
	public async Task Handle_ShouldProcessNext_WhenSecurityStateIsNormal()
	{
        //Arrange
        var request = new GetCustomerQuery(DataProvider.DefaultCustomerId);
		var next = new RequestHandlerDelegate<Response<CustomerModel>>(Next);

		//Act
		var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

		//Assert
		result.Success.Should().NotBeNull().And.BeEquivalentTo(DataProvider.GetCustomerModel());
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
        var next = new RequestHandlerDelegate<Response<CustomerModel>>(Next);
        _mockUserSecurityService.Setup(x => x.IsInRole(Roles.Admin)).Returns(true);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Success.Should().NotBeNull().And.BeEquivalentTo(DataProvider.GetCustomerModel());
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
        var next = new RequestHandlerDelegate<Response<CustomerModel>>(Next);
        _mockUserSecurityService.Setup(x => x.GetCustomerId()).Returns(() => null);

        //Act
        var result = await _unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Unauthorized.Should().NotBeNull();
        _mockUserSecurityService.Verify(x => x.IsInRole(Roles.Admin), Times.Once);
        _mockUserSecurityService.Verify(x => x.GetCustomerId(), Times.Once);
    }

    #endregion

    #region Not IAuthorizedCustomerRequest

    [Fact]
    public async Task Handle_ShouldProcessNext_WhenNotIAuthorizedCustomerRequest()
    {
        //Arrange
        var request = new GetProductsQuery(null, 1, 20, "Name", true);
        var next = new RequestHandlerDelegate<Response<IReadOnlyList<Product>>>(NextProduct);
        var unitUnderTest = new AuthorizedCustomerPipelineBehavior<GetProductsQuery, IReadOnlyList<Product>>(_mockUserSecurityService.Object);

        //Act
        var result = await unitUnderTest.Handle(request, next, _cancellationToken);

        //Assert
        result.Success.Should().NotBeNull().And.BeEquivalentTo(DataProvider.GetProducts());
        _mockUserSecurityService.Verify(x => x.IsInRole(Roles.Admin), Times.Never);
        _mockUserSecurityService.Verify(x => x.GetCustomerId(), Times.Never);
    }


    #endregion

    #region Private Helpers

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    private async Task<Response<CustomerModel>> Next()
    {
        return Response<CustomerModel>.FromSuccess(DataProvider.GetCustomerModel());
    }

    private async Task<Response<IReadOnlyList<Product>>> NextProduct()
    {
        return Response<IReadOnlyList<Product>>.FromSuccess(DataProvider.GetProducts());
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    #endregion
}
